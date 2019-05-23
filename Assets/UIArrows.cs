using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArrows : MonoBehaviour
{

    public Text ArrowAB;
    public Text ArrowBC;
    public Text ArrowCA;

    public bool aB = true;
    public bool bC = true;
    public bool cA = true;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InversionAB()
    {
        if (aB == true)
        {
            aB = false;
        }
        else
        {
            aB = true;
        }
    }

    public void InversionBC()
    {
        if (bC == true)
        {
            bC = false;
        }
        else
        {
            bC = true;
        }
    }

    public void InversionCA()
    {
        if (cA == true)
        {
            cA = false;
        }
        else
        {
            cA = true;
        }
    }

    public void CheckArrows()
    {
        if (aB)
        {
            ArrowAB.text = "←";
        }
        else
        {
            ArrowAB.text = "→";
        }

        if (bC)
        {
            ArrowBC.text = "←";
        }
        else
        {
            ArrowBC.text = "→";
        }

        if (cA)
        {
            ArrowCA.text = "←";
        }
        else
        {
            ArrowCA.text = "→";
        }
    }
}
