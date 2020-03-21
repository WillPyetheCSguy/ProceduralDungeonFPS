using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroythis : MonoBehaviour
{
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "entrance")
        {
            Destroy(collision.gameObject);
            start = true;
        }
            
        //transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
