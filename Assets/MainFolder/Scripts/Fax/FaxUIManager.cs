using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaxUIManager : MonoBehaviour
{
    public GameObject spawnPoint;
    public bool HasPlayedAnim;


    [Header("Texts")]
    [Header("People")]
    public Text txtWattObj;
    public Text txtWattProd;
    public Text txtWattSec;

    [Header("Central")]
    public Text valState;
    public Text valLife;


    // Start is called before the first frame update
    void Start()
    {
        HasPlayedAnim = false;
        //GetComponent<Rigidbody>().AddForce(gameObject.transform.up * 1f, ForceMode.Impulse);
        if (FindObjectOfType(typeof(GameManager)) != null)
        {
            valLife.text = "Vie de la centrale : " + (int)GameManager.instance.integriteGlobale;
            valState.text = "Etat de la centrale : " + GameManager.instance.type.ToString();

            txtWattObj.text = "Objectif de production : " + GameManager.instance.wattObjectif.ToString();
            txtWattProd.text = "Watt produit : " + (int)GameManager.instance.wattProduit + "W";
            txtWattSec.text = "Production par secondes : " + (int)GameManager.instance.wattProductionSeconde + " W/s";
        }
        else
        {
            valLife.text = "Vie de la centrale : " + (int)TutorielManager.instance.integriteGlobale;
            valState.text = "Etat de la centrale : " + TutorielManager.instance.type.ToString();

            txtWattObj.text = "Objectif de production : " + TutorielManager.instance.wattObjectif.ToString();
            txtWattProd.text = "Watt produit : " + (int)TutorielManager.instance.wattProduit + "W";
            txtWattSec.text = "Production par secondes : " + (int)TutorielManager.instance.wattProductionSeconde + " W/s";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Animator>() != null)
        {
            if (HasPlayedAnim)
            {
                DestroyTheAnimation();
            }
        }
    }

    public void DestroyTheAnimation()
    {
        Destroy(GetComponent<Animator>());
    }
}
