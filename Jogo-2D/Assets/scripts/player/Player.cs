using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int player;
    public int Vida;
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
    public Camera cam;

    private Rigidbody2D rig;
    private Animator anim;
    private bool PodePular = true;
    private bool dançando = false;
    private bool PodeJogar = false;
    private bool Atacou;
    private float forçaG = 10;
    public float Jogar = 0;
    public float TempoJogar = 0;
    private float TempoAtaque = 0;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        flip = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        Score.text = "Score: " + dinheiro;

        if(Input.GetKeyDown(controles[0]) && PodePular)
        {
            rig.velocity = new Vector2(rig.velocity.x, pulo);
        }

        if(Input.GetKey(controles[1]))
        {
            TempoJogar += 2 * Time.deltaTime;
            Jogar += 2*Time.deltaTime;

            if(TempoJogar >= 1)
            {
                PodeJogar = true;
                anim.SetBool("Jogou", true);
                cam.orthographicSize += 1*Time.deltaTime;
            }
        } else {
            TempoJogar = 0;
        }

        if(Input.GetKeyUp(controles[1]) && PodeJogar)
        {
            GameObject Picareta = Instantiate(Pica, transform.position, Quaternion.identity); 
            Rigidbody2D rigPica = Picareta.GetComponent<Rigidbody2D>();

            Physics2D.IgnoreCollision(Picareta.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
            Picareta.transform.SetParent(gameObject.transform);
            rigPica.AddForce(new Vector2(forçaG, 2*Jogar), ForceMode2D.Impulse);
            anim.SetBool("Jogou", false);
            PodeJogar = false;
            Jogar = 0;
        }

        
        if(Input.GetKey(controles[1]) && !Atacou && TempoJogar <= 1)
        {
            anim.SetBool("Golpe", true);
            Atacou = true;
            Jogar = 0;
        } else {
            anim.SetBool("Golpe", false);
        }

        if(Atacou)
        {
            TempoAtaque += Time.deltaTime;
            if(TempoAtaque >= 1)
            {
                Atacou = false;
            }
        }

        if(Input.GetKeyDown(controles[2]))
        {
            anim.SetBool("Dance", true);
            dançando = true;
        }

        if(Jogar > 10)
        {
            Jogar = 10;
        }

        if(cam.orthographicSize > 7)
        {
            cam.orthographicSize = 7;
        } 
        else if(cam.orthographicSize < 5)
        {
            cam.orthographicSize = 5;
        }

        if(cam.orthographicSize > 5 && !PodeJogar)
        {
            cam.orthographicSize -= 2*Time.deltaTime;
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
    public void perderVida(int Dano)
    {
        Vida -= Dano;
        StartCoroutine(TempoDano());
        if(Vida <= 0)
        {
            Destroy(gameObject);
            cam.transform.SetParent(null);
            cam.AddComponent<CameraMorte>();
        }
    }

    public void perderDinheiro(int quantidade)
    {
        dinheiro -= quantidade;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "efeito" && !col.transform.IsChildOf(gameObject.transform))
        {
            perderDinheiro(50);
            int dano = col.GetComponent<efeito>().dan;
            perderVida(dano);

            if(flip.flipX == true)
            {
                rig.AddForce(new Vector2(rig.velocity.x - 200,rig.velocity.y));
                
            }else 
            {
                rig.AddForce(new Vector2(rig.velocity.x + 200,rig.velocity.y));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "efeito" && !col.transform.IsChildOf(gameObject.transform))
        {
            perderDinheiro(50);
            int dano = col.gameObject.GetComponent<efeito>().dan;
            perderVida(dano);
        }
    }

    private IEnumerator TempoDano()
    {
        flip.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        flip.color = Color.white;
    }
}
