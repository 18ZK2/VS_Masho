using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HahenParticle : MonoBehaviour
{
    public string layername;
    public Sprite[] Sprites;
    ParticleSystem ps;
    new ParticleSystemRenderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        renderer = GetComponent<ParticleSystemRenderer>();
        ps.startSize = Sprites[0].bounds.size.x;
        renderer.sortingLayerName = layername;
        foreach (var s in Sprites)
            ps.textureSheetAnimation.AddSprite(s);
        ps.emission.SetBurst(0, new ParticleSystem.Burst(0, Sprites.Length));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
