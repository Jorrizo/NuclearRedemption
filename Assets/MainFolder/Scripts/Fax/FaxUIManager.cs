using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaxUIManager : MonoBehaviour
{

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
        GetComponent<Rigidbody>().AddForce(Vector3.forward * 5f, ForceMode.Impulse);

        valLife.text = "Etat de la centrale : " + GameManager.instance.integriteGlobale.ToString();
        valState.text = "Vie de la centrale : " + GameManager.instance.type.ToString();

        peopleToSave.text = "Population à sauver : " + GameManager.instance.PopulationToSave.ToString();
        peopleIndoor.text = "Population restante : " + GameManager.instance.PopulationIndoor.ToString();
        peopleSaved.text = "Population sauvée : " + GameManager.instance.PopulationOutdoor.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
