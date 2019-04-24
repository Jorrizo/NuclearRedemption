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

    public float coolDown = 30f;
    float timeStamp;
    public bool[] currentModuleStable;
    public float currentModuleStableInt = 0f;

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
        Tweek();

    }

    // Update is called once per frame
    void Update()
    {
        EtatsDesModules();
        Debug.Log(NbModulesStable());

        if (Time.time >= timeStamp)
        {
            if (type == GameStates.Préparation)
            {
                type = GameStates.Paisible;
            }

            else
            {

            }


            Tweek();
            timeStamp = Time.time + coolDown;
        }
    }

    void Tweek()
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

    void EtatsDesModules()
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

    int NbModulesStable()
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

}
