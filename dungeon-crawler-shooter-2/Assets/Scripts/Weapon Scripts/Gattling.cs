using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gattling : Weapon {

    public int startingAmmo = 50;
    private float lastShot;
    private float timetoSpinDown = .75f;
    private float startingFireRate = .3f;


    public override bool fire(Transform trans, GameObject bulletPrefab)
    {
        if(lastShot + timetoSpinDown < Time.time)
        {
            fireRate = startingFireRate;
        }
        if (currAmmo != 0 && Time.time > lastShot + fireRate)
        {
            Vector3 start = trans.position + trans.forward*.1f;
            GameObject bullet = Instantiate(bulletPrefab, start, trans.rotation);
            bullet.transform.Rotate(0, 90, 90);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpeed * bullet.transform.up;
            bullet.GetComponent<BulletController>().SetDamage(bulletDamage);
            lastShot = Time.time;
            currAmmo--;
            totalAmmo--;
            totalAmmoText = currAmmo.ToString();
            fireRate = Mathf.Max(.12f,fireRate-.02f);
            return true;
        }
        return false;
    }

    public override bool reload()
    {
        return false;
    }

    public override void init()
    {
        isAuto = true;
        lastShot = Time.time;
        fireRate = .3f;
        magSize = int.MaxValue;
        reloadTime = 0;
        currAmmo = startingAmmo;
        totalAmmo = startingAmmo;
        kickback = 10f;
        shot = Resources.Load<AudioClip>("Rifle_Shot");
        reloadS = Resources.Load<AudioClip>("Reload");
        empty = Resources.Load<AudioClip>("Empty_Clip");
        magSizeText = "\u221E";
        totalAmmoText = startingAmmo.ToString();
        bulletDamage = 34;
    }

	
}
