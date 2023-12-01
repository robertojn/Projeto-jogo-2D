using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bau : MonoBehaviour
{
    public List<String> enemies = new List<String>();
    public Sprite[] sprites;
    public int numeroInim = 0;
    public int Pontos;
    public bool PodePegar;
    private CapsuleCollider2D capsule;
    private SpriteRenderer skin;
    private Player script;
    // Start is called before the first frame update
    void Start()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        skin = GetComponent<SpriteRenderer>();
        numeroInim = enemies.Count;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }

        if(enemies.Count <= 0)
        {
            capsule.enabled = true;
        }

        if(PodePegar)
        {
            if(Input.GetKeyDown(script.controles[1]))
            {
                if(skin.sprite == sprites[0])
                {
                    skin.sprite = sprites[1];
                } else {
                    skin.sprite = sprites[2];
                    script.addDinheiro(Pontos);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "player")
        {
            script = col.GetComponent<Player>();
            PodePegar = true;
        }

        if(col.gameObject.tag == "inimigo")
        {
            if(!enemies.Contains(col.name))
            {
                enemies.Add(col.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "player")
        {
            script = null;
            PodePegar = false;
        }

        if(col.gameObject.tag == "inimigo")
        {
            enemies.Remove(col.name);
        }
    }
}
