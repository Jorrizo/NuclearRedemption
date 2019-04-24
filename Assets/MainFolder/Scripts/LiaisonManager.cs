using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK; //Accès à la catégorie VTRK

public class LiaisonManager : MonoBehaviour
{
    public ModuleState Starting; //Module éméteur
    public ModuleState Ending; //Module de récépteur
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
        if (CurrentFusible != null)
        {

            if (!CurrentFusible.GetComponent<FusibleManager>().isUsed)// Si le fusible n'est pas usé
            {
                Debug.Log("!Stable && !Used");

                if (CurrentFusible.CompareTag("Surchauffe") && (Starting.Surchauffe && !Ending.Surchauffe)) //Si c'est le fusible Surchauffe et que la liaison Surchauffe est respectée
                {
                    IsLiaisonValid = true;
                }
                else if (CurrentFusible.CompareTag("Surcharge") && (Starting.Surcharge && !Ending.Surcharge)) //Si c'est le fusible Surcharge et que la liaison Surchauffe est respectée
                {
                    IsLiaisonValid = true;
                }
                else if (CurrentFusible.CompareTag("Radioactif") && (Starting.Radiation && !Ending.Radiation)) //Si c'est le fusible Radiation et que la liaison Surchauffe est respectée
                {
                    IsLiaisonValid = true;
                }
                else // sécuritée
                {
                    IsLiaisonValid = false;
                }          
            }

            else  // Si le fusible est usée /!\ attention possible mauvais placement de ce code /!\
            {
                GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();
            }
        }
    }
}