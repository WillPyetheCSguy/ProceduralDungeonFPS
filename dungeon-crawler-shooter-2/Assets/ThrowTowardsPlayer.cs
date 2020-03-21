using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTowardsPlayer : MonoBehaviour
{
    private BossController bigBoi;
    public float timer;
    public GameObject Player;
    public float rockSpeed;
    public AudioClip hitsound;
    public AudioSource rocksource;
    public bool isHitBullet;
    private GameObject arAmmoBox;

    // Start is called before the first frame update
    void Start()
    {
        bigBoi = GameObject.Find("zombie").GetComponent<BossController>();
        arAmmoBox = Resources.Load<GameObject>("AmmoBox");
        isHitBullet = false;
        rockSpeed = 8f;
        Player = GameObject.FindGameObjectWithTag("Player");
        timer = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "bullet")
        {
            isHitBullet = true;
        }
        if(collision.gameObject.name == "zombie" && isHitBullet)
        {
            bigBoi.TakeDamage(500);
            Instantiate(arAmmoBox, new Vector3(0, 0.25f, 0), Quaternion.identity);
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<MeshRenderer>().enabled = false;
            rocksource.clip = hitsound;
            rocksource.Play();

            Destroy(this.gameObject, 2);
        }
        if(collision.gameObject.name != "zombie" && collision.gameObject.tag != "bullet" && collision.gameObject.tag != "Boss")
        {
            if(collision.gameObject.tag == "Player")
            {
                Player.GetComponent<PlayerHealthManager>().GiveDamage(40);
            }
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<MeshRenderer>().enabled = false;
            rocksource.clip = hitsound;
            rocksource.Play();

            Destroy(this.gameObject,2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.LookAt(Player.transform.position);
        Vector3 playerPosition = new Vector3(Player.transform.position.x, 1, Player.transform.position.z);
        if (timer > 1)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * rockSpeed);
        }
    }
}
