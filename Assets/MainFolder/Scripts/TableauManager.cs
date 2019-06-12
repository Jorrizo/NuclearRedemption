using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TableauManager : MonoBehaviour
{

    public LiaisonManager[] Liaisons;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TranfertEnergy() //appelé quand on appuit sur le bouton validation
    {
        for (int i = 0; i < Liaisons.Length; i++)
        {
            if (Liaisons[i].IsLiaisonValid)
            {
                if (!Liaisons[i].CurrentFusible.GetComponent<FusibleManager>().isUsed)
                {
                    if (Liaisons[i].CurrentFusible.CompareTag("Surcharge"))
                    {
                        Liaisons[i].Starting.Etats[1] = false;
                        Liaisons[i].CurrentFusible.GetComponent<FusibleManager>().isUsed = true;
                        Liaisons[i].IsLiaisonValid = false;
                        gameObject.GetComponentInChildren<LiaisonManager>().Led.material.CopyPropertiesFromMaterial(gameObject.GetComponentInChildren<LiaisonManager>().Red);
                        Liaisons[i].Starting.IamCalled = false;
                        Liaisons[i].Starting.timeStampState = 5000;
                        Liaisons[i].Starting.CheckState();
                        Liaisons[i].GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();

                    }
                    if (Liaisons[i].CurrentFusible.CompareTag("Surchauffe"))
                    {
                        Liaisons[i].Starting.Etats[2] = false;
                        Liaisons[i].CurrentFusible.GetComponent<FusibleManager>().isUsed = true;
                        Liaisons[i].IsLiaisonValid = false;
                        gameObject.GetComponentInChildren<LiaisonManager>().Led.material.CopyPropertiesFromMaterial(gameObject.GetComponentInChildren<LiaisonManager>().Red);
                        Liaisons[i].Starting.IamCalled = false;
                        Liaisons[i].Starting.timeStampState = 5000;
                        Liaisons[i].Starting.CheckState();
                        Liaisons[i].GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();

                    }
                    if (Liaisons[i].CurrentFusible.CompareTag("Radioactif"))
                    {
                        Liaisons[i].Starting.Etats[3] = false;
                        Liaisons[i].CurrentFusible.GetComponent<FusibleManager>().isUsed = true;
                        Liaisons[i].IsLiaisonValid = false;
                        gameObject.GetComponentInChildren<LiaisonManager>().Led.material.CopyPropertiesFromMaterial(gameObject.GetComponentInChildren<LiaisonManager>().Red);
                        Liaisons[i].Starting.IamCalled = false;
                        Liaisons[i].Starting.timeStampState = 5000;
                        Liaisons[i].Starting.CheckState();
                        Liaisons[i].GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();

                    }
                }
            }
        }
    }

    public void IsProductive() // le module devient productif ou non
    {
        for (int i = 0; i < Liaisons.Length; i++)
        {
            if (Liaisons[i].CurrentFusible != null)
            {
                if (Liaisons[i].CurrentFusible.CompareTag("On") && Liaisons[i].ProperModule.isProductive != true)
                {
                    Liaisons[i].GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();
                    gameObject.GetComponentInChildren<LiaisonManager>().Led.material.CopyPropertiesFromMaterial(gameObject.GetComponentInChildren<LiaisonManager>().Red);
                    Liaisons[i].ProperModule.isProductive = true;
                }
                else if (Liaisons[i].CurrentFusible.CompareTag("Off") && Liaisons[i].ProperModule.isProductive == true)
                {
                    
                    Liaisons[i].GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();
                    gameObject.GetComponentInChildren<LiaisonManager>().Led.material.CopyPropertiesFromMaterial(gameObject.GetComponentInChildren<LiaisonManager>().Red);
                    Liaisons[i].ProperModule.isProductive = false;
                }
            }
        }


    }
}
