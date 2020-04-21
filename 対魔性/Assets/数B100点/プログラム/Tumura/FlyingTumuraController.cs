using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTumuraController : MonoBehaviour
{
    [System.NonSerialized] public HomingTumura homing;
    [Header("地上からの距離")][SerializeField] float dis = 100.0f;
    bool exit = true;
    TumuraContloller TumuraContloller;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        homing = GetComponent<HomingTumura>();
        TumuraContloller = GetComponent<TumuraContloller>();
        rb = GetComponent<Rigidbody2D>();
        //ve = -(TumuraContloller.walkspeed / (rb.mass * rb.drag)*1.1f); //最高速度の1.1倍
    }
    private void FixedUpdate()
    {
        //if (transform.eulerAngles.y == 180 && ve < 0 || transform.eulerAngles.y == 0 && ve > 0) //Tumuraが右向きかつveがマイナス または Tumuraが左向きかつveがプラス
        //{
        //    ve = -ve;
        //}
       // pos = new Vector2(ve, -dis); //反転用の向きを作成
        Vector2 origin = new Vector2(transform.position.x, transform.position.y); //FlyingTumuraの場所(浮遊用)
        Vector2 Rorigin = new Vector2(transform.position.x+transform.right.x*-10.0f, transform.position.y - 16.5f); //FlyingTumuraの場所(回転用)
        RaycastHit2D hit1 = Physics2D.Raycast(origin, Vector2.down, dis, LayerMask.GetMask("Stage"));
        //RaycastHit2D hit2 = Physics2D.Raycast(origin, pos, pos.magnitude*1.2f, LayerMask.GetMask("Stage")); //回転判定用
        RaycastHit2D hit2 = Physics2D.Raycast(Rorigin, Vector2.down, dis * 2.1f, LayerMask.GetMask("Stage")); //回転判定用
        Debug.DrawRay(origin,new Vector2(0,-dis),Color.red,0.1f);
        //Debug.DrawRay(origin, pos*1.2f, Color.yellow, 0.1f);
        Debug.DrawRay(Rorigin, Vector2.down*2.1f*dis, Color.yellow, 0.1f);
        if (hit1.collider)
        {
            rb.AddForce(new Vector2(0.0f, 1.0f) * rb.mass * rb.gravityScale * 16f, ForceMode2D.Force); //重力を打ち消す
        }
        if (hit2.collider &&homing==null) exit = hit2;
        else if (exit == true && homing==null &&!CompareTag("PlayerAttack")) //homingのときは使わない 一度だけ実行(OnCollisionExitみたいな)
        {
            exit = hit2;
            rb.velocity = new Vector2(0.0f,rb.velocity.y); //速度を0
            TumuraContloller.Turn();
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
