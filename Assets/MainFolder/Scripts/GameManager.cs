﻿using System.Collections;
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

    public ModuleState[] modules;

    public GameStates type = GameStates.Préparation;

    public bool[] currentModuleStable;

    public float coolDownState = 30f;
    float timeStampState;

    public float coolDownNextEvent = 10f;
    float timeStampNextEvent;

    public float[] ModulesProbabilities = new float[] { 0.33f, 0.33f, 0.33f };

    /* Mémo
   1: Module A
   2: Module B
   3: Module C
    */


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
            NextModule();
            NextEvent();
            timeStampNextEvent = Time.time + coolDownNextEvent;
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
                modulesEventsProbabilities = new float[] { 0.27f, 0.27f, 0.27f, 0.053f, 0.053f, 0.053f, 0.02f };
                coolDownNextEvent = 10f;
                break;
            case GameStates.Trépidant:
                Debug.Log("Trépidant");
                EventsProbabilities = new float[] { 0.1f, 0.75f, 0.15f };
                modulesEventsProbabilities = new float[] { 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.09f };
                coolDownNextEvent = 5f;
                break;
            case GameStates.Intenable:
                Debug.Log("Intenable");
                EventsProbabilities = new float[] { 0.01f, 0.84f, 0.15f };
                modulesEventsProbabilities = new float[] { 0.12f, 0.12f, 0.12f, 0.163f, 0.163f, 0.163f, 0.14f };
                coolDownNextEvent = 3f;
                break;
        }
    }

    void WhichModulesIsStable() // Stoque dans la liste de bool CurrentModuleStable[] les modules actuelement stable
    {
            for (int i = 0; i<ModuleManager.instance.Modules.Length; i++)
            {

                if (ModuleManager.instance.Modules[i].Etats[0])
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

    float EventPicker(float[] probs) //retourne un evenement selon ses probabilitée d'apparition depuis un tableau de proba donné en argument
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

    bool[] NextEvent() // Quel est le prochain evenement ?
    {
        switch (EventPicker(EventsProbabilities)) // es-ce : Rien ou un problème de module ou de lummière ?
        {
            case 0:
                Debug.Log("Il ne se passe rien");
                return null;
            case 1:
                bool[] i = new bool[] { true, false, false, false };

                switch (EventPicker(modulesEventsProbabilities)) // Quel type de problème ?
                {
                    case 0: // surcharge
                        Debug.Log("surcharge");
                        i = new bool[] { false, true, false, false };
                        return i;

                    case 1: // surchauffe
                        Debug.Log("surchauffe");
                        i = new bool[] { false, false, true, false };
                        return i;


                    case 2: // radiation
                        Debug.Log("radiation");
                        i = new bool[] { false, false, false, true };
                        return i;

                    case 3: // surcharge et surchauffe
                        Debug.Log("surcharge et surchauffe");
                        i = new bool[] { false, true, true, false };
                        return i;

                    case 4: //surcharge et radiation
                        Debug.Log("surcharge et radiation");
                        i = new bool[] { false, true, false, true };
                        return i;

                    case 5: // radiation et surchauffe
                        Debug.Log("radiation et surchauffe");
                        i = new bool[] { false, false, true, true };
                        return i;

                    case 6: // surcharge, surchauffe et radiation
                        Debug.Log("surcharge, surchauffe et radiation");
                        i = new bool[] { false, true, true, true };
                        return i;

                    default:
                        Debug.Log("Etat de module non listé dans les evenements de modules");
                        return i;
                        
                }
                
            case 2:
                Debug.Log("Et la lumière fut");
                return null;
            default:
                Debug.Log("Evenement non listé dans l'eventPicker");
                return null;
        }
    }

    void NextModule()
    {
        bool[] Echantillon = NextEvent();


        switch (EventPicker(modulesEventsProbabilities))
        {
            case 0:
                Debug.Log("Module A");                
                ControlModulesStates(Echantillon, modules[1].Etats, modules[2].Etats);
                for (int i = 0; i < Echantillon.Length; i++)
                {
                    if (Echantillon[i] && !modules[0].Etats[i])
                    {
                        modules[0].Etats[i] = true;
                    }

                }
                break;
            case 1:
                Debug.Log("Module B");
                ControlModulesStates(Echantillon, modules[0].Etats, modules[2].Etats);
                for (int i = 0; i < Echantillon.Length; i++)
                {
                    if (Echantillon[i] && !modules[1].Etats[i])
                    {
                        modules[1].Etats[i] = true;
                    }

                }
                break;
            case 2:
                Debug.Log("Module C");
                ControlModulesStates(Echantillon, modules[0].Etats, modules[1].Etats);
                for (int i = 0; i < Echantillon.Length; i++)
                {
                    if (Echantillon[i] && !modules[2].Etats[i])
                    {
                        modules[2].Etats[i] = true;
                    }

                }
                break;

            default:
                Debug.Log("Module non listé dans l'eventPicker");
                break;
        }
    }





    bool[] ControlModulesStates(bool[] Echantillon, bool[] ModuleX, bool[] ModuleY)
    {

        if (Echantillon != null)
        {
            for (int i = 0; i < Echantillon.Length; i++)
            {
                if (Echantillon[i] == true)
                {
                    if (ModuleX[i] && ModuleY[i])
                    {
                        Echantillon[i] = false;

                    }
                }

            }
            return Echantillon;
        }
        return null;
    }

}
