using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleState : MonoBehaviour
{

    public bool[] Etats = new bool[] { true, false, false, false } ;
    
    /* mémo
     0 : Stable
     1 : Surchauffe
     2 : Surcharge
     3 : Radiation
     */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Etats[0] = true;

        for (int i = 1; i < Etats.Length; i++)
        {
            if(Etats[i] == true)
            {
                Etats[0] = false;

            }
        }
    }


    public int StatesCount()
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

    public float StateTreatment(int s)
    {
        float v = 0;
            switch (s)
            {
                case 0:
                    v = 0f;
                    return v;
                case 1:
                    v = 0.1f;
                    return v;
                case 2:
                    v = 0.12f;
                    return v;
                case 3:
                    v = 0.15f;
                    return v;
            }
        return v;
    }
}
