using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

    public int startingAmmo = int.MaxValue;
    private float lastShot;
    private float lastReload;


    public override bool fire(Transform trans, GameObject bulletPrefab)
    {
        if (currAmmo != 0 && Time.time > lastReload + reloadTime && Time.time > lastShot+fireRate)
        {
            Vector3 start = trans.position + trans.forward*.1f;
            GameObject bullet = Instantiate(bulletPrefab, start, trans.rotation);
            bullet.transform.Rotate(0, 90, 90);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpeed * bullet.transform.up;
            bullet.GetComponent<BulletController>().SetDamage(bulletDamage);
            currAmmo--;
            lastShot = Time.time;
            return true;
        }
        return false;
    }

    public override bool reload()
    {
        if (currAmmo != magSize)
        {
            int ammoNeeded = magSize - currAmmo;
            int ammoLoaded = Mathf.Min(ammoNeeded, totalAmmo);
            currAmmo = ammoLoaded + currAmmo;
            totalAmmo -= ammoLoaded;
            lastReload = Time.time;
            return true;
        }
        return false;
        
    }

    public override void init()
    {
        isAuto = false;
        lastShot = Time.time;
        fireRate = .0f;
        magSize = 7;
        reloadTime = .5f;
        currAmmo = magSize;
        totalAmmo = startingAmmo;
        kickback = .1f;
        shot = Resources.Load<AudioClip>("Rifle_Shot");
        reloadS = Resources.Load<AudioClip>("Reload");
        empty = Resources.Load<AudioClip>("Empty_Clip");
        magSizeText = magSize.ToString();
        totalAmmoText = "\u221E";
        bulletDamage = 20;
    }
}
