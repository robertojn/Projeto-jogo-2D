using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddComp : MonoBehaviour
{
    public int PontosVerde;
    public int PontosVermelho;
    public int PontosAzul;
    public GameObject[] cristais;

    void Start()
    {
       foreach(GameObject cris in cristais)
       {
            cris.AddComponent<BoxCollider2D>().isTrigger = true;
            cris.AddComponent<pontos>();
            
            if(cris.gameObject.name == "Verde")
            {
                cris.GetComponent<pontos>().Pontos = PontosVerde;
            } 
            else if(cris.gameObject.name == "Vermelho")
            {
                cris.GetComponent<pontos>().Pontos = PontosVermelho;
            } 
            else if(cris.gameObject.name == "Azul")
            {
                cris.GetComponent<pontos>().Pontos = PontosAzul;
            }
       }
    }
}
