using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monstro : MonoBehaviour
{
    public Transform Player;
    public Transform groundCheck;
    public LayerMask LayerParede;
    public CapsuleCollider2D col;

    private Transform pos;
    private SpriteRenderer skin;
    private Rigidbody2D rig;
    private bool Vendo = false;
    private float TempoVer = 5f;
    public float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        skin = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

            if(Player.position.y > pos.position.y + 2 && chaoCol())
            {
                rig.velocity = new Vector2(rig.velocity.x, 10f);
            }
        }

        if(!Vendo && Player != null)
        {
            count += 1 * Time.deltaTime;
        }

        if(count >= TempoVer)
        {
            Player = null;
            count = TempoVer;
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
}
