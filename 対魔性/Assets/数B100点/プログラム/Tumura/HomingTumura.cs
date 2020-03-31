using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingTumura : MonoBehaviour
{
    [Header("追従の力の大きさ")] [SerializeField] float f = 13.0f;
    [Header("0～360でオナシャス")] [SerializeField] float startangle = 0, finishangle = 90.0f;
    Vector2 diff,hitvec;
    float ve, maxf = 110.0f, mag=10.0f, angle;
    bool dig = false;
    TumuraContloller TumuraContloller;
    Rigidbody2D rb;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        angle = startangle;
        player = GameObject.Find("Player");
        TumuraContloller = GetComponent<TumuraContloller>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        hitvec.x = mag*Mathf.Cos(angle * Mathf.Deg2Rad);
        hitvec.y = mag*Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 origin = new Vector2(transform.position.x, transform.position.y); //FlyingTumuraの場所(浮遊用)
        RaycastHit2D hit = Physics2D.Raycast(origin, -transform.right*hitvec, mag, LayerMask.GetMask("Stage")); //
        Debug.DrawRay(origin, transform.right.x * -mag*hitvec.normalized, Color.black, 0.1f);

        if (player != null)
        {
            diff = (player.transform.position - transform.position); //TumuraからPlayerまでのベクトル
            if (diff.magnitude < 2000.0f) //一定範囲内の場合
            {
                if (diff.magnitude > maxf) diff = diff.normalized * maxf;
                rb.AddForce(diff * f);
                if (diff.x > 0) transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            if (hit.collider)
            {
                if (diff.y > 0) rb.AddForce(Vector2.up * 250.0f, ForceMode2D.Impulse);
                else rb.AddForce(Vector2.down * 250.0f, ForceMode2D.Impulse);
            }
        }
        //hit3の回転
        {
            angle += 2;
            if (dig && angle > finishangle)
            {
                angle = startangle;
                dig = false;
            }
            if (angle > 360.0f)
            {
                angle -= 360.0f;
                dig = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
