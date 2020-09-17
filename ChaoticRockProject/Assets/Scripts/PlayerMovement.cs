using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StupidPlayerInput)), RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

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
        //Old non physics movement
        //Vector3 movement = new Vector3(spi.controller.Joystick_Left.x, 0, spi.controller.Joystick_Left.y);
        //transform.position += movement * Time.deltaTime * movementSpeed;
    }

    private void FixedUpdate()
    {
        //physics based movement
        direction = new Vector3(spi.controller.Joystick_Left.x, 0, spi.controller.Joystick_Left.y);
        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);

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
}