using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventoScript : MonoBehaviour
{
    public Dictionary<string, float> placar = new Dictionary<string, float>();
    public List<GameObject> jog = new List<GameObject>();
    public GameObject[] Players;
    public Vector3 podio;
    public TextMeshProUGUI vencer;
    public GameObject camVencedor;
    public GameObject camGameOver;
    public ParticleSystem particulas;
    public bool UmApenas = false;

    [Header("Inimigo")]
    public Transform SpawnX;
    public Transform SpawnY;
    public GameObject Morcego;
    private float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject jogadores in Players)
        {
            if(!jog.Contains(jogadores))
            {
                jog.Add(jogadores);
            }
        }

        foreach(GameObject jogadores in jog.ToList())
        {
            if(jogadores == null)
            {
                jog.Remove(jogadores);
            }

            if(jogadores != null && UmApenas)
            {
                Camera cam = jogadores.GetComponentInChildren<Camera>();
                float x = cam.rect.x;

                if(cam.rect.x > 0)
                {
                    x -= 0.3f * Time.deltaTime;
                    if(x <= 0)
                    {
                        x = 0;
                    }
                } else {
                    x += 0.3f * Time.deltaTime;
                    if(x >= 0)
                    {
                        x = 0;
                    }
                }

                cam.rect = new Rect(x, cam.rect.y, cam.rect.width, cam.rect.height);
            }
        }

        if(jog.Count < Players.Length)
        {
            UmApenas = true;
        }

        if(jog.Count < 1)
        {
            var vencedor = Vencedor(placar);
            if(vencedor.HasValue)
            {
                vencer.text = vencedor.Value.Key + " OBTEVE: " + vencedor.Value.Value + " PONTOS E GANHOU!!";
                camVencedor.gameObject.SetActive(true);
            } else {
                camGameOver.gameObject.SetActive(true);
            }
        }

        Vector3 posSpawn = new Vector3(Random.Range(-40f, SpawnX.position.x), Random.Range(0f, SpawnY.position.y));
        count += 1*Time.deltaTime;
        if(count >= 10)
        {
            GameObject mor = Instantiate(Morcego, posSpawn, Quaternion.identity);
            mor.transform.SetParent(GameObject.Find("Inimigos").transform);
            count = 0;
        }
    }

     static KeyValuePair<string, float>? Vencedor(Dictionary<string, float> placar)
    {
        var Jogadores = placar.OrderByDescending(x => x.Value);

        if(Jogadores.Any())
        {
            var primeiro = Jogadores.First();
            return primeiro;
        } else {
            return null;
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Jogo");
        camGameOver.gameObject.SetActive(false);
    }
}
