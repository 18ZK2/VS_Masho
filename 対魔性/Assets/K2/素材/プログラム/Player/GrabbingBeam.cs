using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingBeam : MonoBehaviour
{
    
    [SerializeField] int beamLength = 0;
    [SerializeField] float shotPow = 0, attackPt = 0.10f, whipPow = 10f, grabTime = 1.5f,enemyshotPow=100.0f;
    [SerializeField] GameObject element = null;
    [SerializeField] GameObject head = null;
    [SerializeField] Rigidbody2D armBody = null,playerBody = null;
    [SerializeField] PlayerContloller pc = null;

    List<GameObject> list = new List<GameObject>();
    GrabbingHead gHead;
    GameObject touchedEnemy,touchedObj;

    bool whipMode = false;
    
    Rigidbody2D headRigid;

    SpringJoint2D enemyJoint;

    IEnumerator gt;

    IEnumerator GrabTime()
    {
        yield return new WaitForSeconds(grabTime);
        DeleteBeams();
        yield break;
    }
    public void MakeBeams(Vector2 vec)
    {
        //ワイヤーを生成
        GameObject ele;
        SpringJoint2D eleJoint;
        Rigidbody2D eleBody = null;
        //ワイヤーの紐要素を作成し変数listに追加
        for (int i = 0; i < beamLength; i++)
        {


            //紐の要素を作り、後ろの要素と結合させる。

            ele = Instantiate(element, transform.position, Quaternion.identity);
            eleJoint = ele.GetComponent<SpringJoint2D>();
            eleBody = ele.GetComponent<Rigidbody2D>();

            ele.transform.parent = null;
            ele.name = i.ToString();
            list.Add(ele);

            if (i != 0 && list[i - 1]!=null)
            {
                Rigidbody2D beforeRigid = list[i - 1].GetComponent<Rigidbody2D>();
                eleJoint.connectedBody = beforeRigid;
                
            }
            else
            {
                eleJoint.connectedBody = armBody;
                eleJoint.autoConfigureDistance = false;

            }
        }

        //ワイヤーの他オブジェクトにくっつく部分を作成
        GameObject h = Instantiate(head, transform.position,Quaternion.identity);
        list.Add(h);
        h.transform.parent = null;
        gHead = h.GetComponent<GrabbingHead>();
        headRigid = h.GetComponent<Rigidbody2D>();
        //ワイヤーの末端に接続
        h.GetComponent<SpringJoint2D>().connectedBody = eleBody;
        //発射
        headRigid.AddForce(vec * shotPow, ForceMode2D.Impulse);

    }
    public void DeleteBeams()
    {
        if (list.Count < 1) return;
        whipMode = false;
        headRigid = null;
        StopCoroutine(gt);
        gt = null;gt = GrabTime();
        
        //エネミーの後処理
        if (touchedEnemy != null)
        {
            touchedEnemy.GetComponent<Animator>().enabled = true;
            touchedEnemy.GetComponent<EnemyContloller>().isDamage = true;
            Destroy(enemyJoint);
            //エネミー発射
            touchedEnemy.GetComponent<Rigidbody2D>().AddForce(pc.bodyVec * enemyshotPow, ForceMode2D.Impulse);
            touchedEnemy = null;
        }
        //ギミックギミックの後処理
        else if (touchedObj != null)
        {
            touchedObj.layer = 18;
            touchedObj = null;
        }
        //ワイヤーを削除
        foreach(GameObject g in list)
        {
            if (gameObject.name != g.gameObject.name) Destroy(g.gameObject);
        }
        list.Clear();
        gHead = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        gt = GrabTime();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gHead == null || list[0] == null) return;
        if (whipMode)
        {
            //ワイヤーの先をマウスポインタ―の方向へ動かせる
            headRigid.AddForce(pc.bodyVec * whipPow, ForceMode2D.Force);
        }
        if (gHead.touchedObject != null)
        {
            //グラップのヘッドは触れたゲームオブジェクトを送ってくる
            switch (gHead.touchedObject.tag)
            {
                //以下にワイヤーの末端が触れた時の挙動
                case "Stage":
                    //ステージとキャラを固定する
                    list[0].GetComponent<SpringJoint2D>().connectedBody = playerBody;
                    //末端を固定
                    headRigid.velocity = Vector2.zero;
                    headRigid.bodyType = RigidbodyType2D.Kinematic;
                    break;
                case "Enemy":

                    whipMode = true;

                    touchedEnemy = gHead.touchedObject;
                    gHead.touchedObject = null;
                    //触れた敵は自身の攻撃に
                    touchedEnemy.layer = 9;
                    touchedEnemy.tag = "PlayerAttack";
                    //攻撃
                    touchedEnemy.GetComponent<EnemyContloller>().Damage(attackPt);
                    //グラップと敵をつなぐ準備
                    enemyJoint = touchedEnemy.AddComponent<SpringJoint2D>();
                    enemyJoint.autoConfigureDistance = false;
                    enemyJoint.distance = 0.05f;
                    enemyJoint.frequency = gHead.GetComponent<SpringJoint2D>().frequency;
                    //グラップと敵を接続
                    enemyJoint.connectedBody = headRigid;
                    //アニメーションを止める
                    //目的はマヒさせたいだけ
                    //もっと普遍的な方法求む
                    touchedEnemy.GetComponent<Animator>().enabled = false;
                    //掴み可能時間設定
                    StartCoroutine(gt);

                    break;
                case "Gimmick":
                    //速度をゼロ
                    headRigid.velocity = Vector2.zero;
                    //重くして操作性を上げる
                    headRigid.mass *= 20f;
                    whipMode = true;
                    touchedObj = gHead.touchedObject;
                    gHead.touchedObject = null;
                    //ギミックへダメージ
                    touchedObj.GetComponent<GimmickContloller>().HP -= attackPt;
                    //触れた物だけ引き寄せられるレイヤーに
                    touchedObj.layer = 19;
                    //ヘッドの当たり判定をトリガーにして引き寄せるように
                    Collider2D col = gHead.GetComponent<Collider2D>();
                    col.isTrigger = true;
                    col.usedByEffector = true;
                    gHead.GetComponent<CircleCollider2D>().radius *= 20f;
                    Joint2D j = touchedObj.GetComponent<Joint2D>();
                    break;
                default:
                    DeleteBeams();
                    break;

            }
        }
        //  敵死亡時
        else if (touchedEnemy == null && touchedObj == null && gHead.touched)
        {
            DeleteBeams();
        }
        else if (gHead.exitObject != null && gHead.exitObject == touchedObj)
        {
            DeleteBeams();
        }
    }
}
