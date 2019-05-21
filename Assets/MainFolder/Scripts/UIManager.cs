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
    public bool confirm = false;
    public bool poz = false;
    public int Stack = 0;



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
            //playPause.text = "  Play";

            if (confirm == true)
            {
                playPause.text = "  Pause";
                poz = true;
                Debug.Log("playing");
                confirm = false;

            }

            if (confirm == true && poz == true)
            {
                playPause.text = "  Resume";
                Debug.Log("resume");
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

            if (confirm == true)
            {
                Restart();
                confirm = false;
            }

        }
        else
        {
            Background02.enabled = false;
        }

        if (index == 2)
        {
            Background03.enabled = true;

            if(confirm == true)
            {
                Quit();
                Debug.Log("Quit");
                confirm = false;
            }


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
        if (index > 0 && Stack == 4)
        {
            index--;
        }
    }

    public void Down()
    {
        if (index < 2 && Stack == 4)
        {
            index++;
        }
    }

    public void Valider()
    {
        if (Stack == 4)
        {
            confirm = true;
            Debug.Log("ça confirme sec");
        }
    }

    public void Stacking()
    {
        if (Stack < 4)
        {
            Stack++;
        }
    }
}
