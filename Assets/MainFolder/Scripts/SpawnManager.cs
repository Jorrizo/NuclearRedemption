using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance = null;

    public GameObject[] PreFabFusibles;

    /* Mémo
     1: Surcharge
     2: Surchauffe
     3: radiation
     4: Lumière
     */

    public float[] FusibleSpawnProbabilities = new float[] { 0.25f, 0.25f, 0.25f, 0.25f };

    /* Mémo
     1: Surcharge
     2: Surchauffe
     3: radiation
     4: Lumière
    */

    public float coolDown = 3f;
    float temp;

    public float FrenzyCoolDown = 0.5f;
    public float Temp;
    public float Temp2;


    public enum SpawnStates
    {

        FuseJam,
        Fonctional,
        Frenzy,

    }
    public SpawnStates type = SpawnStates.Fonctional;
    public bool IsFuseJam;


    private void Awake()
    {
        if (instance == null)
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
        temp = Time.time + coolDown;
        type = SpawnStates.Fonctional;
        IsFuseJam = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType(typeof(GameManager)) != null)
        {
            if (GameManager.instance.IsGameStarted)
            {
                InstantiateFuse();
                SpawnRate();
            }
        }
        else
        {
            if (TutorielManager.instance.IsGameStarted)
            {
                InstantiateFuse();
                SpawnRate();
            }
        }
        
    }

    void InstantiateFuse()
    {
        if (temp < Time.time && type != SpawnStates.FuseJam)
        {
            if (FindObjectOfType(typeof(GameManager)) != null)
            {
                switch (GameManager.instance.EventPicker(FusibleSpawnProbabilities))
                {
                    case 0:
                        Instantiate(PreFabFusibles[0], gameObject.GetComponent<Transform>());
                        break;

                    case 1:
                        Instantiate(PreFabFusibles[1], gameObject.GetComponent<Transform>());
                        break;

                    case 2:
                        Instantiate(PreFabFusibles[2], gameObject.GetComponent<Transform>());
                        break;

                    case 3:
                        Instantiate(PreFabFusibles[3], gameObject.GetComponent<Transform>());
                        break;
                }
            }
            else
            {
                switch (TutorielManager.instance.EventPicker(FusibleSpawnProbabilities))
                {
                    case 0:
                        Instantiate(PreFabFusibles[0], gameObject.GetComponent<Transform>());
                        break;

                    case 1:
                        Instantiate(PreFabFusibles[1], gameObject.GetComponent<Transform>());
                        break;

                    case 2:
                        Instantiate(PreFabFusibles[2], gameObject.GetComponent<Transform>());
                        break;

                    case 3:
                        Instantiate(PreFabFusibles[3], gameObject.GetComponent<Transform>());
                        break;
                }
            }
            
            temp = Time.time + coolDown;
        }
    }

    void SpawnRate()
    {
        
        switch (type)
        {
            case SpawnStates.FuseJam:
                IsFuseJam = true;
                break;

            case SpawnStates.Fonctional:
                coolDown = 3;
                IsFuseJam = false;
                break;

            case SpawnStates.Frenzy:
                coolDown = 0.2f;
                //Temp = Time.time + FrenzyCoolDown; //a appeler lors de l'appel de l'evenement dans le gamemanager

                if (Time.time > Temp && IsFuseJam == false)
                {
                    type = SpawnStates.FuseJam;
                }

                if (Time.time > Temp2 && IsFuseJam == true)
                {
                    coolDown = 0.01f;
                    type = SpawnStates.Fonctional;
                }

                break;

        }
    }

    public void RepairFlow()
    {
        if (type == SpawnStates.FuseJam)
        {
            type = SpawnStates.Frenzy;
            Temp2 = Time.time + FrenzyCoolDown;
        }
    }
}
