using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movimento : MonoBehaviour

    
{
    public float speed;
    public float pulo;
    public Transform ground;
    public SpriteRenderer flip;
    public GameObject Pica;
    public Transform ParedeCheck;
    public LayerMask LayerParede;

    private Rigidbody2D rig;
    public bool PodePular = true;
    private float forçaG = 10;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        flip = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        
            transform.Translate(horizontal, 0, 0);

        if(Input.GetKeyDown(KeyCode.Space) && PodePular)
        {
            rig.velocity = new Vector2(rig.velocity.x, pulo);
        }

        if(rig.velocity.y > 1)
        {
            PodePular = false;
        }


        if (horizontal < 0)
        {
            flip.flipX = false;
            forçaG = -10;

        }else if (horizontal > 0 )
        {
            flip.flipX = true;
            forçaG = 10;
        }    

        if(Input.GetKeyDown(KeyCode.G))
        {
            GameObject Picareta = Instantiate(Pica, transform.position, Quaternion.identity); 
            Rigidbody2D rigPica = Picareta.GetComponent<Rigidbody2D>();

            rigPica.AddForce(new Vector2(forçaG, 0), ForceMode2D.Impulse);
        }    
    }
    private void OnCollisionEnter2D(Collision2D Coll)
    {
        if(Coll.gameObject.tag == "chao")
        {
            PodePular = true;
        }
    }

    /*private bool ParedeCol()
    {
        return Physics2D.OverlapCircle(ParedeCheck.position, 0.1f, LayerParede);
    }*/
}
