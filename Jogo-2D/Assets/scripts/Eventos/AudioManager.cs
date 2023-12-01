using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource SFX;
    public AudioClip[] Musics;
    public AudioClip[] SFXs;
    // Start is called before the first frame update
    void Start()
    {
        Music.clip = Musics[0];
        Music.Play();
    }

    public void TocarSom(AudioClip som)
    {
        SFX.PlayOneShot(som);
    }
}
