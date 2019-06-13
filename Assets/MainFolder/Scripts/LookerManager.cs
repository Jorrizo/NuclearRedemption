using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookerManager : MonoBehaviour
{
    private Animator Door;
    // Start is called before the first frame update
    void Start()
    {
        Door = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitAnimation()
    {
        if (gameObject.GetComponentInChildren<LeavingManager>().doorArmed)
        {
            Door.Play("testAnimDoor", 0, 0f); //Play l'animation d'ouverture de la porte
        }
    }

    public void ExitMirror()
    {
        GameManager.instance.AfterLever();
    }
}
