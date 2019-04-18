using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK; //Accès à la catégorie VTRK

public class LiaisonManager : MonoBehaviour
{
    public ModuleState Starting;
    public ModuleState Ending;
    private ModuleState ValueTemp;

    public GameObject CurrentFusible;


    public bool IsLiaisonValid = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Invertion()
    {
        ValueTemp = Starting;
        Starting = Ending;
        Ending = ValueTemp;
    }


    public void SnappedObject () //Stoque le GameObject sur la snapzone
    {
        CurrentFusible = GetComponentInChildren<VRTK_SnapDropZone>().GetCurrentSnappedObject();
    }

    public void CheckLiaison () 
    {
      // if (!Starting.Stable)// Si il n'est pas stable
      //{
        if (CurrentFusible.CompareTag("Surchauffe")) //Surchauffe
        {
            Debug.Log("Surchauffe");
        }
        else if (CurrentFusible.CompareTag("Surcharge")) //Surcharge
        {
            Debug.Log("Surcharge");
        }
        else if (CurrentFusible.CompareTag("Radioactif")) //Radiation
        {
            Debug.Log("Radiation");
        }
      //}
    }

}
