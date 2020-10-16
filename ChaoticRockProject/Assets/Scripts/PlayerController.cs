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

    [Header("Picking up and throwing")]
    public float pickupRange;
    public float holdingDistance;
    public float holdingSpeed;

    public float throwSpeed;
    private Rigidbody rockBody;

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

        //grounded state
        Collider[] colliders = Physics.OverlapSphere(jumpCheckPoint.position, 0.01f, floorMask);
        grounded = colliders.Length > 0;

        //jumping
        if (grounded && spi.controller.ADown)
        {
            rb.AddForce(Vector3.up * jumpingForce, ForceMode.Acceleration);
            if (playerAnim != null)
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
            rockBody.MovePosition(Vector3.Lerp(rockBody.position, transform.position + transform.forward * holdingDistance, holdingSpeed * Time.deltaTime));
            rockBody.useGravity = false;

            //Throw the rock
            if (spi.controller.YDown)
            {
                rockBody.useGravity = true;
                holding = false;
                rockBody.AddForce(transform.forward * throwSpeed, ForceMode.Acceleration); //changed to acceleration to add force independent of mass
                if (playerAnim != null)
                    playerAnim.ToggleCarryAnimation();
            }
        }

        if (spi.controller.BDown)
        {
            bool prevHolding = holding;
            if(holding && rockBody != null)
            {
                //drop the rock
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
                if (playerAnim != null)
                    playerAnim.ToggleCarryAnimation();
            }
        }
    }

    private void FixedUpdate()
    {
        bool isWalking = false;

        //if not stunned
        if (!stunned)
        {
            //keep player upright
            Quaternion rot = Quaternion.FromToRotation(transform.up, Vector3.up);
            rb.AddTorque(new Vector3(rot.x, rot.y, rot.z) * stayUprightSpeed * Time.fixedDeltaTime);

            //physics based movement and dashing
            direction = new Vector3(spi.controller.Joystick_Left.x, 0, spi.controller.Joystick_Left.y);
            Vector3 movementVector = ((direction * movementSpeed) +
                        (dashing ? (direction * additionalDashSpeed) : Vector3.zero)) 
                        * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + movementVector);

            // Setting player walk animation according to weather we're moving
            isWalking = movementVector != Vector3.zero;

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
        }

        if (playerAnim != null)
            playerAnim.isWalking = isWalking;
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
        //if holding rock drop it
        if(rockBody != null)
        {
            rockBody.useGravity = true;
            holding = false;
        }

        stunCounter = stunDuration;
        stunned = true;

        rb.AddForce(knockbackDirection * dashingKnockBack, ForceMode.Acceleration);
    }
}