using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StupidPlayerInput)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(PlayerAnimations))]
public class PlayerController : MonoBehaviour
{
    [Header("Other player scripts")]
    [SerializeField] private PlayerAnimations playerAnim;

    [Header("State")]
    public bool stunned;
    public bool dashing;
    public bool grounded;
    public bool holding;
    public bool walking;
    public bool blocking;

    [Header("Blocking")]
    public float blockingTime;
    public GameObject smokePoofPrefab;
    public GameObject blockingModel;
    public GameObject playerModel;

    [Header("Picking up and throwing")]
    public float pickupRange;
    public float holdingDistance;
    public float holdingHeight;
    public float holdingSpeed;

    public float throwSpeed;
    private Rigidbody rockBody;
    private float rockThrowDelay = 0.35f;

    [Header("Stunning")]
    public LayerMask floorMask;
    public GameObject stunDecal;
    public float stunDuration;
    private float stunCounter = int.MinValue;

    [Header("Default Movement")]
    public float movementSpeed;
    public float rotationSpeed;
    public float stayUprightSpeed;

    [Header("Jumping")]
    public float jumpingForce;
    public Transform jumpCheckPoint;
    public float gravityWhenNotGrounded;
    public float gravityApplicationFactor;
    private float graduallyAplliedGravity = 0;

    [Header("Dashing")]
    public float dashingKnockBack;
    public float additionalDashSpeed;
    public float dashDuration;
    public float dashCooldown;
    private float dashCounter;
    private float dashCooldownCounter;

    private Vector3 direction;
    private Vector3 rotationDirection;

    private StupidPlayerInput spi;
    private Rigidbody rb;

    private void Start()
    {
        spi = GetComponent<StupidPlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //stunning state
        stunCounter -= Time.deltaTime;
        stunned = stunCounter > 0;

        stunDecal.SetActive(stunned);

        if (blocking)
        {
            return;
        }
        if (!stunned)
        {
            if (spi.controller.XDown)
            {
                Block();
                Invoke("UnBlock", blockingTime);
            }
        }

        //grounded state
        Collider[] colliders = Physics.OverlapSphere(jumpCheckPoint.position, 0.1f, floorMask);
        grounded = colliders.Length > 0;

        //jumping
        if (grounded && spi.controller.ADown)
        {
            rb.AddForce(Vector3.up * jumpingForce, ForceMode.Acceleration);
            playerAnim.TriggerJumpAnimation();
        }

        //Dashing
        dashCooldownCounter -= Time.deltaTime;
        dashCounter -= Time.deltaTime;

        dashing = dashCounter > 0;

        if ((spi.controller.L1Down || spi.controller.L2Down) && dashCooldownCounter < 0.0f)
        {
            dashCooldownCounter = dashCooldown;
            dashCounter = dashDuration;
        }

        //Rock pickup
        if (holding && rockBody != null)
        {
            //Move with the player
            rockBody.MovePosition(transform.position + transform.forward * holdingDistance + Vector3.up * holdingHeight);
            rockBody.rotation = transform.rotation;
            rockBody.GetComponent<Collider>().enabled = false;
            rockBody.useGravity = false;

            //Throw the rock
            if (spi.controller.YDown)
            {
                StartCoroutine(this.ThrowRock());
                playerAnim.TriggerThrowAnimation();
            }
        }

        if (spi.controller.BDown) // Pick up / put down
        {
            bool prevHolding = holding;
            if (holding && rockBody != null)
            {
                //drop the rock
                rockBody.GetComponent<Collider>().enabled = true;
                rockBody.useGravity = true;
                holding = false;
            }
            else
            {
                Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, pickupRange);

                for (int i = 0; i < nearbyObjects.Length; i++)
                {
                    if (nearbyObjects[i].tag == "Rock")
                    {
                        rockBody = nearbyObjects[i].GetComponent<Rigidbody>();
                        holding = true;
                    }
                }
            }

            if (prevHolding != holding)
            {
                playerAnim.ToggleCarryAnimation();
            }
        }
    }

    private IEnumerator ThrowRock ()
    {
        yield return new WaitForSeconds(rockThrowDelay);
        holding = false;
        rockBody.useGravity = true;
        rockBody.AddForce(transform.forward* throwSpeed, ForceMode.Acceleration); //changed to acceleration to add force independent of mass

        //Enable collider again
        yield return new WaitForSeconds(.1f);
        rockBody.GetComponent<Collider>().enabled = true;
    }

    private void FixedUpdate()
    {
        //if not stunned
        if (!stunned && !blocking)
        {
            //keep player upright
            Quaternion rot = Quaternion.FromToRotation(transform.up, Vector3.up);
            rb.AddTorque(new Vector3(rot.x, rot.y, rot.z) * stayUprightSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);

            //physics based movement and dashing
            direction = new Vector3(spi.controller.Joystick_Left.x, 0, spi.controller.Joystick_Left.y);
            Vector3 movementVector = ((direction * movementSpeed) +
                        (dashing ? (direction * additionalDashSpeed) : Vector3.zero)) 
                        * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + movementVector);

            // Setting player walk animation according to wether we're moving
            walking = movementVector != Vector3.zero;

            //physics based rotation
            Vector3 newRotationDirection = new Vector3(spi.controller.JoystickRaw_Left.x, 0, spi.controller.JoystickRaw_Left.y);

            //only rotate if stick is actually being pointed somewhere
            if (newRotationDirection.magnitude != 0)
            {
                rotationDirection = newRotationDirection;

                //calculate and rotate smoothly
                Quaternion lookRotation = Quaternion.LookRotation(rotationDirection);
                Quaternion smoothedRotation = Quaternion.Slerp(rb.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);

                rb.MoveRotation(smoothedRotation);
            }

            if (grounded)
            {
                graduallyAplliedGravity = 0;
            }
            else
            {
                graduallyAplliedGravity = Mathf.Lerp(graduallyAplliedGravity, gravityWhenNotGrounded, gravityApplicationFactor);
                rb.AddForce(Vector3.down * graduallyAplliedGravity, ForceMode.Acceleration);
            }
        }

        playerAnim.isWalking = walking;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //for stunning other players
        if (dashing)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerController otherPlayer = collision.gameObject.GetComponent<PlayerController>();

                if (!otherPlayer.stunned)
                {
                    //calculate knockback for other player
                    Vector3 knockback = (collision.transform.position - transform.position).normalized;

                    //stun
                    otherPlayer.Stun(knockback);
                }
            }
            else if(collision.gameObject.CompareTag("Rock"))
            {
                //calculate knockback for rock
                Vector3 knockback = (collision.transform.position - transform.position).normalized * dashingKnockBack;

                //do knockback
                collision.gameObject.GetComponent<Rigidbody>().AddForce(knockback, ForceMode.Acceleration);
            }
        }
    }

    public void Stun(Vector3 knockbackDirection)
    {
        if (!blocking)
        {
            //if holding rock drop it
            if (rockBody != null)
            {
                rockBody.useGravity = true;
                holding = false;
            }

            stunCounter = stunDuration;
            stunned = true;

            rb.AddForce(knockbackDirection * dashingKnockBack, ForceMode.Acceleration);
        }
    }

    public void Block()
    {
        blocking = true;

        Instantiate(smokePoofPrefab, transform.position + Vector3.up, Quaternion.identity);

        playerModel.SetActive(false);
        blockingModel.SetActive(true);
        rb.isKinematic = true;
    }

    public void UnBlock()
    {
        blocking = false;

        Instantiate(smokePoofPrefab, transform.position + Vector3.up, Quaternion.identity);

        playerModel.SetActive(true);
        blockingModel.SetActive(false);
        rb.isKinematic = false;
    }
}