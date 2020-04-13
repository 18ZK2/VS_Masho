using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBoxController : MonoBehaviour
{
    public bool canOpen = false;
    public GameObject Treasure = null;
    [SerializeField] Sprite close = null, open = null;

    bool opened = false;
    SpriteRenderer sr;
    ParticleSystem ps;
    ParticleSystem.EmissionModule emmision;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();

        sr.sprite = close;
        emmision = ps.emission;
        emmision.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen)
        {
            sr.sprite = open;
            emmision.enabled = true;
        }else if (opened)
        {
            emmision.enabled = false;
        }
    }
}
