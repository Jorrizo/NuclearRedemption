using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManagerIG : MonoBehaviour
{
    public static UIManagerIG instance = null;

    [Header("Text")]
    public Text tryAgain;
    public Text quit;

    [Header("Images")]
    public Image Background01;
    public Image Background02;

    [Header("Int")]
    public int index = 0;
    public int stack = 0;

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
        GreenHighlight();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SteamVR_LoadLevel.Begin(SceneManager.GetActiveScene().buildIndex.ToString());
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void Up()
    {
        if (index > 0 && stack == 4)
        {
            index--;
        }
    }

    public void Down()
    {
        if (index < 1 && stack == 4)
        {
            index++;
        }
    }

    public void Selection()
    {
        if (stack == 4)
        {
            switch (index)
            {
                case 0:
                    Restart();
                    break;

                case 1:
                    Quit();
                    break;
            }
        }
    }

    public void GreenHighlight()
    {
        if (index == 0)
        {
            Background01.enabled = true;
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
    }

    public void Stacking()
    {
        if (stack < 4)
        {
            stack++;
        }
    }
}
