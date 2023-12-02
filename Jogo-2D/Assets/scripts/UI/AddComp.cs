using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AddComp : MonoBehaviour
{
    public int PontosVerde;
    public int PontosVermelho;
    public int PontosAzul;
    public List<Transform> cristais;

    void Start()
    {
       for(int i = 0; i < transform.childCount; i++)
       {
            cristais.Add(gameObject.transform.GetChild(i));
       }


       foreach(Transform cris in cristais)
       {
            cris.gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            cris.gameObject.AddComponent<pontos>();
           
            SpriteRenderer cristalSprite = cris.GetComponent<SpriteRenderer>();
            
            if(cristalSprite.sprite.name == "Cristais_0")
            {
                cris.gameObject.name = "Verde";
            }
            else if(cristalSprite.sprite.name == "Cristais_1")
            {
                cris.gameObject.name = "Vermelho";
            }
            else if(cristalSprite.sprite.name == "Cristais_2")
            {
                cris.gameObject.name = "Azul";
            }


            if(cris.gameObject.name == "Verde")
            {
                cris.GetComponent<pontos>().Pontos = PontosVerde;
                cris.GetComponent<Light2D>().color = Color.green;
            } 
            else if(cris.gameObject.name == "Vermelho")
            {
                cris.GetComponent<pontos>().Pontos = PontosVermelho;
                cris.GetComponent<Light2D>().color = Color.red;
            } 
            else if(cris.gameObject.name == "Azul")
            {
                cris.GetComponent<pontos>().Pontos = PontosAzul;
                cris.GetComponent<Light2D>().color = Color.blue;
            }
       }
    }
}
