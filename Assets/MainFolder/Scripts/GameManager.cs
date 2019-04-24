using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public enum GameStates {
        Préparation,
        Paisible,
        Trépidant,
        Intenable
    }

    public GameStates type = GameStates.Préparation;

    public bool[] currentModuleStable;

    public float coolDownState = 30f;
    float timeStampState;

    public float coolDownNextEvent = 10f;
    float timeStampNextEvent;

    public float[] EventsProbabilities = new float[] { 0.0f, 0.0f, 0.0f };

    /* Mémo
     1: Rien
     2: EvenementModule
     3: Lumière
    */

    public float[] modulesEventsProbabilities = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

    /* Mémo
     1: Surcharge
     2: Surchauffe
     3: radiation
     4: surcharge/surchauffe
     5: surcharge/radiation
     6: radiation/surchauffe
     7: radiation/surcharge/surchauffe  
    */
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        type = GameStates.Préparation;
        timeStampState = Time.time + coolDownState;
        FixEventProbabilities();

    }

    // Update is called once per frame
    void Update()
    {
        WhichModulesIsStable();

        if (Time.time >= timeStampState) // Si 30 secondes se sont écoulés
        {
            if (type == GameStates.Préparation) // Après l'état préparation suit forcement l'état Paisible
            {
                type = GameStates.Paisible;
                timeStampNextEvent = Time.time + coolDownNextEvent;
            }

            else
            {
                SwitchStates();
            }

            FixEventProbabilities();
            timeStampState = Time.time + coolDownState;
        }

        if(Time.time >= timeStampNextEvent && type != GameStates.Préparation)
        {
            NextEvent();
        }
    }

    void FixEventProbabilities() // Ajuste les probabilitées d'évenements selon l'état de la partie
    {
        switch (type)
        {
            case GameStates.Préparation:
                Debug.Log("Preparation");
                EventsProbabilities = new float[] { 0.0f, 0.0f, 0.0f };
                modulesEventsProbabilities = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
                coolDownNextEvent = 10f;
                break;
            case GameStates.Paisible:
                Debug.Log("Paisible");
                EventsProbabilities = new float[] { 0.2f, 0.65f, 0.15f };
                modulesEventsProbabilities = new float[] { 0.27f, 0.27f, 0.27f, 0.053f, 0.053f, 0.053f, 0.03f };
                coolDownNextEvent = 10f;
                break;
            case GameStates.Trépidant:
                Debug.Log("Trépidant");
                EventsProbabilities = new float[] { 0.1f, 0.75f, 0.15f };
                modulesEventsProbabilities = new float[] { 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.1f };
                coolDownNextEvent = 5f;
                break;
            case GameStates.Intenable:
                Debug.Log("Intenable");
                EventsProbabilities = new float[] { 0.01f, 0.84f, 0.15f };
                modulesEventsProbabilities = new float[] { 0.12f, 0.12f, 0.12f, 0.163f, 0.163f, 0.163f, 0.15f };
                coolDownNextEvent = 3f;
                break;
        }
    }

    void WhichModulesIsStable() // Stoque dans la liste de bool CurrentModuleStable[] les modules actuelement stable
    {
            for (int i = 0; i<ModuleManager.instance.Modules.Length; i++)
            {

                if (ModuleManager.instance.Modules[i].Stable)
                {
                    currentModuleStable[i] = true;
                }
                else
                {
                currentModuleStable[i] = false;
                }
            }
    }

    int NbModulesStable() // Compte le nombre de module stable depuis la liste de bool CurrentModuleStable[] 
    {
        int resultat = 0;
        for (int i = 0; i <currentModuleStable.Length; i++)
        {

            if (currentModuleStable[i])
            {
                resultat++;
            }

        }
        return resultat;
    }

    void SwitchStates() // Change l'état de la partie
    {
        switch (NbModulesStable())
        {
            case 0:
                type = GameStates.Intenable;
                break;

            case 1:
                type = GameStates.Trépidant;
                break;

            case 2:
                type = GameStates.Paisible;
                break;
        }
    }

    float EventPicker(float[] probs) //Prendre un evenement selon ses probabilitée d'apparition
    {

        float total = 0; 

        foreach (float elem in probs) // nombre de probabilitées dans probs
        {
            total += elem;
        }

        float randomPoint = Random.value * total;  // valeur de 0 à 1 * l'addition de toutes les probabilitées du tableau

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i; // retourne l'evenement en question
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    void NextEvent() // Quel est le prochain evenement ?
    {
        switch (EventPicker(EventsProbabilities)) // es-ce : Rien ou un problème de module ou de lummière ?
        {
            case 0:
                Debug.Log("Il ne se passe rien");
                break;
            case 1:
                switch (EventPicker(modulesEventsProbabilities)) // Quel type de problème ?
                {
                    case 0: // surcharge

                        break;

                    case 1: // surchauffe

                        break;

                    case 2: // radiation

                        break;

                    case 3: // surcharge et surchauffe

                        break;

                    case 4: //surcharge et radiation

                        break;

                    case 5: // radiation et surchauffe

                        break;

                    case 6: // surcharge, surchauffe et radiation

                        break;

                    default:
                        Debug.Log("Etat de module non listé dans les evenements de modules");
                        break;
                }
                break;
            case 2:
                Debug.Log("Et la lumière fut");
                break;
            default:
                Debug.Log("Evenement non listé dans l'eventPicker");
                break;
        }
    }


}
