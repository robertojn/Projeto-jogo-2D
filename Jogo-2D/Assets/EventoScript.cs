using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoScript : MonoBehaviour
{
    public GameObject[] Players;
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
            if(jogadores == null)
            {
                UmApenas = true;
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
    }
}
