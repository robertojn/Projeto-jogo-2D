using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class EventoScript : MonoBehaviour
{
    public Dictionary<string, float> placar = new Dictionary<string, float>();
    public List<GameObject> jog = new List<GameObject>();
    public GameObject[] Players;
    public Vector3 podio;
    public TextMeshProUGUI vencer;
    public Camera camFinal;
    public bool UmApenas = false;
    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject jogadores in Players)
        {
            if(!jog.Contains(jogadores))
            {
                jog.Add(jogadores);
            }
        }

        foreach(GameObject jogadores in jog.ToList())
        {
            if(jogadores == null)
            {
                jog.Remove(jogadores);
            }

            if(jogadores != null && UmApenas)
            {
                Camera cam = jogadores.GetComponentInChildren<Camera>();
                float x = cam.rect.x;

                if(cam.rect.x > 0)
                {
                    x -= 0.3f * Time.deltaTime;
                    if(x <= 0)
                    {
                        x = 0;
                    }
                } else {
                    x += 0.3f * Time.deltaTime;
                    if(x >= 0)
                    {
                        x = 0;
                    }
                }

                cam.rect = new Rect(x, cam.rect.y, cam.rect.width, cam.rect.height);
            }
        }

        if(jog.Count < Players.Length)
        {
            UmApenas = true;
        }

        if(jog.Count < 1)
        {
            var primeiro = Vencedor(placar);
            vencer.text = primeiro.Key + " OBTEVE: " + primeiro.Value + "pts E GANHOU!!";
            camFinal.gameObject.SetActive(true);
        }
    }

     static KeyValuePair<string, float> Vencedor(Dictionary<string, float> placar)
    {
        var Jogadores = placar.OrderByDescending(x => x.Value);
        var primeiro = Jogadores.First();
        return primeiro;
    }
}
