using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TekosManager : MonoBehaviour
{
    public Animator anim;
    public int random;
    public int bonusTekos = 2;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(1, 4);
        SetAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetAnimation()
    {
        switch (random)
        {
            case 1:
                anim.SetBool("1", true);
                break;
            case 2:
                anim.SetBool("2", true);
                break;
            case 3:
                anim.SetBool("3", true);
                break;            
        }
    }
}
