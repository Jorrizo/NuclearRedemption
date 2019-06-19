using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ProductiveManager : MonoBehaviour
{

    public GameObject currentFusible;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SnappedObject() //Stoque le GameObject sur la snapzone
    {
        if (GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject() != null)
        {
            currentFusible = GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject();
        }
        else
        {
            currentFusible = null;
        }
    }
}