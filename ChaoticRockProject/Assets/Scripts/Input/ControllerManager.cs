using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    public int connectedControllers;
    public string[] controllerNames;

    private void Start()
    {
        connectedControllers = Gamepad.all.Count;
    }

    private void Update()
    {
        connectedControllers = Gamepad.all.Count;

        controllerNames = new string[Gamepad.all.Count];
        for(int n = 0; n < controllerNames.Length; n++)
        {
            controllerNames[n] = Gamepad.all[n].name;
        }
    }
}