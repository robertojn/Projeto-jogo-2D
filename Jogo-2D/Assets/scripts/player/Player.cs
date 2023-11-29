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
    public float dinheiro = 500;
    public SpriteRenderer flip;
    public GameObject Pica;
    public Transform groundCheck;
    public Transform ParedeCheck;
    public LayerMask LayerParede;
    public TextMeshProUGUI Score;
    public KeyCode[] controles;
    public GameObject[] efeitos;

    private Rigidbody2D rig;
    private Animator anim;
    private bool PodePular = true;
    private bool dançando = false;
    private bool PodeJogar = false;
    private float forçaG = 10;
    public float Jogar = 0;
    public float TempoJogar = 0;
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
            TempoJogar += 2 * Time.deltaTime;
            Jogar += 2*Time.deltaTime;

            if(TempoJogar >= 3)
            {
                PodeJogar = true;
            }
        } else {
            TempoJogar = 0;
        }

        if(Input.GetKeyUp(controles[1]) && PodeJogar)
        {
            GameObject Picareta = Instantiate(Pica, transform.position, Quaternion.identity); 
            Rigidbody2D rigPica = Picareta.GetComponent<Rigidbody2D>();

            rigPica.AddForce(new Vector2(forçaG, 1*Jogar), ForceMode2D.Impulse);
            PodeJogar = false;
        }

        if(Input.GetKeyDown(controles[1]))
        {
            anim.SetBool("Golpe", true);
        } else {
            anim.SetBool("Golpe", false);
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
        }  
        else {
            PodePular = false;
        }

        if(ParedeCol())
        {
            anim.SetBool("Parede", true);
            PodePular = true;
        } else {
            anim.SetBool("Parede", false);
        }
    }

    private bool chaoCol()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerParede);
    }
    private bool ParedeCol()
    {
       return Physics2D.OverlapCircle(ParedeCheck.position, 0.2f, LayerParede);
    }

    public void Animou()
    {
        anim.SetBool("Idle", false);
        count = 0;
    }

    public void EfeitoGolpe()
    {
        if(flip.flipX)
        {
            efeitos[0].GetComponent<SpriteRenderer>().flipX = true;
            GameObject golpe = Instantiate(efeitos[0], transform.position + new Vector3(0.4f,-0.1f,0), Quaternion.identity);
            golpe.transform.SetParent(gameObject.transform);
        }
        else {
            efeitos[0].GetComponent<SpriteRenderer>().flipX = false;
            GameObject golpe = Instantiate(efeitos[0], transform.position - new Vector3(0.4f,+0.1f,0), Quaternion.identity);
            golpe.transform.SetParent(gameObject.transform);
        }
    }

    public void addDinheiro(int pontos)
    {
        dinheiro += pontos;
    }

    public void perderDinheiro(int quantidade)
    {
        dinheiro -= quantidade;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "efeito")
        {
            perderDinheiro(50);
        }
    }
}
