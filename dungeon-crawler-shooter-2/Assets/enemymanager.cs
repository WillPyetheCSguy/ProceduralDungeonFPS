using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public int numEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        int i = 0;
        foreach (GameObject x in enemies)
        {
            i++;
        }
        return i;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
