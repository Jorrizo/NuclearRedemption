using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManagerGameOver : MonoBehaviour
{
    public static UIManagerGameOver instance = null;

    [Header("Text")]

    [Header("Informations")]
    public Text nomCentrale;
    public Text date;
    public Text heure;
    public Text test;
    public Text numeroMission;

    [Header("Health")]
    public Text integriteVal;
    public Text markHealth;

    [Header("Tech")]
    public Text techAliveVal;
    public Text techDeadVal;
    public Text markTech;

    [Header("Production")]
    public Text prodObjVal;
    public Text prodProdVal;
    public Text markProd;

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
        integriteVal.text = "Intégrité de la centrale : " + (int)GameManager.instance.integriteGlobale;


        techAliveVal.text = "Techniciens en vie : " + (int)GameManager.instance.tekosSaved;
        techDeadVal.text = "Techniciens morts : " + (int)GameManager.instance.tekosDead;

        prodObjVal.text = "Objectif production : " + (int)GameManager.instance.wattObjectif;
        prodProdVal.text = "Production Watt : " + (int)GameManager.instance.wattProduit;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        //SteamVR_LoadLevel.Begin(SceneManager.GetActiveScene().buildIndex.ToString());
    }
    

    public void Quit()
    {
        Application.Quit();
    }
}
