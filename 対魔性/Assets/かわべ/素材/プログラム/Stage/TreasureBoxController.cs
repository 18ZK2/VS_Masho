using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBoxController : MonoBehaviour
{
    public bool canOpen = false;
    public GameObject Treasure = null;
    [SerializeField] AudioClip SE;
    [SerializeField] Sprite close = null, open = null;

    bool opened = false;
    SpriteRenderer sr;
    ParticleSystem ps;
    ParticleSystem.EmissionModule emmision;
    AudioSource ass;
    GimmickContloller gc;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
        ass = GetComponent<AudioSource>();
        gc = GetComponent<GimmickContloller>();
        sr.sprite = close;
        emmision = ps.emission;
        emmision.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (opened)
        {
            emmision.enabled = false;
        }
        else if (canOpen)
        {
            sr.sprite = open;
            emmision.enabled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!opened && collision.gameObject.tag == "Player")
        {
            opened = true;
            gc.HP = 1;
            if (Treasure != null)
            {
                ass.PlayOneShot(SE);
                Vector3 pos = transform.position + Vector3.up * 32;
                GameObject t = Instantiate(Treasure, pos, Quaternion.identity);
            }
        }
    }
}
