using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;
    public float controllerSensitivity; 
    public float joystickDeadzone = 0.1f; 
    public Transform Player;
    float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity * Time.fixedDeltaTime;

        float controllerX = Gamepad.current.rightStick.x.ReadValue();
        float controllerY = Gamepad.current.rightStick.y.ReadValue();

        if (Mathf.Abs(controllerX) < joystickDeadzone)
        {
            controllerX = 0f;
        }

        if (Mathf.Abs(controllerY) < joystickDeadzone)
        {
            controllerY = 0f;
        }

        xRotation -= mouseY + controllerY * controllerSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * (mouseX + controllerX * controllerSensitivity));
    }
}
