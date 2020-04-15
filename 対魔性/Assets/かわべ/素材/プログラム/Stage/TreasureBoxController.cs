using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBoxController : MonoBehaviour
{
    public bool canOpen = false;
    public GameObject Treasure = null;
    [SerializeField] AudioClip SE = null;
    [SerializeField] Sprite close = null, open = null;
    [Header("ボタンと連動")][SerializeField] GameObject button = null; 
    bool opened = false;
    ButtonController bc;
    SpriteRenderer sr;
    ParticleSystem ps;
    ParticleSystem.EmissionModule emmision;
    AudioSource ass;
    GimmickContloller gc;

    // Start is called before the first frame update
    void Start()
    {
        if (button == null) canOpen = true;
        else
        {
            canOpen = false;
            bc = button.GetComponent<ButtonController>();
        }
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
        if (bc != null && bc.on == true) canOpen = true;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canOpen && collision.gameObject.tag == "Player")
        {
            gc.HP = 1;
            if (!opened && Treasure != null)
            {
                opened = true;
                ass.PlayOneShot(SE);
                Vector3 pos = transform.position + Vector3.up * 32;
                GameObject t = Instantiate(Treasure, pos, Quaternion.identity);
            }
        }
    }
}
