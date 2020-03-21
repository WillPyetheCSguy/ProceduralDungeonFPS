using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponController : MonoBehaviour {

    private Weapon gattlingGun;
    private Weapon pistol;
    private Weapon assaultrifle;
    public GameObject gattlingGunPrefab;
    public GameObject pistolPrefab;
    public GameObject assaultRiflePrefab;
    public GameObject bulletPrefab;

    public GameObject arUpText;
    public GameObject gUpText;
    public GameObject hUpText;

    private float textTimer;

    //Health and Ammo variables
    private int ammoInPack = 25;
    private int healthInPack = 25;
    private PlayerHealthManager hpManager;

    private Weapon activeWeapon;
    private GameObject activeWeaponPrefab;
    private int currWeapon;
    private int numWeapons = 3;

    private AudioSource audiosrc;
    public AudioClip hitmarker;

    public Text ammoStats;
    public Text totalAmmo;

    public int health;
    public float playerPickupRadius = 4f;

	// Use this for initialization
	void Start () {
        arUpText.SetActive(false);
        gUpText.SetActive(false);
        hUpText.SetActive(false);
        textTimer = Time.time;
        //Initialize Weapons
        audiosrc = GetComponent<AudioSource>();
        gattlingGun = new Gattling();
        pistol = new Pistol();
        assaultrifle = new assaultRifle();
        hpManager = transform.parent.GetComponent<PlayerHealthManager>();

        activeWeapon = gattlingGun;
        activeWeaponPrefab = gattlingGunPrefab;
        pistolPrefab.SetActive(false);
        assaultRiflePrefab.SetActive(false);
        gattlingGun.init();
        pistol.init();
        assaultrifle.init();
        currWeapon = 0;

        //Initialize UI
        ammoStats.text = activeWeapon.currAmmo + " / " + activeWeapon.magSize;
        totalAmmo.text = "Total Ammo: " + activeWeapon.totalAmmo;
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time > textTimer + 2)
        {
            arUpText.SetActive(false);
            gUpText.SetActive(false);
            hUpText.SetActive(false);
        }
        //Weapon Firing
        if (activeWeapon.isAuto)
        {
            if (Input.GetMouseButton(0))
            {
                bool fired = activeWeapon.fire(transform, bulletPrefab);
                if (fired)
                {
                    audiosrc.clip = activeWeapon.shot;
                    audiosrc.Play();
                    activeWeaponPrefab.GetComponent<Animation>().Play();
                }
                if(activeWeapon.currAmmo == 0)
                {
                    audiosrc.clip = activeWeapon.empty;
                    audiosrc.Play();
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                bool fired = activeWeapon.fire(transform, bulletPrefab);
                if (fired)
                {
                    audiosrc.clip = activeWeapon.shot;
                    audiosrc.Play();
                    activeWeaponPrefab.GetComponent<Animation>().Play();
                }
                else
                {
                    audiosrc.clip = activeWeapon.empty;
                    audiosrc.Play();
                }
            }
        }
        

        //Weapon Reload
        if (Input.GetKeyDown("r"))
        {
            bool reloaded = activeWeapon.reload();
            if (reloaded)
            {
                audiosrc.clip = activeWeapon.reloadS;
                audiosrc.Play();
            }
        }

        //Weapon Change
        var wheelMovement = Input.GetAxis("Mouse ScrollWheel");
        if(wheelMovement != 0){
            UpdateCurrentWeapon(wheelMovement);
        }

        
        

        //Check for nearby health or ammo items
        Collider[] nearby = Physics.OverlapSphere(transform.position, playerPickupRadius);
        if(nearby.Length != 0)
        {
            foreach(Collider col in nearby)
            {
                if(col.tag == "health")
                {
                    if(hpManager.PlayerHealth != 100)
                    {
                        hUpText.SetActive(true);
                        textTimer = Time.time;
                        hpManager.PlayerHealth = Mathf.Min(hpManager.PlayerHealth + healthInPack, 100);
                        Destroy(col.gameObject);
                    }
                }
                if(col.tag == "ammo")
                {
                    float q = Random.Range(0, 10);
                    if (q < 5)
                    {
                        arUpText.SetActive(false);
                        gUpText.SetActive(false);
                        hUpText.SetActive(false);
                        arUpText.SetActive(true);
                        textTimer = Time.time;
                        assaultrifle.totalAmmo += ammoInPack;
                    }
                    else
                    {
                        arUpText.SetActive(false);
                        gUpText.SetActive(false);
                        hUpText.SetActive(false);
                        gUpText.SetActive(true);
                        textTimer = Time.time;
                        gattlingGun.totalAmmo += ammoInPack;
                        gattlingGun.currAmmo += ammoInPack;
                    }
                    Destroy(col.gameObject);
                }
            }
        }
        UpdateText();
    }

    void UpdateCurrentWeapon(float change)
    {
        int delta = 0;
        if (change < 0) delta = 2;
        if (change > 0) delta = 1;
        currWeapon = Mathf.Abs((currWeapon+delta) % numWeapons);
        switch (currWeapon)
        {
            case 0:
                activeWeapon = gattlingGun;
                activeWeaponPrefab = gattlingGunPrefab;
                gattlingGunPrefab.SetActive(true);
                pistolPrefab.SetActive(false);
                assaultRiflePrefab.SetActive(false);
                break;
            case 1:
                activeWeapon = pistol;
                activeWeaponPrefab = pistolPrefab;
                gattlingGunPrefab.SetActive(false);
                pistolPrefab.SetActive(true);
                assaultRiflePrefab.SetActive(false);
                break;
            case 2:
                activeWeapon = assaultrifle;
                activeWeaponPrefab = assaultRiflePrefab;
                gattlingGunPrefab.SetActive(false);
                pistolPrefab.SetActive(false);
                assaultRiflePrefab.SetActive(true);
                break;
        }
    }

    public void UpdateText()
    {
        gattlingGun.totalAmmoText = gattlingGun.totalAmmo.ToString();
        assaultrifle.totalAmmoText = assaultrifle.totalAmmo.ToString();
        ammoStats.text = activeWeapon.currAmmo + " / " + activeWeapon.magSizeText;
        totalAmmo.text = "Total Ammo: " + activeWeapon.totalAmmoText;
    }

    public void PlayHitMarker()
    {
        audiosrc.PlayOneShot(hitmarker);
    }
}
