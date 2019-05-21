using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;

    [Header("Text")]
    public Text playPause;
    public Text tryAgain;
    public Text quit;

    [Header("Images")]
    public Image Background01;
    public Image Background02;
    public Image Background03;

    [Header("Boules")]
    public bool isPlaying = false;
    public bool isPaused = false;

    [Header("Index")]
    public int index = 0;


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

    public void Play()
    {
        isPlaying = true;
        playPause.text = "  Pause";
    }

    public void Pause ()
    {
        isPaused = true;
        playPause.text = "  Resume";
    }

    public void Resume()
    {
        playPause.text = "  Pause";
        //isPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Up()
    {
        if(index > 0)
        {
            index--;
        }
    }

    public void Down()
    {
        if(index < 2)
        { 
            index++;
        }
    }

    public void Selection()
    {
        switch (index)
        {
            case 0:
                if(isPlaying == false && isPaused == false)
                {
                    Play();
                }

                if (isPlaying == true && isPaused == false)
                {
                    Pause();
                }

                if (isPlaying == true && isPaused == true)
                {
                    Resume();
                }
                break;

            case 1:
                Restart();
                break;

            case 2:
                Quit();
                break;
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

        if (index == 2)
        {
            Background03.enabled = true;
        }
        else
        {
            Background03.enabled = false;
        }
    }
}
