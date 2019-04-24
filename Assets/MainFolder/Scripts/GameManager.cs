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

    public float coolDown = 30f;
    float timeStamp;

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
        timeStamp = Time.time + coolDown;
        FixEventProbabilities();

    }

    // Update is called once per frame
    void Update()
    {
        WhichModulesIsStable();

        if (Time.time >= timeStamp) // Si 30 secondes se sont écoulés
        {
            if (type == GameStates.Préparation) // Après l'état préparation suit forcement l'état Paisible
            {
                type = GameStates.Paisible;
            }

            else
            {
                SwitchStates();
            }

            FixEventProbabilities();
            timeStamp = Time.time + coolDown;
        }
    }

    void FixEventProbabilities() // Ajuste les probabilitées d'évenements selon l'état de la partie
    {
        switch (type)
        {
            case GameStates.Préparation:
                Debug.Log("Preparation");
                break;
            case GameStates.Paisible:
                Debug.Log("Paisible");
                break;
            case GameStates.Trépidant:
                Debug.Log("Trépidant");
                break;
            case GameStates.Intenable:
                Debug.Log("Intenable");
                break;
        }
    }

    void WhichModulesIsStable() // Stoque dans CurrentBoolStable[] les modules actuelement stable
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

    int NbModulesStable() // Compte le nombre de module stable depuis CurrentBoolStable[] 
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

}
