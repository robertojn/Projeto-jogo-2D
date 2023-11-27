using System.Collections;
using System.Collections.Generic;
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

    private Rigidbody2D rig;
    private Animator anim;
    private bool PodePular = true;
    private bool dançando = false;
    private float forçaG = 10;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        flip = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        if(Input.GetKeyDown(controles[0]) && PodePular)
        {
            rig.velocity = new Vector2(rig.velocity.x, pulo);
        }

        if(Input.GetKeyDown(controles[1]))
        {
            GameObject Picareta = Instantiate(Pica, transform.position, Quaternion.identity); 
            Rigidbody2D rigPica = Picareta.GetComponent<Rigidbody2D>();

            rigPica.AddForce(new Vector2(forçaG, 0), ForceMode2D.Impulse);
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
        }

        if(horizontal != 0)
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
}
