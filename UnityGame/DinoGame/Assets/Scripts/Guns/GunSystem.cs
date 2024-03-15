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

    public GameObject Flash, Bullethole;
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
        float x = Random.Range(-Spread, Spread);
        float y = Random.Range(-Spread, Spread);
        Vector3 direction = Cam.transform.position + new Vector3(x,y,0);


        if (Physics.Raycast(Cam.transform.position, direction, out RayHit, Range, WhatIsEnemy))
        {
            Debug.Log(RayHit.collider.name);
            if (RayHit.collider.CompareTag("Enemy"))
            {
                //RayHit.collider.GetComponent<Enemy>.TakeDamage(damage);
                Debug.Log("CU CU PUMASSS");
            }
        }

        //Instantiate(Bullethole, RayHit.point, Quaternion.Euler(0, 180, 0));
        GameObject MuzzleFlash = Instantiate(Flash, AttackPoint.position, Quaternion.identity);
        Destroy(MuzzleFlash, .5f);
        bulletsleft--;
        bulletsshot--;
        Invoke("ResetShot", TimeBetweenShooting);

        if(bulletsshot > 0 && bulletsleft > 0)
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
