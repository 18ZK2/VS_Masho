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
    [Header("管理番号(大切なアイテム以外は0)")] public int num = 0;
    [Header("代わりのアイテム")] [SerializeField] GameObject Tr = null;
    bool opened = false;
    [System.NonSerialized] public bool oneopened = false;
    ButtonController bc;
    SpriteRenderer sr;
    ParticleSystem ps;
    ParticleSystem.EmissionModule emmision;
    AudioSource ass;
    GimmickContloller gc;

    // Start is called before the first frame update
    void Start()
    {

        if (oneopened&&Tr!=null) 
        {
            Treasure = Tr;//代わりのアイテムを代入
        }
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
                if (num != 0&&!oneopened) //管理番号が0でなく一度も開いていない場合
                {
                    GameManager_ gm= GameObject.Find("GameManager").GetComponent<GameManager_>();
                    gm.Save(num+8,"True");
                    oneopened = true;
                }
                opened = true;
                ass.PlayOneShot(SE);
                Vector3 pos = transform.position + Vector3.up * 32;
                GameObject t = Instantiate(Treasure, pos, Quaternion.identity);
            }
        }
    }
}
