using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour
{
    public int Damage, MagSize, BulletsPerTap;
    public float TimeBetweenShooting, Spread, Range, ReloadTime, TimeBetweenShots;
    public bool AllowButtonHold;
    int bulletsleft, bulletsshot;

    bool shooting, readytoShoot, reloading;

    public Camera Cam;
    public Transform AttackPoint;
    public RaycastHit RayHit;
    public LayerMask WhatIsEnemy;

    public GameObject Flash;
    public TextMeshProUGUI text;

    public AudioClip shootingSound;
    private AudioSource audioSource;

    private void Awake()
    {
        bulletsleft = MagSize;
        readytoShoot = true;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing from the GameObject.");
        }
    }

    private void Update()
    {
        MyInput();
        text.SetText(bulletsleft + "/" + MagSize);
    }

    private void MyInput()
    {
        if (AllowButtonHold)
            shooting = Mouse.current.leftButton.isPressed || Gamepad.current.rightTrigger.isPressed;
        else
            shooting = Mouse.current.leftButton.wasPressedThisFrame || Gamepad.current.rightTrigger.wasPressedThisFrame;

        if (Keyboard.current.rKey.wasPressedThisFrame || Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            if (bulletsleft < MagSize && !reloading)
                Reload();
        }

        if (readytoShoot && shooting && !reloading && bulletsleft > 0)
        {
            bulletsshot = BulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readytoShoot = false;

        // Play shooting sound
        if (shootingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootingSound);
            Debug.Log("Playing shooting sound.");
        }
        else
        {
            Debug.LogWarning("Shooting sound or AudioSource is not set.");
        }

        Ray ray = Cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RayHit, Range, WhatIsEnemy))
        {
            if (RayHit.collider.CompareTag("DinosaurBody"))
            {
                RayHit.collider.GetComponent<DinoAI>().TakeDamage(Damage);
                Debug.Log("Hit");
            }
        }

        GameObject MuzzleFlash = Instantiate(Flash, AttackPoint.position, Quaternion.identity);
        Destroy(MuzzleFlash, .2f);

        bulletsleft--;
        bulletsshot--;
        Invoke("ResetShot", TimeBetweenShooting);

        if (bulletsshot > 0 && bulletsleft > 0)
            Invoke("Shoot", TimeBetweenShots);
    }

    private void ResetShot()
    {
        readytoShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", ReloadTime);
    }

    private void ReloadFinished()
    {
        bulletsleft = MagSize;
        reloading = false;
    }
}
