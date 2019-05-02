using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LumiereManager : MonoBehaviour
{
    public static LumiereManager instance = null;

    public GameObject CurrentFusible;

    public Light[] LumiereGlobale;
    public Light[] LumiereLampe;

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
        LumiereSetActive(LumiereGlobale, LumiereLampe);
    }

    // Update is called once per frame
    void Update()
    {
        CheckLiaison();
    }

    public void SnappedObject() //Stoque le GameObject sur la snapzone
    {
        CurrentFusible = GetComponentInChildren<VRTK_SnapDropZone>().GetCurrentSnappedObject();
    }

    public void LumiereSetActive(Light[] Sample, Light[] Sample2)
    {
        for (int i = 0; i < Sample.Length; i++)
        {
            Sample[i].enabled = true;
            Sample2[i].enabled = false;
        }
    }

    public void CheckLiaison()
    {
        if (CurrentFusible != null)
        {
            if (!CurrentFusible.GetComponent<FusibleManager>().isUsed)// Si le fusible n'est pas usé
            {
                if (CurrentFusible.CompareTag("Lumière")) //Si c'est le fusible Surchauffe et que la liaison Surchauffe est respectée
                {
                    LumiereSetActive(LumiereGlobale, LumiereLampe);
                }
            }
            else  // Si le fusible est usée /!\ attention possible mauvais placement de ce code /!\
            {
                GetComponentInChildren<VRTK_SnapDropZone>().ForceUnsnap();
            }
        }
        else
        {
            LumiereSetActive(LumiereLampe,LumiereGlobale);
        }
    }
    public void OnUnSnapped()
    {
        CurrentFusible.GetComponent<FusibleManager>().isUsed = true;
        CurrentFusible = null;
    }
}
