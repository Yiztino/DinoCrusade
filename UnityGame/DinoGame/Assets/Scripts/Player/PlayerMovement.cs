using System.Collections;
using UnityEngine;

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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        if ((Input.GetButtonDown("Jump") && isGrounded)) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dinosaur"))
        {
            speed = 0f;
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
