using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRenderOrderContloller : MonoBehaviour
{
    [SerializeField] GameObject spear = null;
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
    public void SetLayer(int order)
    {
        foreach (var cs in SpearChildRenderers)
        {
            cs.sortingOrder = order;
        }
    }
}
