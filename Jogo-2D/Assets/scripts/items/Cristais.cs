using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristais : MonoBehaviour
{
    public AudioClip som;
    private AudioSource sons;
    // Start is called before the first frame update
    void Start()
    {
        sons = GetComponent<AudioSource>();
        som = Resources.Load<AudioClip>("Crystal");
        sons.clip = som;
    }
}
