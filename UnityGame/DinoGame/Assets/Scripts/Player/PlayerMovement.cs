using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float gravity = -9.81f;
    public float jumpHeight;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    bool isGrounded;

    public Canvas deathCanvas;

    public float slowMotionFactor;
    public float slowMotionDuration;
    private bool isSlowed = false;

    public float leftJoystickDeadzone = 0.2f;

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        if ((Keyboard.current.spaceKey.wasPressedThisFrame || Gamepad.current.buttonSouth.wasPressedThisFrame) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            print("Salto");
        }

        float x = 0f;
        float z = 0f;

        if (Keyboard.current != null)
        {
            x += Keyboard.current.aKey.isPressed ? -1f : 0f;
            x += Keyboard.current.dKey.isPressed ? 1f : 0f;
            z += Keyboard.current.sKey.isPressed ? -1f : 0f;
            z += Keyboard.current.wKey.isPressed ? 1f : 0f;
        }

        if (Gamepad.current != null)
        {
            float leftStickX = Gamepad.current.leftStick.x.ReadValue();
            float leftStickY = Gamepad.current.leftStick.y.ReadValue();

            if (Mathf.Abs(leftStickX) < leftJoystickDeadzone)
            {
                leftStickX = 0f;
            }

            if (Mathf.Abs(leftStickY) < leftJoystickDeadzone)
            {
                leftStickY = 0f;
            }

            x += leftStickX;
            z += leftStickY;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DinoAttackPoint"))
        {
            speed = 0f;
            Cursor.lockState = CursorLockMode.None;
            DisableGun("GunSystem");

            if (deathCanvas != null)
            {
                deathCanvas.gameObject.SetActive(true);
                print("Morido");
            }

            if (!isSlowed)
            {
                StartCoroutine(SlowMotion());
            }
        }
    }

    IEnumerator SlowMotion()
    {
        isSlowed = true;
        Time.timeScale = slowMotionFactor;
        yield return new WaitForSeconds(slowMotionDuration);
        Time.timeScale = 1f;
        isSlowed = false;
    }

    private void DisableGun(string scriptName)
    {
        Component[] scripts = GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script.GetType().Name == scriptName)
            {
                script.enabled = false;
                break;
            }
        }
    }
}
