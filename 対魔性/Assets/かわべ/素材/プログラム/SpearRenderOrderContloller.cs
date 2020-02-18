using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRenderOrderContloller : MonoBehaviour
{
    [SerializeField] const int effectSize = 3;
    [SerializeField] GameObject spear = null;
    [SerializeField] Transform[] effectPos = new Transform[effectSize];
    [SerializeField] GameObject[] effects = new GameObject[effectSize];
    private SpriteRenderer[] SpearChildRenderers;
    // Start is called before the first frame update
    void Start()
    {
        if(spear!=null) SpearChildRenderers = spear.GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ParticleShot(int i)
    {
        if (i < effectSize) Instantiate(effects[i], effectPos[i].position, transform.rotation);
    }
}