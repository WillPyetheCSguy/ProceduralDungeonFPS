using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuHandler : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PauseMenu.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("WillTest");
    }
    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
    public void PauseGame()
    {
        if (Player != null)
        {
            PauseMenu.SetActive(true);
            Player.GetComponent<FirstPersonController>().enabled = false;
            Player.transform.GetChild(0).GetComponent<WeaponController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        
 
    }
    public void UnPauseGame()
    {
        if (Player != null)
        {
            PauseMenu.SetActive(false);
            Player.GetComponent<FirstPersonController>().enabled = true;
            Player.transform.GetChild(0).GetComponent<WeaponController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        
    }
    public void Credits()
    {
        SceneManager.LoadScene("credits");
    }
    public void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (PauseMenu.gameObject.activeInHierarchy == false)
        {
            Time.timeScale = 1;
        }
    }
}
