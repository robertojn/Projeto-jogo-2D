using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Saida : MonoBehaviour
{
    private bool PodeSair = false;
    private Player script;
    private EventoScript evento;
    // Start is called before the first frame update
    void Start()
    {
        evento = GameObject.Find("EventSystem").GetComponent<EventoScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PodeSair && script != null)
        {
            if(Input.GetKeyDown(script.controles["Bater"]))
            {
                evento.placar.Add(script.gameObject.name, script.dinheiro);
                Destroy(script.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        PodeSair = true;
        script = col.GetComponent<Player>();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        PodeSair = false;
        script = null;
    }
}
