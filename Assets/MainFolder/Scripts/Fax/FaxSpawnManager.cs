using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FaxSpawnManager : MonoBehaviour
{
    public static FaxSpawnManager instance = null;


    public GameObject SheetFax;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFax()
    {
        Instantiate(SheetFax, gameObject.transform.position, Quaternion.identity);
    }
}
