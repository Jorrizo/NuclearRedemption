using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FusibleManager : MonoBehaviour
{
    public bool isUsed = false;

    public AudioClip Falling;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            GetComponent<AudioSource>().PlayOneShot(Falling);
        }
    }
}
