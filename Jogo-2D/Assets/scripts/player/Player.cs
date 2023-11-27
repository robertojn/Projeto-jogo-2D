using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int player;
    public float count = 0;
    public float speed;
    public float pulo;
    public float TempoAnim;
    public SpriteRenderer flip;
    public GameObject Pica;
    public Transform groundCheck;
    public LayerMask LayerParede;
    public KeyCode[] controles;
    public TextMeshProUGUI Score;

    private Rigidbody2D rig;
    private Animator anim;
    private bool PodePular = true;
    private bool dançando = false;
    private float forçaG = 10;
    private float dinheiro = 500;
    private float Jogar = 0;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        flip = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        Score.text = "Score: " + dinheiro + "g";
        

        if(Input.GetKeyDown(controles[0]) && PodePular)
        {
            rig.velocity = new Vector2(rig.velocity.x, pulo);
        }

        if(Input.GetKey(controles[1]))
        {
            Jogar += 2 * Time.deltaTime;

            if(Jogar >= 3)
            {
                GameObject Picareta = Instantiate(Pica, transform.position, Quaternion.identity); 
                Rigidbody2D rigPica = Picareta.GetComponent<Rigidbody2D>();

                rigPica.AddForce(new Vector2(forçaG, 0), ForceMode2D.Impulse);
                Jogar = 0;
            }
        } else {
            Jogar = 0;
        }

        if(Input.GetKeyDown(controles[2]))
        {
            anim.SetBool("Dance", true);
            dançando = true;
        }
    }
    void FixedUpdate()
    {
        float horizontal = 0;
        if(player == 1)
        {
            horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(horizontal, 0, 0);
        } 
        else if(player == 2)
        {
            horizontal = Input.GetAxis("Horizontal2") * speed * Time.deltaTime;
            transform.Translate(horizontal, 0, 0);
        }

        if(rig.velocity.x == 0)
        {
            count += 2 * Time.deltaTime;
        } 

        if(count >= TempoAnim && !dançando)
        {
            anim.SetBool("Idle", true);
        } else {
            anim.SetBool("Idle", false);
        }

        if(rig.velocity.y > 1)
        {
            PodePular = false;
            anim.SetBool("Pulou", true);
        }

        if(horizontal != 0 && PodePular)
        {
            anim.SetBool("Andar", true);
            anim.SetBool("Dance", false);
        } else {
            anim.SetBool("Andar", false);
        }

        if (horizontal < 0)
        {
            flip.flipX = false;
            forçaG = -10;
            count = 0;

        }else if (horizontal > 0 )
        {
            flip.flipX = true;
            forçaG = 10;
            count = 0;
        }    

        if(chaoCol())
        {
            PodePular = true;
            anim.SetBool("Pulou", false);
        }  else {
            PodePular = false;
        }
    }

    private bool chaoCol()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, LayerParede);
    }

    public void Animou()
    {
        anim.SetBool("Idle", false);
        count = 0;
    }

    public void addDinheiro(int pontos)
    {
        dinheiro += pontos;
    }
}
