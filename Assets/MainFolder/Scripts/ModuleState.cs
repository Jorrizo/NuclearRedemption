using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ModuleState : MonoBehaviour
{
    public bool isProductive = false; // Es-ce qu'il produit des watts ?

    public bool[] Etats = new bool[] { true, false, false, false } ;

    public GameObject[] etatsIndicators;
    public GameObject[] LedsError;

    [Header("Watts")]
    public float productionWattSecondes = 2f;

    public Material Stable;
    public Material NotStable;

    [Header("Techniciens")]
    public GameObject[] Tekos;
    public GameObject TekosPrefab;
    public Transform[] TekosPos;
    public int intraTekos = 5;

    [Header("Cooldowns")]
    public float coolDownkill = 10f;
    public float timeStampState = 5000f;

    public bool IamCalled = false;


    //Predicate<GameObject> Pred;
    /* mémo
     0 : Stable
     1 : Surchauffe
     2 : Surcharge
     3 : Radiation
     */

    // Start is called before the first frame update
    void Start()
    {
        Etats[0] = true;
        AddAndReturnTekos(Tekos);

    }

    // Update is called once per frame
    void Update()
    {
        if (!IamCalled & !Etats[0])
        {
            CoolDownTekos(StatesCount());
            IamCalled = true;
        }
        //ArrayUtility.RemoveAt(ref Tekos, (int)(Array.FindLastIndex(Tekos, TekosPrefab)));
        if (Time.time >= timeStampState) // Si 6 secondes se sont écoulés
        {
            KillTekos();
        }
        

      
    }


    public int StatesCount() // nombres d'états actifs
    {
        int s = 0;

        for (int i = 1; i < Etats.Length; i++)
        {
            if (Etats[i] == true)
            {
                s++;
            }
        }       
        return s;
    }

    public float StateTreatment(int s) // valeur de destruction de la centrale selon le nombre d'etats sur le module
    {
        float v = 0;
            switch (s)
            {
                case 0:
                    v = 0f;
                    return v;
                case 1:
                    v = 1/6f;
                    return v;
                case 2:
                    v = 2/6f;
                    return v;
                case 3:
                    v = 3/6f;
                    return v;
            }
        return v;
    }


    public void CheckState() // activation des VFX 
    {
        Etats[0] = true;
        etatsIndicators[0].SetActive(true);


        for (int i = 1; i < Etats.Length; i++)
        {

            etatsIndicators[i].SetActive(false);
            if (Etats[i] == true)
            {
                //if (!etatsIndicators[i].activeSelf) // si il n'est pas actif
               // {
                    etatsIndicators[i].SetActive(true);
               // }
                etatsIndicators[0].SetActive(false);
                Etats[0] = false;

            }
        }
    }


    int NbEvent() // Compte le nombre d'evenement instable depuis la liste de bool Etats[] 
    {
        int resultat = 0;
        for (int i = 1; i < Etats.Length; i++)
        {

            if (Etats[i] == true)
            {
                resultat++;
            }

        }
        return resultat;
    }

    public void ErrorInspector()
    {
        int compte = 0;
        switch (NbEvent())
        {
            case 0: // aucune erreur
                for (int i = 0; i < LedsError.Length; i++)
                {
                    LedsError[i].SetActive(false);
                }
                break;

            case 1: // une erreur
                compte = NbEvent();
                for (int i = 0; i < LedsError.Length; i++)
                {
                    
                    if (compte > 0)
                    {
                        if (LedsError[i].activeSelf != true)
                        {
                            LedsError[i].SetActive(true);
                            compte--;

                        }
                        else if (LedsError[i].activeSelf == true)
                        {
                            compte--;
                        }
                    }
                    else if (compte == 0)
                    {
                        LedsError[i].SetActive(false);
                    }


                }
                break;

            case 2: // deux erreur
                compte = NbEvent();

                for (int i = 0; i < LedsError.Length; i++)
                {
                    
                    if (compte > 0)
                    {
                        if (LedsError[i].activeSelf != true)
                        {
                            LedsError[i].SetActive(true);
                            compte--;

                        }
                        else if (LedsError[i].activeSelf == true)
                        {
                            compte--;
                        }
                    }
                    else if (compte == 0)
                    {
                        LedsError[i].SetActive(false);
                    }

                }
                break;

            case 3: // trois erreur
                compte = NbEvent();

                for (int i = 0; i < LedsError.Length; i++)
                {
                    
                    if (compte > 0)
                    {
                        if (LedsError[i].activeSelf != true)
                        {
                            LedsError[i].SetActive(true);
                            compte--;

                        }
                        else if (LedsError[i].activeSelf == true)
                        {
                            compte--;
                        }
                    }
                    else if (compte == 0)
                    {
                        LedsError[i].SetActive(false);
                    }


                }
                break;

            default:
                break;
        }
    }

    public int AddAndReturnTekos(GameObject[] tekos)
    {
        int tempBonus = 0;
        
        for (int w = 0; w < tekos.Length; w++)
        {
            GameObject MyTekos = tekos[w];
            if (tekos.Length <= 5) // Sécuritée
            {
                if(tekos[w] == null) // Add Tekos
                {
                    if (intraTekos > 0)
                    {
                        for (int f = 0; f < TekosPos.Length; f++)
                        {
                            if (!(Physics.CheckSphere(TekosPos[f].position, 0.5f)))
                            {
                                Tekos[w] = Instantiate(TekosPrefab, TekosPos[f].position, Quaternion.identity, gameObject.transform);
                                break;
                            }
                        }
                        intraTekos--;
                    }
                    else if (GameManager.instance.extraTekos > 0)
                    {
                        for (int f = 0; f < Tekos.Length; f++)
                        {
                            if (!(Physics.CheckSphere(TekosPos[f].position, 0.5f)))
                            {
                                Tekos[w] = Instantiate(TekosPrefab, TekosPos[f].position, Quaternion.identity, gameObject.transform);
                                break;
                            }
                        }
                        GameManager.instance.extraTekos--;
                    }
                }
                if (MyTekos != null)
                {
                    tempBonus += MyTekos.GetComponent<TekosManager>().bonusTekos;
                }
            }
        }
        return tempBonus;
    }

    public void CoolDownTekos(int nbState)
    {      
        int tempState = nbState;
        if (tempState == 0)
        {
            timeStampState = 500f;
        }
        else if (tempState > 0)
        {          
            timeStampState = Time.time + coolDownkill;
        }
    }

    public void KillTekos()
    {
        //ArrayUtility.RemoveAt(ref Tekos, (Array.FindLastIndex( Tekos, Pred)));
        for (int i = 0; i < Tekos.Length; i++)
        {
            if (Tekos[i] != null)
            {
                Debug.Log("il y a qqlshoz");
                Destroy(Tekos[i]);
                Tekos[i] = null;
                timeStampState = 5000f;
                break;
            }     
        }
    }
}
