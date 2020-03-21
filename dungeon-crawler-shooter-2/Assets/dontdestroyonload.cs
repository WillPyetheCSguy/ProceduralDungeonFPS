using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontdestroyonload : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip Bossclip;
    public AudioClip Mainclip;
    public AudioClip Menuclip;
    public string activeScene;
    private static dontdestroyonload instance;
        private void Awake()
        {
            // if the singleton hasn't been initialized yet
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;//Avoid doing anything else
            }

            instance = this;
        
            DontDestroyOnLoad(this.gameObject);
        }
  
    // Start is called before the first frame update
    void Start()
    {
        activeScene = "";
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "WillTest" && !activeScene.Equals(SceneManager.GetActiveScene().name))
        {
            audio.clip = Mainclip;
            audio.Play();
            activeScene = SceneManager.GetActiveScene().name;
        }

        else if(SceneManager.GetActiveScene().name == "bossfight" && !activeScene.Equals(SceneManager.GetActiveScene().name))
        {
            audio.clip = Bossclip;
            audio.Play();
            activeScene = SceneManager.GetActiveScene().name;
        }
        else if (!activeScene.Equals(SceneManager.GetActiveScene().name))
        {
            audio.clip = Menuclip;
            audio.Play();
            activeScene = SceneManager.GetActiveScene().name;
        }
        

    }
}
