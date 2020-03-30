using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnerController : MonoBehaviour
{
    public bool isLaunch=false;

    [SerializeField] AudioClip SE;
    ParticleSystem ps;
    AudioSource ass;
    bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ass = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = ps.emission; //こうしないとできないらしい
        emission.enabled=isLaunch;
        if (isLaunch && !played)
        {
            ass.PlayOneShot(SE);
            played = true;
        }
        else if (!isLaunch)
        {
            played = false;
            ass.Stop();
        }
        if (!ass.isPlaying)
        {
            played = false;
        }
    }
}
