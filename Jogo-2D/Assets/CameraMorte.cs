using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMorte : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = cam.rect.x;
                
        if(cam.rect.x > 0)
        {
            x += 0.3f * Time.deltaTime;
        } else {
            x -= 0.3f * Time.deltaTime;
        }
        
        cam.rect = new Rect(x, cam.rect.y, cam.rect.width, cam.rect.height);
        
        Destroy(gameObject, 3f);
    }
}
