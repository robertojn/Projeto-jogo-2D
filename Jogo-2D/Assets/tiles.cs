using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using Unity.Mathematics;

public class tiles : MonoBehaviour
{
    public Tilemap tile;
    public GameObject sombra;
    // Start is called before the first frame update
    void Start()
    {
        tile = GetComponent<Tilemap>();
        BoundsInt bounds = tile.cellBounds;
        TileBase[] todos = tile.GetTilesBlock(bounds);

        foreach(TileBase t in todos)
        {
            //GameObject s = Instantiate(sombra, transform.position, quaternion.identity);
            s.transform.SetParent(sombra.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
