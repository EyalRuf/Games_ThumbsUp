using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    StupidPlayerInput spi;

    public int speed = 10;
 
    void Start()
    {
        spi = GetComponent<StupidPlayerInput>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(-spi.controller.Joystick_Left.x, 0, spi.controller.Joystick_Left.y);      

        transform.position += movement * Time.deltaTime * speed;
        //GetComponent<Rigidbody>().MovePosition(transform.position + movement * Time.deltaTime * speed);
    }
}