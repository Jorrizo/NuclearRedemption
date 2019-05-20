using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public int index = 0;
    public Text playPause;
    public Text tryAgain;
    public Text quit;
    public Image Background01;
    public Image Background02;
    public Image Background03;
    bool confirm = true;
    bool poz = false;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Selection();

    }

    void Selection()
    {

        if (index == 0)
        {
            Background01.enabled = true;
            

            if (confirm == true)
            {
                playPause.text = "  Pause";
                poz = true;
                confirm = false;

            }

            if (confirm == true && poz==true)
            {
                playPause.text = "  Resume";
                poz = false;
                confirm = false;


            }
        }
        else
        {
            Background01.enabled = false;
        }

        if (index == 1)
        {
            Background02.enabled = true;
        }
        else
        {
            Background02.enabled = false;
        }

        if (index == 2)
        {
            Background03.enabled = true;
        }
        else
        {
            Background03.enabled = false;
        }
    }

    void Quit()
    {
        Application.Quit();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Up()
    {
        if (index > 0)
        {
            index--;
        }
    }

    public void Down()
    {
        if (index < 2)
        {
            index++;
        }
    }

    public void Valider()
    {
        confirm = true;
        //confirm = false;
    }
}
