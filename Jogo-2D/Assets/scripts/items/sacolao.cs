using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sacolao : MonoBehaviour
{
    public GameObject Player;
    public Vector3 esq;
    public Vector3 dir;
    public Vector3 rot;
    public Sprite[] estado;

    private float z;
    private SpriteRenderer bag;
    // Start is called before the first frame update
    void Start()
    {
        bag = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer spritePlayer = Player.GetComponent<SpriteRenderer>();
        Player script = Player.GetComponent<Player>();

        //girar e flipar sprite
        if(spritePlayer.flipX == true)
        {
            transform.localPosition = esq;
            z = 41f;
        } 
        else if(spritePlayer.flipX == false)
        {
            transform.localPosition = dir;
            z = -41f;
        }

        rot = new Vector3(transform.localRotation.x, transform.localRotation.y, z);
        transform.localRotation = Quaternion.Euler(rot);
        
        //mudar sprite de acordo com o dinheiro do player
        if(script.dinheiro < 1000)
        {
            bag.sprite = estado[0];
        }
        else if(script.dinheiro < 1500)
        {
            bag.sprite = estado[1];
        }
        else if(script.dinheiro >= 1500)
        {
            bag.sprite = estado[2];
        }
        
    }
}
