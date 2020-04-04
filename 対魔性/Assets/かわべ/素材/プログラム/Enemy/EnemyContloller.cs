using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContloller : MonoBehaviour
{
    public float HP = 1.0f;
    [SerializeField] bool isGimickAttack = false;
    [System.NonSerialized] public bool isDamage = true;

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

    private IEnumerator Immortal()
    {
        isDamage = false;
        yield return new WaitForSeconds(immortalTime);
        isDamage = true;
        gameObject.tag = "Enemy";
        gameObject.layer = 10;
        StopCoroutine(Immortal());
    }

    //呼び出される
    public void Damage(float attackPow)
    {
        
        if (isDamage)
        {
            Debug.Log(attackPow);
            HP -= attackPow;
            HP = Mathf.Round(HP * 10.0f) / 10.0f; //10倍して四捨五入->10で割る
            if (asc != null && SE != null) asc.PlayOneShot(SE);
            StartCoroutine(Immortal());
        }

    }
    //破片用に汎用化しました
    public void MakeHahen(GameObject obj,GameObject hahen)
    {
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
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anm!=null)anm.SetBool("immortal", !isDamage);
        if (HP <= 0)
        {
            //死ぬとき
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
        rb.collisionDetectionMode = (gameObject.tag == "PlayerAttack") ? CollisionDetectionMode2D.Continuous : CollisionDetectionMode2D.Discrete;
    }

    private float dm;
    private Vector3 beforePos;
    private void FixedUpdate()
    {
        if (isDamage)
        {
            //微小距離
            dm = (transform.position - beforePos).magnitude;
            beforePos = transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerContloller pc = collision.gameObject.GetComponent<PlayerContloller>();
            pc.Damage(attackPt);
        }
        else if(isGimickAttack && collision.gameObject.tag == "Gimmick")
        {
            GimmickContloller gc = collision.gameObject.GetComponent<GimmickContloller>();
            Debug.Log("Attack_to_Gimmick_From_Enemy");
            gc.HP -= attackPt;
        }
        else if (gameObject.tag == "PlayerAttack")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float damagePow = rb.mass * dm / 50f;
            if (collision.gameObject.tag == "Enemy")
            {
                Debug.Log("Attack_to_Enemy_From_Enemy");
                collision.gameObject.GetComponent<EnemyContloller>().Damage(damagePow);
            }
            if (collision.gameObject.tag == "Gimmick")
            {
                Debug.Log("Attack_to_Gimmick_From_Enemy");
                GimmickContloller gc = collision.gameObject.GetComponent<GimmickContloller>();
                if (damagePow > HP) gc.HP -= HP;
                else gc.HP -= damagePow;
            }
            Damage(damagePow);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerContloller pc = collision.gameObject.GetComponentInParent<PlayerContloller>();
            pc.Damage(attackPt);
        }
    }
}
