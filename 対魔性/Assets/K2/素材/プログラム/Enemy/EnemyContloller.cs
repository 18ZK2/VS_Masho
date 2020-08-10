using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyContloller : MonoBehaviour
{
    public float HP = 1.0f;
    [SerializeField] bool isGimickAttack = false;
    [System.NonSerialized] public bool isDamage = true;
    [System.NonSerialized] public GameObject playerAttackEffect;

    //当たり判定のモードを設定
    [SerializeField] bool CollisionMode = false;
    //当たり判定がトリガーでもダメージが入るか
    [SerializeField] bool damageisTrigger = false;
    //接触ダメージ
    [SerializeField] float attackPt = 1f;
    //ダメージ後の無敵時間
    [SerializeField] float immortalTime = 0.5f;
    
    //必ず落とす
    [SerializeField] GameObject mustDrop = null;
    //確率で落とす
    [SerializeField] GameObject[] dropItem = null;
    //dropProba が ランダムな値0~1より大きいとドロップ
    [SerializeField] float[] dropProba = null;

    [SerializeField] AudioClip SE = null;

    AudioSource asc;
    Animator anm;
    Rigidbody2D rb;
    float maxHp;

    IEnumerator imm;

    //無敵時間
    private IEnumerator Immortal()
    {
        isDamage = false;
        yield return new WaitForSeconds(immortalTime);
        isDamage = true;
        gameObject.tag = "Enemy";
        gameObject.layer = 10;
        var p = playerAttackEffect.GetComponent<ParticleSystem>();
        var e = p.emission;
        e.enabled = false;
        StopCoroutine(imm);
        imm = null;
        
    }

    //呼び出されるとダメージ+無敵時間発生
    public void Damage(float attackPow)
    {

        if (isDamage)
        {
            HP -= attackPow;
            HP = Mathf.Round(HP * 10.0f) / 10.0f; //10倍して四捨五入->10で割る
            if (asc != null && SE != null) asc.PlayOneShot(SE);
            imm = Immortal();
            StartCoroutine(imm);
        }

    }
    //破片用に汎用化しました
    public void MakeHahen(GameObject obj,GameObject hahen)
    {
        //自分の画像をすべて取得し、パーティクルとしてばらまく
        GameObject h = null;
        if (hahen != null)
        {
            h = Instantiate(hahen, transform.position, Quaternion.identity);
            HahenParticle p = h.GetComponent<HahenParticle>();
            h.transform.parent = null;
            if (p != null)
            {
                SpriteRenderer[] sps = gameObject.GetComponentsInChildren<SpriteRenderer>();
                Sprite[] sprites = new Sprite[sps.Length];
                for (int i = 0; i < sps.Length; i++)
                {
                    sprites[i] = sps[i].sprite;
                }
                p.Sprites = sprites;
                p.layername = sps[0].sortingLayerName;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        asc = GetComponent<AudioSource>();
        anm = GetComponent<Animator>();
        if(anm!=null)anm.keepAnimatorControllerStateOnDisable = true;
        rb = GetComponent<Rigidbody2D>();
        maxHp = HP;
        playerAttackEffect = Instantiate((GameObject)Resources.Load("弾丸化エフェクト"), transform.position, Quaternion.identity);
        playerAttackEffect.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(anm!=null)anm.SetBool("immortal", !isDamage);
        if (HP <= 0)
        {
            //死ぬとき
            Destroy(playerAttackEffect, 3f);
            playerAttackEffect.transform.parent = null;
            if (dropItem.Length != 0)//ドロップアイテムが設定されているとき
            {
                int i = Random.Range(0, dropItem.Length);//落とすアイテムの種類を抽選
                bool isDrop = dropProba[i] > Random.Range(0f, 1f);//落とすかどうかを抽選
                if (isDrop) Instantiate(dropItem[i], transform).transform.parent = null;
            }
            if (mustDrop != null)
            {
                MakeHahen(gameObject, mustDrop);
            }
            Destroy(gameObject);
        }
        if (rb != null)
        {
            rb.collisionDetectionMode =
                (gameObject.tag == "PlayerAttack" || CollisionMode) ?
                CollisionDetectionMode2D.Continuous : CollisionDetectionMode2D.Discrete;
        }
    }

    private float dm;
    private Vector3 beforePos;
    private void FixedUpdate()
    {
        if (isDamage)
        {
            //微小距離　ぶつかったときの衝撃を計算するときに使う
            dm = (transform.position - beforePos).magnitude;
            beforePos = transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //グラップに捕まった時
        if (gameObject.tag == "PlayerAttack")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //衝撃は1/25されています
            float damagePow = rb.mass * dm / 25f;
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyContloller>().Damage(damagePow);
            }
            else if (collision.gameObject.tag == "Gimmick")
            {

                GimmickContloller gc = collision.gameObject.GetComponent<GimmickContloller>();
                if (damagePow > HP) gc.HP -= HP;
                else gc.HP -= damagePow;
            }
            Damage(damagePow);
        }
        //接触ダメージ
        else if (collision.gameObject.tag == "Player")
        {
            PlayerContloller pc = collision.gameObject.GetComponent<PlayerContloller>();
            pc.Damage(attackPt);
            Debug.Log("attack");
            
        }
        //ギミックにダメージが入る設定
        else if(isGimickAttack && collision.gameObject.tag == "Gimmick")
        {
            GimmickContloller gc = collision.gameObject.GetComponent<GimmickContloller>();
            gc.HP -= attackPt;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //当たり判定がトリガーでもダメージが入るとき
        if (damageisTrigger && collision.gameObject.tag == "Player")
        {
            Debug.Log("koko");
            PlayerContloller pc = collision.gameObject.GetComponentInParent<PlayerContloller>();
            pc.Damage(attackPt);
        }
    }
}
