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
        if(col.gameObject.tag == "player")
        {
            Player script = col.GetComponent<Player>();
            if(Input.GetKeyDown(script.controles[1]))
            {
                script.addDinheiro(Pontos);
                Destroy(gameObject);
            }
        }
    }
}
