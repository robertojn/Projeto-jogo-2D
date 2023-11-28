using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pontos : MonoBehaviour
{
    public int Pontos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "efeito")
        
            {
                Player valor = col.GetComponentInParent<Player>();
                Rigidbody2D Rig = col.GetComponentInParent<Rigidbody2D>();
                Transform Posi = col.GetComponentInParent<Transform>();
                SpriteRenderer personagem = col.GetComponentInParent<SpriteRenderer>();

                if(personagem.flipX == true)
                {
                Rig.AddForce(new Vector2(Posi.position.x-100,Posi.position.y));
                
                }else 
                {
                   Rig.AddForce(new Vector2(Posi.position.x+100,Posi.position.y));
                }
                valor.addDinheiro(Pontos);
                Destroy(gameObject);
            }
    }
}
