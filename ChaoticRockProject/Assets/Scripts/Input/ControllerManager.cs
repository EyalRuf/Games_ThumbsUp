using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    public int connectedControllers;

    private void Start()
    {
        connectedControllers = Gamepad.all.Count;
    }

    private void Update()
    {
        connectedControllers = Gamepad.all.Count;
    }
}