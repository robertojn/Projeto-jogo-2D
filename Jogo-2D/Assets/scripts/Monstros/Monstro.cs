using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UIElements;

public class Monstro : MonoBehaviour
{
    public int Vida;
    public int Pontos;
    public float DistanciaFlip;
    public float DistanciaAtaque;
    public bool chefao;
    public bool Voador;
    public Transform Player;
    public Transform groundCheck;
    public Transform paredeCheck;
    public LayerMask LayerParede;
    public CapsuleCollider2D col;
    public GameObject Efeito;
    public GameObject bauChefe;
    public Vector2 DistanciaVerDir;
    public Vector2 DistanciaVerEsq;
    public Vector3 EfeitoDir;
    public Vector3 EfeitoEsq;

    private Transform pos;
    private SpriteRenderer skin;
    private Rigidbody2D rig;
    private BoxCollider2D box;
    private Animator anim;
    private bool Vendo = false;
    private float TempoVer = 5f;
    private float count = 0;
    private bool Atacou = false;
    private GameObject[] Jogadores; 
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        skin = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        Jogadores = GameObject.FindGameObjectsWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Voador && Player == null && !Jogadores.All(x => x == null))
        {
            Player = Jogadores[Random.Range(0, Jogadores.Length)].transform;
        }

        if(!Vendo && chefao)
        {
            float x = transform.position.x;
            if(!skin.flipX)
            {
                x += 1*Time.deltaTime;
            } else {
                x -= 1*Time.deltaTime;
            }
            transform.position = new Vector3(x, transform.position.y);
        }

        if(ParedeCol() && chefao)
        {
            if(skin.flipX)
            {
                skin.flipX = false;
            } else {
                skin.flipX = true;
            }
        }

        if(!Atacou)
        {
            anim.SetBool("Bater", false);
        }
        
        if(skin.flipX == false && col != null)
        {
            col.offset = DistanciaVerDir;
        } 
        else if(col != null)
        {
            col.offset = DistanciaVerEsq;
        }

        if(Player != null)
        {
            if(Player.position.x > pos.position.x + DistanciaFlip)
            {
                float x = pos.position.x;
                x += 2 * Time.deltaTime;
                transform.position = new Vector2(x, pos.position.y);
                skin.flipX = false;
            } 
            else if(Player.position.x < pos.position.x - DistanciaFlip)
            {
                float x = pos.position.x;
                x -= 2 * Time.deltaTime;
                transform.position = new Vector2(x, pos.position.y);
                skin.flipX = true;
            }

            if(Player.position.y > pos.position.y && Voador)
            {
                float y = pos.position.y;
                y += 2 * Time.deltaTime;
                transform.position = new Vector2(pos.position.x, y);
            } else {
                float y = pos.position.y;
                y -= 2 * Time.deltaTime;
                transform.position = new Vector2(pos.position.x, y);
            }

            float distancia = Vector3.Distance(Player.position, pos.position);
            if(chefao)
            {
                Debug.Log(distancia);
            }
            if(distancia < DistanciaAtaque && !Atacou)
            {
                if(skin.flipX == false)
                {
                Efeito.GetComponent<SpriteRenderer>().flipX = true;
                GameObject golpe = Instantiate(Efeito, transform.position + EfeitoDir, Quaternion.identity);
                golpe.transform.SetParent(gameObject.transform);
                Atacou = true;
                StartCoroutine(tempoAtacar());
                } else {
                Efeito.GetComponent<SpriteRenderer>().flipX = false;
                GameObject golpe = Instantiate(Efeito, transform.position - new Vector3(EfeitoDir.x, -EfeitoDir.y), Quaternion.identity);
                golpe.transform.SetParent(gameObject.transform);
                Atacou = true;
                StartCoroutine(tempoAtacar());
                }
            }

            if(Player.position.y > pos.position.y + 2 && chaoCol() && !Voador)
            {
                rig.velocity = new Vector2(rig.velocity.x, 10f);
            }
            anim.SetBool("Andar", true);
        }

        if(!Vendo && Player != null)
        {
            count += 1 * Time.deltaTime;
        }

        if(count >= TempoVer && !Voador)
        {
            Player = null;
            count = TempoVer;
            anim.SetBool("Andar", false);
        }
    }

    public void perderVida(int dano)
    {
        Vida -= dano;
    }

    private bool chaoCol()
    {
        if(groundCheck != null)
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerParede);
        } else {
            return false;
        }
    }
    private bool ParedeCol()
    {
        if(paredeCheck != null)
        {
            return Physics2D.OverlapCircle(paredeCheck.position, 3f, LayerParede);
        } else {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D colide)
    {
        if(colide.gameObject.tag == "efeito" && !colide.transform.IsChildOf(gameObject.transform))
        {
            int dano = colide.gameObject.GetComponent<efeito>().dan;
            perderVida(dano);
            StartCoroutine(receberDano());
            Destroy(colide.gameObject);

            if(Vida <= 0)
            {
                Player script = colide.transform.GetComponentInParent<Player>();
                script.addDinheiro(Pontos);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colide)
    {
        if(colide.gameObject.tag == "player")
        {
            Player = colide.GetComponent<Transform>();
            Vendo = true;
            anim.SetBool("Alerta", true);
        } 
        else if(colide.gameObject.tag == "efeito")
        {
            if(colide.transform.parent.tag == "player")
            {
            int dano = colide.GetComponent<efeito>().dan;
            perderVida(dano);
            StartCoroutine(receberDano());
            
            if(skin.flipX == false)
            {
                rig.AddForce(new Vector2(rig.velocity.x - 200,rig.velocity.y));
                
            }else 
            {
                rig.AddForce(new Vector2(rig.velocity.x + 200,rig.velocity.y));
            }

            if(Vida <= 0)
            {
                Player script = colide.GetComponentInParent<Player>();
                script.addDinheiro(Pontos);
                Destroy(gameObject);
            }
            }
        }

    }

    private void OnTriggerStay2D(Collider2D colide)
    {
        if(colide.gameObject.tag == "inimigo")
        {
            Monstro script = colide.GetComponent<Monstro>();
            if(script.Vendo)
            {
                Player = script.Player;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D colide)
    {
        count = 0;
        Vendo = false;
    }

    private IEnumerator tempoAtacar()
    {
        anim.SetBool("Bater", true);
        yield return new WaitForSeconds(0.5f);
        Atacou = false;
    }

    private IEnumerator receberDano()
    {
        skin.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        skin.color = Color.white;
    }
}
