using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SakuraRenderController : MonoBehaviour
{
    ParticleSystem ps;
    Renderer r;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        r = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
    
            if (r.isVisible)
            {
                var e = ps.emission;
                e.enabled = true;
                //ps.emission = e;
                Debug.Log("表示中");
            }
            else
            {
                var e = ps.emission;
                e.enabled = false;
                Debug.Log("非表示中");
            }
    }
}
