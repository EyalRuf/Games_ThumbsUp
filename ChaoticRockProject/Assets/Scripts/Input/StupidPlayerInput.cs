using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StupidPlayerInput : MonoBehaviour
{
    public StupidController controller = new StupidController(0);
    private ControllerManager manager;

    private void Start()
    {
        manager = FindObjectOfType<ControllerManager>();
    }

    private void Update()
    {
        if(manager == null)
        {
            Debug.LogError("There is no controller manager in the scene, please add one.");
            manager = FindObjectOfType<ControllerManager>();
            return;
        }

        controller.Update(manager.connectedControllers);
    }
}

[System.Serializable]
public class StupidController
{
    [Header("General")]
    public bool Debug;
    public bool isConnected;
    public int gamepadId;


    [Header("Joysticks")]
    public float JoystickDampSpeed = 10;
    public float deadzone = 0.1f;

    private Vector2 _joystick_Left = Vector2.zero;
    private Vector2 Joystick_Left_Velocity = Vector2.zero;
    public Vector2 Joystick_Left = Vector2.zero;

    private Vector2 _joystick_Right = Vector2.zero;
    private Vector2 Joystick_Right_Velocity = Vector2.zero;
    public Vector2 Joystick_Right = Vector2.zero;


    [Header("Raw Joysticks")]
    public Vector2 JoystickRaw_Left = Vector2.zero;
    public Vector2 JoystickRaw_Right = Vector2.zero;

    [Header("Buttons")]
    public bool StartDown;
    public bool Start;
    public bool StartUP;

    public bool SelectDown;
    public bool Select;
    public bool SelectUP;

    public bool ADown;
    public bool A;
    public bool AUP;

    public bool BDown;
    public bool B;
    public bool BUP;

    public bool XDown;
    public bool X;
    public bool XUP;

    public bool YDown;
    public bool Y;
    public bool YUP;

    public bool L1Down;
    public bool L1;
    public bool L1UP;

    public bool L2Down;
    public bool L2;
    public bool L2UP;

    public bool R1Down;
    public bool R1;
    public bool R1UP;

    public bool R2Down;
    public bool R2;
    public bool R2UP;

    private Key[] DebugKeys = { 
        Key.A, Key.D, Key.W, Key.S,
        Key.LeftArrow, Key.RightArrow, Key.UpArrow, Key.DownArrow,
        Key.Escape, Key.Backspace,
        Key.Space, Key.LeftShift, Key.LeftCtrl, Key.Q,
        Key.RightShift, Key.RightCtrl, Key.Numpad1, Key.Numpad0
    };

    public StupidController(int gamepadId)
    {
        this.gamepadId = gamepadId;
    }

    public void Update(int gamepadsConnected)
    {
        //switches controls to the keyboard
        if (Debug)
        {
            //read raw analog sticks
            JoystickRaw_Left = new Vector2(
                (Keyboard.current[DebugKeys[0]].isPressed ? 0 : 1) + (Keyboard.current[DebugKeys[1]].isPressed ? 0 : -1),
                (Keyboard.current[DebugKeys[2]].isPressed ? 1 : 0) + (Keyboard.current[DebugKeys[3]].isPressed ? -1 : 0)
            );

            JoystickRaw_Right = new Vector2(
                (Keyboard.current[DebugKeys[4]].isPressed ? 0 : 1) + (Keyboard.current[DebugKeys[5]].isPressed ? 0 : -1),
                (Keyboard.current[DebugKeys[6]].isPressed ? 1 : 0) + (Keyboard.current[DebugKeys[7]].isPressed ? -1 : 0)
            );

            //read buttons
            StartDown = Keyboard.current[DebugKeys[8]].wasPressedThisFrame;
            Start = Keyboard.current[DebugKeys[8]].isPressed;
            StartUP = Keyboard.current[DebugKeys[8]].wasReleasedThisFrame;

            SelectDown = Keyboard.current[DebugKeys[9]].wasPressedThisFrame;
            Select = Keyboard.current[DebugKeys[9]].isPressed;
            SelectUP = Keyboard.current[DebugKeys[9]].wasReleasedThisFrame;

            ADown = Keyboard.current[DebugKeys[10]].wasPressedThisFrame;
            A = Keyboard.current[DebugKeys[10]].isPressed;
            AUP = Keyboard.current[DebugKeys[10]].wasReleasedThisFrame;

            BDown = Keyboard.current[DebugKeys[11]].wasPressedThisFrame;
            B = Keyboard.current[DebugKeys[11]].isPressed;
            BUP = Keyboard.current[DebugKeys[11]].wasReleasedThisFrame;

            XDown = Keyboard.current[DebugKeys[11]].wasPressedThisFrame;
            X = Keyboard.current[DebugKeys[12]].isPressed;
            XUP = Keyboard.current[DebugKeys[12]].wasReleasedThisFrame;

            YDown = Keyboard.current[DebugKeys[13]].wasPressedThisFrame;
            Y = Keyboard.current[DebugKeys[13]].isPressed;
            YUP = Keyboard.current[DebugKeys[13]].wasReleasedThisFrame;

            L1Down = Keyboard.current[DebugKeys[14]].wasPressedThisFrame;
            L1 = Keyboard.current[DebugKeys[14]].isPressed;
            L1UP = Keyboard.current[DebugKeys[14]].wasReleasedThisFrame;

            L2Down = Keyboard.current[DebugKeys[15]].wasPressedThisFrame;
            L2 = Keyboard.current[DebugKeys[15]].isPressed;
            L2UP = Keyboard.current[DebugKeys[15]].wasReleasedThisFrame;

            R1Down = Keyboard.current[DebugKeys[16]].wasPressedThisFrame;
            R1 = Keyboard.current[DebugKeys[16]].isPressed;
            R1UP = Keyboard.current[DebugKeys[16]].wasReleasedThisFrame;

            R2Down = Keyboard.current[DebugKeys[17]].wasPressedThisFrame;
            R2 = Keyboard.current[DebugKeys[17]].isPressed;
            R2UP = Keyboard.current[DebugKeys[17]].wasReleasedThisFrame;
        }
        else
        {
            //if controller isn't connected
            if (gamepadId <= gamepadsConnected)
            {
                isConnected = false;
                return;
            }
            else
            {
                isConnected = true;
            }

            //read raw analog sticks
            JoystickRaw_Left = Gamepad.all[gamepadId].leftStick.ReadValue();
            JoystickRaw_Right = Gamepad.all[gamepadId].rightStick.ReadValue();

            //read buttons
            StartDown = Gamepad.all[gamepadId].startButton.wasPressedThisFrame;
            Start = Gamepad.all[gamepadId].startButton.isPressed;
            StartUP = Gamepad.all[gamepadId].startButton.wasReleasedThisFrame;

            SelectDown = Gamepad.all[gamepadId].selectButton.wasPressedThisFrame;
            Select = Gamepad.all[gamepadId].selectButton.isPressed;
            SelectUP = Gamepad.all[gamepadId].selectButton.wasReleasedThisFrame;

            ADown = Gamepad.all[gamepadId].aButton.wasPressedThisFrame;
            A = Gamepad.all[gamepadId].aButton.isPressed;
            AUP = Gamepad.all[gamepadId].aButton.wasReleasedThisFrame;

            BDown = Gamepad.all[gamepadId].bButton.wasPressedThisFrame;
            B = Gamepad.all[gamepadId].bButton.isPressed;
            BUP = Gamepad.all[gamepadId].bButton.wasReleasedThisFrame;

            XDown = Gamepad.all[gamepadId].xButton.wasPressedThisFrame;
            X = Gamepad.all[gamepadId].xButton.isPressed;
            XUP = Gamepad.all[gamepadId].xButton.wasReleasedThisFrame;

            YDown = Gamepad.all[gamepadId].yButton.wasPressedThisFrame;
            Y = Gamepad.all[gamepadId].yButton.isPressed;
            YUP = Gamepad.all[gamepadId].yButton.wasReleasedThisFrame;

            L1Down = Gamepad.all[gamepadId].leftShoulder.wasPressedThisFrame;
            L1 = Gamepad.all[gamepadId].leftShoulder.isPressed;
            L1UP = Gamepad.all[gamepadId].leftShoulder.wasReleasedThisFrame;

            L2Down = Gamepad.all[gamepadId].leftTrigger.wasPressedThisFrame;
            L2 = Gamepad.all[gamepadId].leftTrigger.isPressed;
            L2UP = Gamepad.all[gamepadId].leftTrigger.wasReleasedThisFrame;

            R1Down = Gamepad.all[gamepadId].rightShoulder.wasPressedThisFrame;
            R1 = Gamepad.all[gamepadId].rightShoulder.isPressed;
            R1UP = Gamepad.all[gamepadId].rightShoulder.wasReleasedThisFrame;

            R2Down = Gamepad.all[gamepadId].rightTrigger.wasPressedThisFrame;
            R2 = Gamepad.all[gamepadId].rightTrigger.isPressed;
            R2UP = Gamepad.all[gamepadId].rightTrigger.wasReleasedThisFrame;
        }

        //spring joysticks
        _joystick_Left = Vector2.SmoothDamp(_joystick_Left, JoystickRaw_Left, ref Joystick_Left_Velocity, 1 / JoystickDampSpeed);
        _joystick_Right = Vector2.SmoothDamp(_joystick_Right, JoystickRaw_Right, ref Joystick_Right_Velocity, 1 / JoystickDampSpeed);

        //deadzone
        if(_joystick_Left.magnitude < deadzone)
        {
            Joystick_Left = Vector2.zero;
        }
        else
        {
            Joystick_Left = _joystick_Left;
        }

        if (_joystick_Right.magnitude < deadzone)
        {
            Joystick_Right = Vector2.zero;
        }
        else
        {
            Joystick_Right = _joystick_Right;
        }
    }
}