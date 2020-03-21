using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public float fireRate;
    public int magSize;
    public float reloadTime;
    public int currAmmo;
    public int totalAmmo;
    public bool isAuto;
    public float kickback;
    public float bulletSpeed = 90;
    public AudioClip shot;
    public AudioClip reloadS;
    public AudioClip empty;
    public string magSizeText;
    public string totalAmmoText;
    public int bulletDamage;

    public abstract bool reload();

    public abstract bool fire(Transform trans, GameObject bulletPrefab);

    public abstract void init();

}
