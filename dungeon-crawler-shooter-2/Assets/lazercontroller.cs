using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lazercontroller : MonoBehaviour
{
    public int enemyHealth;
    destroythis startFollow;
    public GameObject Player;
    public Text warning;
    public GameObject[] enemies;
    public float randomSway;
    private IEnumerator coroutine;
    public bool startAttack;

    public GameObject rockPrefab;
    public float rockSpeed;

    public AudioClip deathsound;
    public AudioSource zombiesource;
    public int laserAttack;
    public bool playdeathnow;

    public bool fireDaLazers;
    public bool fireRocks;
    public GameObject lazer1;


    public GameObject[] ways;
    public bool way1;
    public bool activateWarning;

    public void instantiateRock()
    {
        if (fireRocks)
        {
            Vector3 start = new Vector3(transform.position.x, 4, transform.position.z);
            GameObject rock = Instantiate(rockPrefab, start, transform.rotation);
        }

    }
    IEnumerator ZombieThrowing(int seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            instantiateRock();
            yield return new WaitForSeconds(seconds);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
  
        ways = GameObject.FindGameObjectsWithTag("way");
        way1 = false;
        fireRocks = true;
        fireDaLazers = true;
        activateWarning = false;
        StartCoroutine(ZombieThrowing(3));
        playdeathnow = true;
        zombiesource = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        startAttack = true;
        Player.GetComponent<AudioSource>().clip = deathsound;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, 0f, Player.transform.position.z));
        fireRocks = true;
    }
}
