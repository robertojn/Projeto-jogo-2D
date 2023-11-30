using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Monstro : MonoBehaviour
{
    public Transform Player;
    public Transform groundCheck;
    public LayerMask LayerParede;
    public CapsuleCollider2D col;
    public GameObject Efeito;

    private Transform pos;
    private SpriteRenderer skin;
    private Rigidbody2D rig;
    private Animator anim;
    private bool Vendo = false;
    private float TempoVer = 5f;
    private float count = 0;
    private bool Atacou = false;
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        skin = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Atacou)
        {
            anim.SetBool("Bater", false);
        }
        
        if(skin.flipX == false)
        {
            col.offset = new Vector2(3.8f, 0.7f);
        } else {
            col.offset = new Vector2(-3.8f, 0.7f);
        }

        if(Player != null)
        {
            if(Player.position.x > pos.position.x)
            {
                float x = pos.position.x;
                x += 2 * Time.deltaTime;
                transform.position = new Vector2(x, pos.position.y);
                skin.flipX = false;
            } else {
                float x = pos.position.x;
                x -= 2 * Time.deltaTime;
                transform.position = new Vector2(x, pos.position.y);
                skin.flipX = true;
            }

            if(skin.flipX == false)
            {
                if(Player.position.x < pos.position.x + 2 && !Atacou)
                {
                    Efeito.GetComponent<SpriteRenderer>().flipX = true;
                    GameObject golpe = Instantiate(Efeito, transform.position + new Vector3(0.4f,-0.1f,0), Quaternion.identity);
                    golpe.transform.SetParent(gameObject.transform);
                    Atacou = true;
                    StartCoroutine(tempoAtacar());
                }
            } else {
                if(Player.position.x > pos.position.x - 2 && !Atacou)
                {
                    Efeito.GetComponent<SpriteRenderer>().flipX = false;
                    GameObject golpe = Instantiate(Efeito, transform.position - new Vector3(0.4f,+0.1f,0), Quaternion.identity);
                    golpe.transform.SetParent(gameObject.transform);
                    Atacou = true;
                    StartCoroutine(tempoAtacar());
                }
            }

            if(Player.position.y > pos.position.y + 2 && chaoCol())
            {
                rig.velocity = new Vector2(rig.velocity.x, 10f);
            }
            anim.SetBool("Andar", true);
        }

        if(!Vendo && Player != null)
        {
            count += 1 * Time.deltaTime;
        }

        if(count >= TempoVer)
        {
            Player = null;
            count = TempoVer;
            anim.SetBool("Andar", false);
        }
    }

    private bool chaoCol()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerParede);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "player")
        {
            Player = col.GetComponent<Transform>();
            Vendo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
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
}
