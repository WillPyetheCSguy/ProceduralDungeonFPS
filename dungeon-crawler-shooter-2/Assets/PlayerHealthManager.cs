using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public int PlayerHealth = 100;
    public Text health;
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("health").GetComponent<Text>();
        
    }
    public void GiveDamage(int damage)
    {
        //To Do add Hit sound and possiblity to drop item
        PlayerHealth -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth <= 0)
        {
            SceneManager.LoadScene("lose");
        }
        health.text = "Health: " + PlayerHealth + "/100";
    }
}
