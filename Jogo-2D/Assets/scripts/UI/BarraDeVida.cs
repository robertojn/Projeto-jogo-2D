using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVida : MonoBehaviour
{
    public Sprite[] barras;
    public SpriteRenderer render;
    public Player scriptPlayer;

    private int VidaAnterior;
    private int numero = 0;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        VidaAnterior = scriptPlayer.Vida;
        render.sprite = barras[0];
    }

    // Update is called once per frame
    void Update()
    {
        switch(scriptPlayer.Vida)
        {
            case 150:
            break;
            case 140:
            break;
            case 130:
            break;
            case 120:
            break;
            case 110:
            break;
            case 100:
            break;
            case 90:
            break;
            case 80:
            break;
            case 70:
            break;
            case 60:
            break;
            case 50:
            break;
            case 40:
            break;
            case 30:
            break;
            case 20:
            break;
            case 10:
            break;
            case 0:
            break;
        }
    }
}
