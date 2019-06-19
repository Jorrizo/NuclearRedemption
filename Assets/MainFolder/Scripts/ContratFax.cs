using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContratFax : MonoBehaviour
{
    public bool HasPlayed;

    public Text objectif;

    // Start is called before the first frame update
    void Start()
    {
        HasPlayed = false;
        objectif.text = GameManager.instance.wattObjectif.ToString() + " Watt";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Animator>() != null)
        {
            if (HasPlayed)
            {
                DestroyTheAnimation();
            }
        }
    }

    public void DestroyTheAnimation()
    {
        Destroy(GetComponent<Animator>());
    }
}
