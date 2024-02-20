using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : MonoBehaviour
{

    public GameObject MenuJogo;
    public GameObject MenuControles;
    public Slider MusicControl;
    public Slider SFXControl;
    public TextMeshProUGUI[] botões;
    private AudioSource music;
    private AudioSource sfx;
    private GameObject[] jogs;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        sfx = GameObject.Find("SFX").GetComponent<AudioSource>();
        jogs = GameObject.FindGameObjectsWithTag("player");

        foreach(TextMeshProUGUI t in botões)
        {
            Button b = t.GetComponentInParent<Button>();
            b.onClick.AddListener(() => Keybind1(b));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < botões.Length; i++)
        {
            if(botões[i].text == "Aguardando...")
            {
                foreach(KeyCode key in Enum.GetValues(typeof(KeyCode)))
                {
                    if(Input.GetKey(key))
                    {
                    botões[i].text = key.ToString();
                    jogs[1].GetComponent<Player>().controles;
                    jogs[1].GetComponent<Player>().controles.Add("Pular", key);
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !MenuJogo.activeSelf)
        {
            MenuJogo.SetActive(true);
        } else if(Input.GetKeyDown(KeyCode.Escape) && MenuJogo.activeSelf){
            MenuJogo.SetActive(false);
        }

        music.volume = MusicControl.value;
        sfx.volume = SFXControl.value;
    }

    public void Controles()
    {
        MenuControles.SetActive(true);
        MenuJogo.SetActive(false);
    }
    public void Voltar()
    {
        MenuControles.SetActive(false);
        MenuJogo.SetActive(true);
    }

    public void Keybind1(Button botãoclicado)
    {
        
        TextMeshProUGUI text = botãoclicado.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "Aguardando..."; 
    }
}
