using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public enum GameStates {
        Préparation,
        Paisible,
        Trépidant,
        Intenable
    }

    public Light integrityLight;
    public ModuleState[] modules;

    public GameStates type = GameStates.Préparation;

    public bool[] currentModuleStable;

    public float coolDownState = 10f;
    float timeStampState;

    public float coolDownNextEvent = 10f;
    float timeStampNextEvent;

    bool[] Default = new bool[] { false, false, false, false };
    private string informationsEvent = "nothing";


    public int PopulationMax = 200;
    public int PopulationToSave = 200;
    public int PopulationSaved = 200;
    public int EnchantillonToGamble = 20;
    float[] Percentage;
    public int TempSavedPeople;


    public float[] ModulesProbabilities = new float[] { 0.33f, 0.33f, 0.33f };

    /* Mémo
   1: Module A
   2: Module B
   3: Module C
    */


    public float[] EventsProbabilities = new float[] { 0.0f, 0.0f, 0.0f, 0.0f };

    /* Mémo
     1: Rien
     2: EvenementModule
     3: Lumière
     4: FusibleRate
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

    //Gestion de l'intégritée globale de la centrale

    public float integriteGlobale = 1000f;

    public float facteurPrimaire = 0f;
    public float facteurSecondaire = 1f;
    public float facteurIntegrite = 0f;

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
        Integrity();

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
          //  NextEvent();
            NextModuleEvent();
            timeStampNextEvent = Time.time + coolDownNextEvent;
        }
    }

    void FixEventProbabilities() // Ajuste les probabilitées d'évenements selon l'état de la partie
    {
        switch (type)
        {
            case GameStates.Préparation:
                EventsProbabilities = new float[] { 0.0f, 0.0f, 0.0f, 1f };
                modulesEventsProbabilities = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
                coolDownNextEvent = 10f;
                break;
            case GameStates.Paisible:
                EventsProbabilities = new float[] { 0.2f, 0.65f, 0.07f, 0.08f };
                modulesEventsProbabilities = new float[] { 0.27f, 0.27f, 0.27f, 0.053f, 0.053f, 0.053f, 0.02f };
                coolDownNextEvent = 10f;
                break;
            case GameStates.Trépidant:
                EventsProbabilities = new float[] { 0.1f, 0.75f, 0.07f, 0.08f };
                modulesEventsProbabilities = new float[] { 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.09f };
                coolDownNextEvent = 6f;
                break;
            case GameStates.Intenable:
                EventsProbabilities = new float[] { 0.01f, 0.84f, 0.07f, 0.08f };
                modulesEventsProbabilities = new float[] { 0.12f, 0.12f, 0.12f, 0.163f, 0.163f, 0.163f, 0.14f };
                coolDownNextEvent = 4f;
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
                facteurSecondaire = 4f;
                AmountGamble(4f,4/6f, Percentage = new float[] { 0.2f, 0.8f });
                break;

            case 1:
                type = GameStates.Trépidant;
                facteurSecondaire = 2f;
                AmountGamble(6f,2/4f,Percentage = new float[] { 0.5f, 0.5f });
                Debug.Log("Evenementtada");
                break;

            case 2:
                type = GameStates.Paisible;
                facteurSecondaire = 1f;
                AmountGamble(10f,6f, Percentage = new float[] { 1f, 0f });
                break;
        }
    }

    public float EventPicker(float[] probs) //retourne un evenement selon ses probabilitée d'apparition depuis un tableau de proba donné en argument
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
        Debug.Log("Out Of Range");
        return probs.Length - 1;
    }

    bool[] NextEvent() // Quel est le prochain evenement ?
    {
        switch ((int)EventPicker(EventsProbabilities)) // es-ce : Rien ou un problème de module ou de lummière ?
        {
            case 0:
                Debug.Log("Il ne se passe rien");
                return Default;
            case 1:
                bool[] i = new bool[] { false, false, false, false }; // Bool à retourner quand ce n'est pas un Evenement concernant les modules

                switch ((int)EventPicker(modulesEventsProbabilities)) // Quel type de problème ?
                {
                    case 0: // surcharge
                        informationsEvent = "Surcharge"; // aide pour le Debug.Log
                        i = new bool[] { false, true, false, false };
                        return i;

                    case 1: // surchauffe
                        informationsEvent = "Surchauffe";
                        i = new bool[] { false, false, true, false };
                        return i;


                    case 2: // radiation
                        informationsEvent = "Radiation";
                        i = new bool[] { false, false, false, true };
                        return i;

                    case 3: // surcharge et surchauffe
                        informationsEvent = "Surcharge et surchauffe";
                        i = new bool[] { false, true, true, false };
                        return i;

                    case 4: //surcharge et radiation
                        informationsEvent = "Surcharge et radiation";
                        i = new bool[] { false, true, false, true };
                        return i;

                    case 5: // radiation et surchauffe
                        informationsEvent = "Radiation et surchauffe";
                        i = new bool[] { false, false, true, true };
                        return i;

                    case 6: // surcharge, surchauffe et radiation
                        informationsEvent = "Surcharge, surchauffe et radiation";
                        i = new bool[] { false, true, true, true };
                        return i;

                    default:
                        Debug.Log("Etat de module non listé (inconnu)");
                        return i;
                        
                }
                
            case 2:
                Debug.Log("Et la lumière fut");
                LumiereManager.instance.GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();
                return Default;

            case 3:
                Debug.Log("Les fusibles !");
                if (SpawnManager.instance.IsFuseJam == false)
                {
                    SpawnManager.instance.type = SpawnManager.SpawnStates.Frenzy;
                    SpawnManager.instance.Temp = Time.time + SpawnManager.instance.FrenzyCoolDown;
                }
                return Default;

            default:
                Debug.Log("Evenement non listé dans l'eventPicker");
                return Default;
        }
    }

    void NextModuleEvent() //Choisi le prochain module à appliquer un ou des etats
    {
            bool[] Echantillon = NextEvent(); // Va chercher le prochain Evenement

        if (Echantillon != Default) // eviter les evenements qui ne concerne pas les modules.
        {
            switch ((int)EventPicker(ModulesProbabilities))
            {

                case 0: // Module A
                    Debug.Log(informationsEvent + " pour le Module A");
                    ControlModulesStates(Echantillon, modules[1].Etats, modules[2].Etats);
                    for (int i = 0; i < Echantillon.Length; i++)  // Parcourt le tableau et ecrase avec le tableau Echantillon
                    {
                        if (Echantillon[i] && !modules[0].Etats[i])
                        {
                            modules[0].Etats[i] = true;
                        }

                    }
                    break;
                case 1: // Module B
                    Debug.Log(informationsEvent + " pour le Module B");
                    ControlModulesStates(Echantillon, modules[0].Etats, modules[2].Etats);
                    for (int i = 0; i < Echantillon.Length; i++)
                    {
                        if (Echantillon[i] && !modules[1].Etats[i])
                        {
                            modules[1].Etats[i] = true;
                        }

                    }
                    break;
                case 2: // Module C
                    Debug.Log(informationsEvent + " pour le Module C");
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
                    Debug.Log("Module non listé dans l'eventPicker(inconnu)");
                    break;
            }
        }
        
    }




    bool[] ControlModulesStates(bool[] Echantillon, bool[] ModuleX, bool[] ModuleY) // Ajuste l'Echantillon pour eviter que + de 2 modules aient le même état
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


   void Integrity()
    {
        FacteurPrimaire();
        facteurIntegrite = facteurPrimaire * facteurSecondaire;
        integriteGlobale -= facteurIntegrite * Time.deltaTime;

        if (integrityLight != null)
        {
            switch (integriteGlobale)
            {
                case float n when (n < 1000 && n > 950):
                    integrityLight.color = new Color(0,0,255);
                    break;

                case float n when (n < 950 && n > 930):
                    integrityLight.color = new Color(255, 165, 0);
                    break;

                case float n when (n < 930):
                    integrityLight.color = new Color(255, 0, 0);
                    break;
            }

        }
    }


    void FacteurPrimaire()
    {
        facteurPrimaire = 0f;
        for (int i = 0; i < modules.Length; i++)
        {

            facteurPrimaire += modules[i].StateTreatment(modules[i].StatesCount());
        }
    }

    public void RouletteRusse(int echantillonToGamble, float [] SavePourcentage)
    {
        for (int i = 0; i < echantillonToGamble; i++)
        {
            switch (EventPicker(SavePourcentage))
            {
                case 0:
                    Debug.Log("survivre");
                    break;

                case 1:
                    Debug.Log("1mort");
                    PopulationSaved--;
                    TempSavedPeople++;

                    break;
            }
        }
    }

    public void AmountGamble(float timeOfRelance,float AmountRelance, float [] Percentage)
    {
        float temp = Time.time + timeOfRelance;
        float temp2 = Time.time + AmountRelance;
        TempSavedPeople = EnchantillonToGamble;
        if (Time.time < temp)
        {
            Debug.Log("temp");

            if (Time.time < temp2)
            {
                Debug.Log("temp2");

                EnchantillonToGamble -= TempSavedPeople;
                RouletteRusse(EnchantillonToGamble, Percentage);
                temp2 = Time.time + AmountRelance;
            }
        }
    }
}