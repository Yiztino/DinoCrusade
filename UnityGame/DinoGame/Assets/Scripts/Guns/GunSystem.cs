using UnityEngine;
using TMPro;

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

    private void Awake()
    {
        bulletsleft = MagSize;
        readytoShoot = true;
    }

    private void Update()
    {
        MyInput();
        text.SetText(bulletsleft + "/" + MagSize);
    }

    private void MyInput()
    {
        if (AllowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsleft < MagSize && !reloading) Reload();

        if(readytoShoot&& shooting && !reloading && bulletsleft > 0)
        {
            bulletsshot = BulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readytoShoot = false;

        // Creamos el rayo en la dirección de la cámara
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RayHit, Range, WhatIsEnemy))
        {
            if (RayHit.collider.CompareTag("DinosaurBody"))
            {
                // Lógica para dañar al enemigo
                RayHit.collider.GetComponent<DinosaurHealth>().TakeDamage(Damage);
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
