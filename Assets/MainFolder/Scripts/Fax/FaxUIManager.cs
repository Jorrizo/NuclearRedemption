using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaxUIManager : MonoBehaviour
{
    public GameObject spawnPoint;
    public bool HasPlayed;


    [Header("Texts")]
    [Header("People")]
    public Text peopleToSave;
    public Text peopleIndoor;
    public Text peopleSaved;

    [Header("Central")]
    public Text valState;
    public Text valLife;


    // Start is called before the first frame update
    void Start()
    {
        HasPlayed = false;
        //GetComponent<Rigidbody>().AddForce(gameObject.transform.up * 1f, ForceMode.Impulse);

        valLife.text = "Vie de la centrale : " + (int)GameManager.instance.integriteGlobale;
        valState.text = "Etat de la centrale : " + GameManager.instance.type.ToString();

        peopleToSave.text = "Population à sauver : " + GameManager.instance.PopulationToSave.ToString();
        peopleIndoor.text = "Population restante : " + (int)GameManager.instance.PopulationIndoor;
        peopleSaved.text = "Population sauvée : " + (int)GameManager.instance.PopulationOutdoor;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Animator>())
        {
            if (HasPlayed)
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
