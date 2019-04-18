using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiaisonManager : MonoBehaviour
{
    public ModuleState Starting;
    public ModuleState Ending;
    private ModuleState ValueTemp;

    public GameObject [] FusibleList;
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

    public void SurchauffeIsSnapping ()
    {
        this.CurrentFusible = FusibleList[0];
    }

    public void SurchargeIsSnapping()
    {
        this.CurrentFusible = FusibleList[1];
    }

    public void RadiationIsSnapping()
    {
        this.CurrentFusible = FusibleList[2];
    }
}
