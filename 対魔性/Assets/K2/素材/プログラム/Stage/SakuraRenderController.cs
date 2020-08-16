using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SakuraRenderController : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystemRenderer pr;
    Renderer r;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    
            
    }
    private void OnBecameInvisible()
    {
        var e = ps.emission;
        e.enabled = false;
    }
    private void OnBecameVisible()
    {
        var e = ps.emission;
        e.enabled = true;
    }
}
