using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int currentHp;
    public int maxHp;
    public float hpBarLength;
    private GUIStyle currentStyle = null;
    // Use this for initialization
    void Start()
    {
        maxHp = 100;
        currentHp = maxHp;
        hpBarLength = Screen.width / 2;    
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHp(0);
    }


    void OnGUI()
    {
        InitStyles();
        GUI.Box(new Rect(10, 10, hpBarLength, 25), currentHp + "/" + maxHp, currentStyle);


    }
    private void InitStyles()
    {
        if (currentStyle == null)
        {
            currentStyle = new GUIStyle(GUI.skin.box);
            //currentStyle.normal.background = ;
        }
    }
    public void ChangeHp(int hp)
    {
        currentHp += hp;
        hpBarLength = (Screen.width / 2) * (currentHp / (float)maxHp);
        if (currentHp <= 0)
        {
            //Kill();
        }
    }
   // void Kill()
   // {
        //Destroy(this.gameObject);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
  //  }
}