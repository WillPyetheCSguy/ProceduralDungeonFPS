using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorDetection : MonoBehaviour
{
    public bool doorLeft;
    public bool doorRight;
    public bool doorUp;
    public bool doorDown;
    public bool currentPlane;
    public GameObject doorLeftChild;
    public GameObject doorRightChild;
    public GameObject doorUpChild;
    public GameObject doorDownChild;
    public enemymanager enemyManager;
    public int numenem;
    public GameObject coll;

    void OnTriggerEnter(Collider collision)
    {
        if (numenem == 0)
        {
            if (collision.gameObject.tag != "unpassable")
                {
           
                if (collision.gameObject.tag == "doorLeft")
                {
                    Destroy(collision.gameObject);
                    doorLeft = true;
                    //Debug.Log("doorLeft hit!!");
                }
                if (collision.gameObject.tag == "doorUp")
                {
                    Destroy(collision.gameObject);
                    doorUp = true;
                    //Debug.Log("doorUp hit!!");
                }
                if (collision.gameObject.tag == "doorRight")
                {
                    Destroy(collision.gameObject);
                    doorRight = true;
                    //Debug.Log("doorRight hit!!");
                }
                if (collision.gameObject.tag == "doorDown")
                {
                    Destroy(collision.gameObject);
                    doorDown = true;
                    //Debug.Log("doorDown hit!!");
                }
            }
        }
       // Debug.Log(collision.gameObject.tag);
       // collision.gameObject.SetActive(false);
           
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    public void SetBoolsFalse()
    {
        doorLeft = false;
        doorRight = false;
        doorUp = false;
        doorDown = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        doorLeft = false;
        doorRight = false;
        doorUp = false;
        doorDown = false;
        enemyManager = GameObject.Find("enemyManager").GetComponent<enemymanager>();
        numenem = 0;
      

    }
    private void Update()
    {
        numenem = enemyManager.numEnemies();
    }

}
