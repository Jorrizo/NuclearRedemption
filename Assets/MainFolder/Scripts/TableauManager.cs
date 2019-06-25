using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.Audio;

public class TableauManager : MonoBehaviour
{
    public AudioClip snap;
    public AudioClip unSnap;

    public ModuleState ModuleA;
    public ModuleState ModuleB;
    public LiaisonManager[] Liaisons;
    public ProductiveManager[] ProductiveSnap;
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
                        GetComponent<AudioSource>().PlayOneShot(unSnap);
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
                        GetComponent<AudioSource>().PlayOneShot(unSnap);
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
                        GetComponent<AudioSource>().PlayOneShot(unSnap);
                    }
                }
            }
        }
    }

    public void IsProductive() // le module devient productif ou non
    {
        if (Liaisons.Length == ProductiveSnap.Length)
        {
            for (int i = 0; i < ProductiveSnap.Length; i++)
            {
                if (ProductiveSnap[i].currentFusible != null)
                {
                    if (ProductiveSnap[i].currentFusible.CompareTag("On"))
                    {
                        if (!Liaisons[i].ProperModule.isProductive)
                        {
                            Liaisons[i].ProperModule.isProductive = true;
                        }
                    }
                    else
                    {
                        ProductiveSnap[i].GetComponent<VRTK_SnapDropZone>().ForceUnsnap();
                        Liaisons[i].ProperModule.isProductive = false;
                    }
                }
                else
                {

                    Liaisons[i].ProperModule.isProductive = false;
                }
            }
        }
        else if (Liaisons.Length != ProductiveSnap.Length)
        {
            Debug.Log("Tutorielmode");
            for (int i = 0; i < ProductiveSnap.Length; i++)
            {
                switch (i)
                {
                    case 1:
                        if (ProductiveSnap[1].currentFusible != null)
                        {
                            if (ProductiveSnap[1].currentFusible.CompareTag("On"))
                            {
                                if (!ModuleA.isProductive)
                                {
                                    ModuleA.isProductive = true;
                                }
                            }
                            else
                            {
                                ProductiveSnap[1].GetComponent<VRTK_SnapDropZone>().ForceUnsnap();
                                ModuleA.isProductive = false;
                            }
                        }
                        else
                        {
                            if (ProductiveSnap[1].currentFusible == null)
                            {
                                ModuleA.isProductive = false; // module A 
                            }
                        }
                            break;

                    case 2:
                        if (ProductiveSnap[2].currentFusible != null)
                        {
                            Debug.Log("NotNull");
                            if (ProductiveSnap[2].currentFusible.CompareTag("On"))
                            {
                                Debug.Log("CompareTag Ok");
                                if (!ModuleB.isProductive)
                                {
                                    ModuleB.isProductive = true;
                                }
                            }
                            else
                            {
                                ProductiveSnap[2].GetComponent<VRTK_SnapDropZone>().ForceUnsnap();
                                ModuleB.isProductive = false;
                            }
                        }
                        else
                        {
                            if (ProductiveSnap[2].currentFusible == null)
                            {
                                ModuleB.isProductive = false; // module A 
                            }
                        }
                        break;
                }
            }
        }
    }

    public void SoundSnap()
    {
        GetComponent<AudioSource>().PlayOneShot(snap);
    }

    public void SoundUnSnap()
    {
        GetComponent<AudioSource>().PlayOneShot(unSnap);
    }
}
