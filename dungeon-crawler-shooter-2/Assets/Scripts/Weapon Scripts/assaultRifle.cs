using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assaultRifle : Weapon {

    public int startingAmmo = 50;
    private float lastShot;
    private float lastReload;

    public override bool fire(Transform trans, GameObject bulletPrefab)
    {
        if(currAmmo != 0 && Time.time > lastReload+reloadTime && Time.time > lastShot + fireRate)
        {
            Vector3 start = trans.position + trans.forward*.1f;
            GameObject bullet = Instantiate(bulletPrefab, start, trans.rotation);
            bullet.transform.Rotate(0, 90, 90);
            lastShot = Time.time;
            bullet.GetComponent<Rigidbody>().velocity = bulletSpeed * bullet.transform.up;
            bullet.GetComponent<BulletController>().SetDamage(bulletDamage);
            currAmmo--;
            return true;
        }
        return false;
    }

    public override bool reload()
    {
        if(totalAmmo == 0)
        {
            return false;
        }
        if(currAmmo != magSize)
        {
            int ammoNeeded = magSize - currAmmo;
            int ammoLoaded = Mathf.Min(ammoNeeded, totalAmmo);
            currAmmo = ammoLoaded + currAmmo;
            totalAmmo -= ammoLoaded;
            lastReload = Time.time;
            totalAmmoText = totalAmmo.ToString();
            return true;
        }
        return false;
        
    }

    public override void init()
    {
        isAuto = true;
        lastShot = Time.time;
        fireRate = .1f;
        magSize = 28;
        reloadTime = .5f;
        currAmmo = magSize;
        totalAmmo = startingAmmo;
        kickback = .2f;
        shot = Resources.Load<AudioClip>("Rifle_Shot");
        reloadS = Resources.Load<AudioClip>("Reload");
        empty = Resources.Load<AudioClip>("Empty_Clip");
        magSizeText = magSize.ToString();
        totalAmmoText = startingAmmo.ToString();
        bulletDamage = 34;
    }

    

}
