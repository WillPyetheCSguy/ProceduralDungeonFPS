using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDatGun : MonoBehaviour {

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public GameObject playerCam;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 start = transform.position + transform.forward;
        GameObject bullet = Instantiate(bulletPrefab, start, transform.rotation);
        bullet.transform.Rotate(0, 90, 90);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpeed * bullet.transform.up;
    }
}
