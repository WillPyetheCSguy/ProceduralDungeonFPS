using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Collider bulletCollider;
    private int bulletDamage;
    public bool hitHead;
    WeaponController controller;


    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WeaponController>();
        bulletCollider = GetComponent<Collider>();
        Destroy(gameObject, 10f);
        hitHead = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.name == " Head")
            {
                hitHead = true;
                contact.otherCollider.gameObject.transform.parent.GetComponent<EnemyController>().TakeDamage(bulletDamage * 2);
                controller.PlayHitMarker();
                break;
            }
        }
        if (collision.gameObject.tag == "enemy" && hitHead == false)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(bulletDamage);
            controller.PlayHitMarker();
        }
        if(collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<BossController>().TakeDamage(bulletDamage);
            controller.PlayHitMarker();
        }
        hitHead = false;
        Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        bulletDamage = damage;
    }

}
