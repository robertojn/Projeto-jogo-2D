using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimento : MonoBehaviour

    
{
    public float speed;
    public Transform ground;

    private Rigidbody2D rig;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(horizontal, 0, 0);

        if(Input.GetKey(KeyCode.Space))
        {
            rig.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        }

        if(transform.position.y > ground.position.y + 5)
        {
            rig.AddForce(new Vector2(0, 0));
        }
    }
}
