using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavingManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float timeLeft = 3.0f;
    public bool LeavingPossible = false;
    public bool doorArmed = false;
    public bool iAmCalled = false;
    public GameObject LedArm;
    public Material red;
    public Material green;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void ArmedLeaving()
    {
        if (iAmCalled == false && GameManager.instance.IsGameStarted)
        {
            doorArmed = true;
            //LedArm.GetComponent<Renderer>().copy
            iAmCalled = true;
        }
        
    }
}
