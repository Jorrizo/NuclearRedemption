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


    [Header("Watt")]
    public float wattObjectif = 0f; // Objectif de production en Watt
    public float wattProductionSeconde = 0f; // Production actuelle en Watt
    public int bonusPercentage = 0;
    public float wattProduit = 0f;

    [Header("User Interface")]
    public int indexUI = 0;

    [Header("Lights")]
    public Light integrityLight;

    [Header("Modules")]
    public ModuleState[] modules;
    public bool[] currentModuleStable;
    public bool[] currentModuleProductive;

    [Header("Liaisons")]
    public LiaisonManager[] Liaisons;

    [Header("GameState")]
    public bool IsGameStarted = false;
    public GameStates type = GameStates.Préparation;
    
    [Header("Cooldowns")]
    public float coolDownState = 20f;
    float timeStampState;

    public float coolDownNextEvent = 10f;
    float timeStampNextEvent;

    bool[] Default = new bool[] { false, false, false, false };
    private string informationsEvent = "nothing";

    bool Iamcalled = false;

    [Header("Techniciens")]
    public int extraTekos = 5;
    public int tekosMax;
    public int tekosSaved;
    public int tekosDead;
    //public int PopulationMax = 200;
    //public int PopulationToSave = 200; // à supprimer (Pas sûr)
    //public float PopulationIndoor = 200; // techniciens dans la centrale
    //public float PopulationOutdoor = 0;  // à supprimer
    //public float PopulationFlow = 0; // à supprimer
    //public int EnchantillonToGamble = 20; // à suprimer
    //float[] Percentage;
    //public int TempSavedPeople;

    [Header("Combo")]
    //public float PaisibleCombo;

    [Header("Probabilities Arays")]
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

    [Header("Central integrity")]
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
        FixEventProbabilities();
        FaxSpawnManager.instance.SpawnFax();

        //TempSavedPeople = 0;

    }

    // Update is called once per frame
    void Update()
    {
        UpdatesFonctions();
    }

    public void UpdatesFonctions()
    {
        if (IsGameStarted) // Si la partie à commencé
        {

            if (!Iamcalled)
            {
                timeStampState = Time.time + coolDownState; // cooldown preparation
                Iamcalled = true;
            }

            WhichModulesIsStable();
            Integrity();
            ProdutionSeconde();


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
                FaxSpawnManager.instance.SpawnFax();

            }

            if (Time.time >= timeStampNextEvent && type != GameStates.Préparation)
            {
                NextModuleEvent();
                //PeopleFlow();
                ProductionWatt();

                for (int i = 0; i < modules.Length; i++)
                {
                    modules[i].CheckState();
                    modules[i].ErrorInspector();
                }

                // PaisibleFlow();

                timeStampNextEvent = Time.time + coolDownNextEvent;
            }
            /*PopulationIndoor -= (Time.deltaTime * PopulationFlow);
            PopulationOutdoor += (Time.deltaTime * PopulationFlow);*/
        }
        else
        {
            IsAllModuleAreProductive();
        }
    }

   /* public void PaisibleFlow()
    {
        if (type == GameStates.Paisible)
        {
            if (PaisibleCombo < 5f)
            {
                PaisibleCombo += 0.25f;
                if (PaisibleCombo > 0.26f && PaisibleCombo < 6f)
                {
                    if (PopulationFlow < 3)
                    {
                        PopulationFlow += PaisibleCombo;
                    }
                }
            }
        }
        else
        {
            PaisibleCombo = 0f;
        }
    }*/

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
                //EnchantillonToGamble = 20;
                //TempSavedPeople = 0;
                //AmountGamble(4f,5, Percentage = new float[] { 0.5f, 0.5f });
                break;

            case 1:
                type = GameStates.Trépidant;
                facteurSecondaire = 2f;
                //EnchantillonToGamble = 20;
                //TempSavedPeople = 0;
                //AmountGamble(6f,3,Percentage = new float[] { 0.8f, 0.2f });
                break;

            case 2:
                type = GameStates.Paisible;
                facteurSecondaire = 1f;
                //EnchantillonToGamble = 20;
                //TempSavedPeople = 0;
                //AmountGamble(10f,1, Percentage = new float[] { 1f, 0f });
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
                            if (modules[1].isProductive)
                            {
                                modules[0].Etats[i] = true;
                            }
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
                            if (modules[1].isProductive)
                            {
                                modules[1].Etats[i] = true;
                            }
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
                            if (modules[2].isProductive)
                            {
                                modules[2].Etats[i] = true;
                            }
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


   void Integrity() // lumière de l'intégrité de la centrale à modifier pour que ce soit selon l'état de la partie
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

   /* public void RouletteRusse(int echantillonToGamble, float [] SavePourcentage) // à supprimer 
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
                    PopulationIndoor--;
                    break;
            }
        }
    }

    public void AmountGamble(float timeOfRelance,int AmountRelance, float [] Percentage)
    {
                RouletteRusse(((int)((PopulationIndoor*25)/100)), Percentage);

    }

    public void PeopleFlow()
    {
        int etatsAmount = 0;
        int modulesStables = 0;
        for (int i = 0; i < modules.Length; i++)
        {
            for (int j = 1; j < modules[0].Etats.Length; j++)
            {
                if (modules[i].Etats[j] == true)
                {
                    etatsAmount++;

                }
            }
            if (modules[i].Etats[0])
            {
                modulesStables++;
            }
        }
        switch (modulesStables)
        {
            case 0:
                switch (etatsAmount)
                {
                    case 1:
                        //Impossible
                        break;
                    case 2:
                        //Impossible
                        break;
                    case 3:
                        PopulationFlow = 0.8f;
                        break;
                    case 4:
                        PopulationFlow = 0.7f;
                        break;
                    case 5:
                        PopulationFlow = 0.5f;
                        break;
                    case 6:
                        PopulationFlow = 0.2f;
                        break;

                    default:
                        break;
                }
                break;
            case 1:
                switch (etatsAmount)
                {
                    case 1:
                        //Impossible
                        break;
                    case 2:
                        PopulationFlow = 0.9f;
                        break;
                    case 3:
                        PopulationFlow = 0.7f;
                        break;
                    case 4:
                        PopulationFlow = 0.6f;
                        break;
                    case 5:
                        PopulationFlow = 0.3f;
                        break;
                    case 6:
                        PopulationFlow = 0f;
                        break;

                    default:
                        break;
                }
                break;
            case 2:
                switch (etatsAmount)
                {
                    case 1:
                        PopulationFlow = 1f ;
                        break;
                    case 2:
                        PopulationFlow = 0.7f;
                        break;
                    case 3:
                        PopulationFlow = 0.5f;
                        break;
                    case 4:
                        //Impossible
                        break;
                    case 5:
                        //Impossible
                        break;
                    case 6:
                        //Impossible
                        break;

                    default:
                        break;
                }
                        break;
            case 3:
                if(PaisibleCombo == 0f)
                {
                    PopulationFlow = 1.3f;
                }
                break;

            default:
                break;
        }
    }*/

   public void  IsAllModuleAreProductive() // pour le démarage de la partie
   {
        WhichModulesIsProductive();
        switch (NbModulesProductif())
        {
            case 0:
                Debug.Log("Aucun module productif");
                break;

            case 1:
                Debug.Log("1 module productif");
                break;

            case 2:
                Debug.Log("2 modules productif");
                break;

            case 3:
                Debug.Log("3 modules productif");
                if (Liaisons[0].Starting == modules[0] && Liaisons[1].Starting == modules[1] && Liaisons[2].Starting == modules[2]) // Liaisons bien initialisée
                {
                    IsGameStarted = true;
                }

                break;
        }

    }

   void WhichModulesIsProductive() // Stocke dans la liste de bool currentModuleProductive[] les modules actuelement productif /!\ Doublon de fonction /!\
    {
        for (int i = 0; i < ModuleManager.instance.Modules.Length; i++)
        {

            if (ModuleManager.instance.Modules[i].isProductive)
            {
                currentModuleProductive[i] = true;
            }
            else
            {
                currentModuleProductive[i] = false;
            }
        }
   }

    int NbModulesProductif() // Compte le nombre de modules productifs depuis la liste de bool CurrentModuleProductive[] /!\ Doublon de fonction /!\
    {
        int resultat = 0;
        for (int i = 0; i < currentModuleProductive.Length; i++)
        {

            if (currentModuleProductive[i])
            {
                resultat++;
            }

        }
        return resultat;
    }

    public void ProductionWatt()
    {
        int tempBonusPercentage = 0;
        float tempwattProductionSeconde = 0;
        for (int i = 0; i < modules.Length; i++)
        {
            ModuleState myModule = modules[i];
            if (myModule.isProductive)
            {
                tempBonusPercentage += myModule.AddAndReturnTekos(myModule.Tekos);
                tempwattProductionSeconde += myModule.productionWattSecondes;
            }

        }
        bonusPercentage = tempBonusPercentage;
        wattProductionSeconde = tempwattProductionSeconde +((tempwattProductionSeconde*bonusPercentage)/100);
    }

    public void ProdutionSeconde()
    {
        wattProduit += wattProductionSeconde * Time.deltaTime;
    }
}
 