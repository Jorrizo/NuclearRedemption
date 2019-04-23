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
                    if (Liaisons[i].CurrentFusible.CompareTag("Surchauffe"))
                    {
                        Liaisons[i].Starting.Surchauffe = false;
                        Liaisons[i].CurrentFusible.GetComponent<FusibleManager>().isUsed = true;
                        Liaisons[i].IsLiaisonValid = false;
                        Liaisons[i].GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();

                    }
                    if (Liaisons[i].CurrentFusible.CompareTag("Surcharge"))
                    {
                        Liaisons[i].Starting.Surcharge = false;
                        Liaisons[i].CurrentFusible.GetComponent<FusibleManager>().isUsed = true;
                        Liaisons[i].IsLiaisonValid = false;
                        Liaisons[i].GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();
                    }
                    if (Liaisons[i].CurrentFusible.CompareTag("Radioactif"))
                    {
                        Liaisons[i].Starting.Radiation = false;
                        Liaisons[i].CurrentFusible.GetComponent<FusibleManager>().isUsed = true;
                        Liaisons[i].IsLiaisonValid = false;
                        Liaisons[i].GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();
                    }
                }
            }
        }
    }
}
