using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleManager: MonoBehaviour
{
    public static ModuleManager instance = null;
    public ModuleState[] Modules;

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
       /* for (int i = 0; i < Modules.Length; i++)
        {
            Modules[i].Stable = true;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
