using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTumuraController : MonoBehaviour
{
    [Header("追従")][SerializeField] bool homing = false; //追従する場合
    Vector2 pos,diff;
    [Header("地上からの距離")][SerializeField] float dis = 100.0f;
    [Header("追従の力の大きさ")] [SerializeField] float f = 13.0f;

    float ve,maxf=110.0f;
    TumuraContloller TumuraContloller;
    Rigidbody2D rb;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        TumuraContloller = GetComponent<TumuraContloller>();
        rb = GetComponent<Rigidbody2D>();
        ve = -(TumuraContloller.walkspeed / (rb.mass * rb.drag)*1.1f); //最高速度の1.1倍
    }
    private void FixedUpdate()
    {
        if (homing && player!=null)
        {
            diff = (player.transform.position - transform.position); //TumuraからPlayerまでのベクトル
            
            if (diff.magnitude<2000.0f) //一定範囲内の場合
            {
                if (diff.magnitude > maxf) 
                {
                    diff = diff.normalized*maxf; 
                }
                rb.AddForce(diff * f);
                if (diff.x > 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

                }
            }
        }
        if (transform.eulerAngles.y == 180 && ve < 0 || transform.eulerAngles.y == 0 && ve > 0) //Tumuraが右向きかつveがマイナス または Tumuraが左向きかつveがプラス
        {
            ve = -ve;
        }
        pos = new Vector2(ve, -dis); //反転用の向きを作成
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - 16.5f); //FlyingTumuraの場所
        RaycastHit2D hit1 = Physics2D.Raycast(origin, Vector2.down, dis, LayerMask.GetMask("Stage"));
        RaycastHit2D hit2 = Physics2D.Raycast(origin, pos, pos.magnitude*1.2f, LayerMask.GetMask("Stage")); //回転判定用
        Debug.DrawRay(origin,new Vector2(0,-dis),Color.red,0.1f);
        Debug.DrawRay(origin, pos*1.2f, Color.yellow, 0.1f);

        if (hit1.collider)
        {
            rb.AddForce(new Vector2(0.0f, 1.0f) * rb.mass * rb.gravityScale * 15f, ForceMode2D.Force); //重力を打ち消す
        }
        if (hit2.collider == null && !homing)
        {
            TumuraContloller.Turn();
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
