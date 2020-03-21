using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
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
    public GameObject lazer2;


    public GameObject[] ways;
    public bool way1;
    public bool activateWarning;

    // Start is called before the first frame update
    void Start()
    {
        warning = GameObject.Find("BehindYOu").GetComponent<Text>();
        warning.gameObject.SetActive(false);
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
        enemyHealth = 5000;
        laserAttack = 1800;
        Player.GetComponent<AudioSource>().clip = deathsound;
    }

    public void instantiateRock()
    {
        if (fireRocks)
        {
            Vector3 start = new Vector3(transform.position.x, 6, transform.position.z);
            GameObject rock = Instantiate(rockPrefab, start, transform.rotation);
        }
       
    }
    
    public void InstantiateLazerZombs()
    {
        
        
        lazer1.SetActive(true);
        lazer2.SetActive(true);

    }
    public void patrol()
    {
        if (way1)
        {
            transform.position = Vector3.MoveTowards(transform.position, ways[0].transform.position, Time.deltaTime * 2);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, ways[1].transform.position, Time.deltaTime * 2);
        }
        
    }

    IEnumerator ZombieThrowing(int seconds)
    {
        while (true) {
            instantiateRock();
            yield return new WaitForSeconds(seconds);
        }
    }
    public void playdeath()
    {
        Player.GetComponent<AudioSource>().PlayOneShot(deathsound);
    }

    public void EnemyManager()
    {
        if (enemyHealth <= 0)
        {
            playdeath();
            Destroy(gameObject);
        }
        patrol();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "way")
        {
            way1 = !way1;
        }
    }


    IEnumerator displaywarning()
    {
        warning.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        warning.gameObject.SetActive(false);
        fireDaLazers = false;

    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, 0f, Player.transform.position.z));
        EnemyManager();
        if (enemyHealth <= laserAttack)
        {
           

            if (fireDaLazers)
            {
                StartCoroutine(displaywarning());
                InstantiateLazerZombs();
                
            }
        }
        if (enemyHealth <= 0)
        {
            SceneManager.LoadScene("WinScene");
        }

    }

    public void TakeDamage(int damage)
    {
        
        enemyHealth -= damage;
        Debug.Log(enemyHealth);
    }
}
