using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceController : MonoBehaviour
{
    public bool isFence = true;

    [SerializeField] float damage = 2.5f, burstPow = 1000f;
    [SerializeField] FenceController pair = null;


    bool broken = false,brokenPair = false;
    Transform capcel,brokenCap, particle,Player;
    Transform pairCapcel;
    LineRenderer line;
    PolygonCollider2D polygon;
    Bounds b;
    // Start is called before the first frame update
    void Start()
    {
        capcel = transform.Find("カプセル");
        brokenCap = transform.Find("割れたカプセル");
        particle = transform.Find("カプセルの破片");
        Player = GameObject.Find("Player").transform;

        

        line = GetComponentInChildren<LineRenderer>();
        polygon = GetComponentInChildren<PolygonCollider2D>();
        //ラインの始点を設定
        line.SetPosition(0, transform.position);
        //ラインの終点を設定
        line.SetPosition(1, transform.position);
        if (pair != null)
        {
            pairCapcel = pair.transform.Find("カプセル");
            line.SetPosition(1, pair.transform.position);
        }
        else
        {
            Destroy(capcel);
        }
        //当たり判定をラインから取得　　　斜めとかにするの厳禁
        b = line.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (capcel != null && b.Contains(Player.position))
        {
            //プレイヤーが当たった

            //ダメージ
            PlayerContloller p = Player.GetComponent<PlayerContloller>();
            Rigidbody2D prb = Player.GetComponent<Rigidbody2D>();
            p.Damage(damage);

            //吹っ飛ばす
            Vector2 vec = transform.position - pair.transform.position;
            Vector2 diff = Player.position - b.center;
            Vector2 burstVec = Vector2.zero;

            //上下
            if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
            {

                burstVec = Vector2.up * diff.y;
            }
            //左右
            else
            {
                burstVec = Vector2.right * diff.x;
            }

            prb.AddForce(burstVec.normalized * burstPow, ForceMode2D.Impulse);
        }
        if (capcel == null && !broken)
        {
            //こっちがやられた
            broken = true;
            brokenCap.gameObject.SetActive(true);
            particle.gameObject.SetActive(true);
        }
        if(pairCapcel == null && capcel!=null && !brokenPair)
        {
            //向こうがやられた
            brokenPair = true;
            Destroy(capcel.gameObject);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(b.center, b.size);
    }
}
