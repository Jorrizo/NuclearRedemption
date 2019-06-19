using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK; //Accès à la catégorie VTRK

public class LiaisonManager : MonoBehaviour
{
    public ModuleState Starting; //Module éméteur
    public ModuleState Ending; //Module de récépteur
    private ModuleState ValueTemp;
    public Renderer Led;
    public Material Red;
    public Material Green;
    public Material Activate;

    public GameObject CurrentFusible;

    public ModuleState ProperModule;
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

                if (CurrentFusible.CompareTag("Surcharge") && (Starting.Etats[1] && !Ending.Etats[1])) //Si c'est le fusible Surchauffe et que la liaison Surchauffe est respectée
                {
                        IsLiaisonValid = true;
                        Led.material.CopyPropertiesFromMaterial(Green);
                }
                else if (CurrentFusible.CompareTag("Surchauffe") && (Starting.Etats[2] && !Ending.Etats[2])) //Si c'est le fusible Surcharge et que la liaison Surchauffe est respectée
                {
                        IsLiaisonValid = true;
                        Led.material.CopyPropertiesFromMaterial(Green);
                }
                else if (CurrentFusible.CompareTag("Radioactif") && (Starting.Etats[3] && !Ending.Etats[3])) //Si c'est le fusible Radiation et que la liaison Surchauffe est respectée
                {
                        IsLiaisonValid = true;
                        Led.material.CopyPropertiesFromMaterial(Green);
                }
                else // sécuritée
                {
                        IsLiaisonValid = false;
                        Led.material.CopyPropertiesFromMaterial(Red);
                }
            }
            else  // Si le fusible est usée 
            {
                GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();
                Led.material.CopyPropertiesFromMaterial(Red);
                Debug.Log("retour rouge");
            }
        }
        else  
        {
                GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();
                Led.material.CopyPropertiesFromMaterial(Red);
                Debug.Log("retour rouge");
        }
        
    }
}