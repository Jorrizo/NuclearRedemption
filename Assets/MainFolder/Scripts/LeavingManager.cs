using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavingManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float timeLeft = 3.0f;
    public bool LeavingPossible = false;
    public bool BoutonReached = false;
    public int Stack = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BoutonReached == true && Stack == 2)
        {
            TimerOpenning();
        }
    }

    public void TimerOpenning()
    {
        if (timeLeft >= 0)
        { 
            timeLeft -= Time.deltaTime;
            if (timeLeft< 0)
            {
                LeavingPossible = true;
            }
        }
    }

    public void Bouton()
    {
        BoutonReached = true;
        if(Stack < 2)
        {
            Stack++;
        }
    }
}
