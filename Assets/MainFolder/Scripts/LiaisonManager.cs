using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LiaisonManager : MonoBehaviour
{
    public ModuleState Starting;
    public ModuleState Ending;
    private ModuleState ValueTemp;

   
    public GameObject CurrentFusible;


    public bool IsLiaisonValid = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Invertion()
    {
        ValueTemp = Starting;
        Starting = Ending;
        Ending = ValueTemp;
    }

    public void CheckLiaison ()
    {
       
    }

    public void SnappedObject ()
    {
        CurrentFusible = GetComponentInChildren<VRTK_SnapDropZone>().GetCurrentSnappedObject();
    }


}
