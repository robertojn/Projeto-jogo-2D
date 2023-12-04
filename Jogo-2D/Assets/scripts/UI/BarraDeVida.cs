using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Sprite[] barras;
    public Image render;
    public Player scriptPlayer;
    public int Dano;

    private int VidaAnterior;
    private int danoRecebido = 0;
    private int vidaRecebida = 0;
    private int numero = 0;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Image>();
        VidaAnterior = scriptPlayer.Vida;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.X))
        {
            scriptPlayer.perderVida(Dano);
        }
        render.sprite = barras[numero];

        danoRecebido = VidaAnterior - scriptPlayer.Vida;
        vidaRecebida = scriptPlayer.Vida - VidaAnterior;

        if(danoRecebido >= 10)
        {
            numero++;
            danoRecebido -= 10;
            VidaAnterior = scriptPlayer.Vida;
        }

        if(vidaRecebida >= 10)
        {
            numero--;
            vidaRecebida -= 10;
            VidaAnterior = scriptPlayer.Vida;
        }
    }
}
