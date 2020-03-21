using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private int enemyHealth;
    destroythis startFollow;
    public GameObject Player;
    private Animator enemyAnim;
    public float speed;
    public float dieTimer;
    public waypointManager waypoints;

    private GameObject arAmmoBox;
    private GameObject gAmmoBox;
    private GameObject healthBox;
    public GameObject[] enemies;
    public float randomSway;
    private IEnumerator coroutine;
    public bool startAttack;
    roomManagement roomManager;
    public AudioClip hitsound;
    public AudioClip deathsound;
    public AudioSource zombiesource;
    public bool playdeathnow;
    public bool isDead;




    // Start is called before the first frame update
    void Start()
    {

        isDead = false;
        playdeathnow = true;
        zombiesource = GetComponent<AudioSource>();
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        waypoints = GameObject.Find("RoomManager").GetComponent<waypointManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        startFollow = Player.GetComponent<destroythis>();
        enemyAnim = GetComponent<Animator>();
        enemyAnim.SetBool("walking", true);
        enemyAnim.SetBool("attacking", false);
        enemyAnim.SetBool("dying", false);
        startAttack = true;
        arAmmoBox = Resources.Load<GameObject>("AmmoBox");
        gAmmoBox = Resources.Load<GameObject>("AmmoBox");
        healthBox = Resources.Load<GameObject>("HealthBox");
        randomSway = Random.Range(-2f, 2f);
        enemyHealth = 150;
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<roomManagement>();
        speed = Random.Range(2,(roomManager.NumRooms()/2.5f)+2);
        coroutine = damagePlayer(25);
        Player.GetComponent<AudioSource>().clip = deathsound;


    }

   

    public void FollowPlayer()
    {
        enemyAnim.SetBool("walking", true);
        enemyAnim.SetBool("attacking", false);
     
    }
    public void AttackPlayer()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, 0f, Player.transform.position.z));
        enemyAnim.SetBool("walking", false);
        enemyAnim.SetBool("attacking", true);
        
    }
    public void enemyAI()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) > 2.5)
        {
           
            startAttack = true;
            FollowPlayer();
            StopCoroutine(coroutine);
        }
        else
        {
          
            AttackPlayer();
            if(startAttack)
                StartCoroutine(coroutine);
        }
    }
    IEnumerator damagePlayer(int damage)
    {
        
        while (true)
        {
                startAttack = false;
                Player.GetComponent<PlayerHealthManager>().GiveDamage(damage);
                zombiesource.clip = hitsound;
                zombiesource.Play();
                yield return new WaitForSeconds(2);
                startAttack = true;
        }

    }
    public void playdeath()
    {
        if (playdeathnow)
        {
            Player.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(deathsound);
            
        }
        playdeathnow = false;
    }

   
    public void EnemyManager()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        
        if (enemyHealth <= 0)
        {
            GetComponent<Collider>().enabled = false;
            isDead = true;
            playdeath();
            enemyAnim.SetBool("dying", true);
            enemyAnim.SetBool("walking", false);
            enemyAnim.SetBool("attacking", false);
           
            dieTimer += Time.deltaTime;
            if (dieTimer > 3)
            {
                
                float spawnChance = 30;
                float rand = Random.Range(0, 100);
                if (rand < spawnChance)
                {
                    if (rand < 10)
                    {
                        Instantiate(arAmmoBox, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                    }
                    else if (rand < 20)
                    {
                        Instantiate(gAmmoBox, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(healthBox, transform.position + new Vector3(0,0.10f,0), Quaternion.identity);
                    }
                }
                Destroy(gameObject);
            }
        }
        else
        {
            enemyAI();
        }
    }


    // Update is called once per frame
    void Update()
    {
        EnemyManager();
       
    }

    public void TakeDamage(int damage)
    {
        //To Do add Hit sound and possiblity to drop item
        enemyHealth -= damage;
    }

}
