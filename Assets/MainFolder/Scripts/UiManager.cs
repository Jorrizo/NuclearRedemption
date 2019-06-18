using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;

    [Header("Text")]
    public Text tryAgain;
    public Text quit;
    public Text contrat;

    [Header("Images")]
    public Image Background00;
    public Image Background01;
    public Image Background02;

    [Header("Int")]
    public int index = 0;
    public int stack = 0;
    public int indexContrat = 0;

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
        Print();
        indexContrat = 0;
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
        if(index > 0 && stack == 4)
        {
            index--;
        }
    }

    public void Down()
    {
        if(index < 2 && stack == 4)
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
                    Print();
                    break;

                case 1:
                    Restart();
                    break;

                case 2:
                    Quit();
                    break;
            }
        }
    }

    public void GreenHighlight()
    {
        if (index == 0)
        {
            Background00.enabled = true;
        }
        else
        {
            Background00.enabled = false;
        }

        if (index == 1)
        {
            Background01.enabled = true;
        }
        else
        {
            Background01.enabled = false;
        }

        if (index == 2)
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

    public void Print()
    {
        if (!PlayerPrefs.HasKey("AddvalueObjectif"))
        {
            PlayerPrefs.SetFloat("AddvalueObjectif", 0f);
        }

        switch (indexContrat)
        {
            case 0:
                GameManager.instance.wattObjectif = 2000 + PlayerPrefs.GetFloat("AddvalueObjectif");
                FaxSpawnManager.instance.SpawnContrat();
                break;

            case 1:
                GameManager.instance.wattObjectif = 3000 + PlayerPrefs.GetFloat("AddvalueObjectif");
                FaxSpawnManager.instance.SpawnContrat();
                break;

            case 2:
                GameManager.instance.wattObjectif = 4000 + PlayerPrefs.GetFloat("AddvalueObjectif");
                FaxSpawnManager.instance.SpawnContrat();
                break;

            default:
                break;
        }
    }

    public void PrintPlus()
    {
        if(index == 0 && stack == 4)
        {
            if (indexContrat <= 2)
            {
                indexContrat++;
            }
            else
            {
                indexContrat = 0;
            }
        }
    }
}
