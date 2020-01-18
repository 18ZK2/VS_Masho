using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRenderOrderContloller : MonoBehaviour
{
    [SerializeField] GameObject spear = null;
    [SerializeField] Transform boostPos = null;
    [SerializeField] GameObject[] effects = null;
    private SpriteRenderer[] SpearChildRenderers;
    // Start is called before the first frame update
    void Start()
    {
        SpearChildRenderers = spear.GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ParticleShot(int i)
    {
        GameObject effect = Instantiate(effects[i], boostPos.position, transform.rotation);
    }

    public void SetLayer(int order)
    {
        foreach (var cs in SpearChildRenderers)
        {
            cs.sortingOrder = order;
        }
    }
}
