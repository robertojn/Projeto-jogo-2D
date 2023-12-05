using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : MonoBehaviour
{

    public GameObject MenuJogo;
    public Slider MusicControl;
    public Slider SFXControl;
    private AudioSource music;
    private AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        sfx = GameObject.Find("SFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !MenuJogo.activeSelf)
        {
            MenuJogo.SetActive(true);
        } else if(Input.GetKeyDown(KeyCode.Escape) && MenuJogo.activeSelf){
            MenuJogo.SetActive(false);
        }

        music.volume = MusicControl.value;
        sfx.volume = SFXControl.value;
    }
}
