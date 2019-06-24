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

    [Header("%")]
    public int Tech;
    public int Health;
    public int Prod;

    public string sTech;
    public string sHealth;
    public string sProd;

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
        Pourcentages();
        integriteVal.text = "Intégrité de la centrale : " + (int)GameManager.instance.integriteGlobale;
        //markHealth.text = Health + ("%");
        markHealth.text = sHealth;


        techAliveVal.text = "Techniciens en vie : " + (int)GameManager.instance.tekosSaved;
        techDeadVal.text = "Techniciens morts : " + (int)GameManager.instance.tekosDead;
        //markTech.text = Tech + ("%");
        markTech.text = sTech;

        prodObjVal.text = "Objectif production : " + (int)GameManager.instance.wattObjectif;
        prodProdVal.text = "Production Watt : " + (int)GameManager.instance.wattProduit;
        //markProd.text = Prod + ("%");
        markProd.text = sProd;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pourcentages()
    {
        Health = ((100 * (int)GameManager.instance.integriteGlobale) / 1000);

        if ((int)GameManager.instance.extraTekos != 0)
        {
            Tech = ((100 * (int)GameManager.instance.tekosSaved) / (int)GameManager.instance.extraTekos);
        }

        if ((int)GameManager.instance.extraTekos == 0)
        {
            sTech = "D";
        }

        Prod = ((100 * (int)GameManager.instance.wattProduit) / (int)GameManager.instance.wattObjectif);

        if(Health > 91)
        {
            sHealth = "A";
        }
        else if (Health > 51 && Health < 90)
        {
            sHealth = "B";
        }
        else if (Health > 26 && Health < 50)
        {
            sHealth = "C";
        }
        else if (Health >= 0 && Health < 25)
        {
            sHealth = "D";
        }

        if (Tech > 91)
        {
            sTech = "A";
        }
        else if (Tech > 51 && Tech < 90)
        {
            sTech = "B";
        }
        else if (Tech > 26 && Tech < 50)
        {
            sTech = "C";
        }
        else if (Tech >= 0 && Tech < 25)
        {
            sTech = "D";
        }

        if (Prod> 91)
        {
            sProd = "A";
        }
        else if (Prod > 51 && Prod < 90)
        {
            sProd = "B";
        }
        else if (Prod > 26 && Prod < 50)
        {
            sProd = "C";
        }
        else if (Prod >= 0 && Prod < 25)
        {
            sProd = "D";
        }
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
