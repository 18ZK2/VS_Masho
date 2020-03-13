using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTumuraController : MonoBehaviour
{
    Vector2 pos;
    [SerializeField] float dis = 100.0f, dis2 = 150.0f;
    float ve;
    TumuraContloller TumuraContloller;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
        TumuraContloller = GetComponent<TumuraContloller>();
        rb = GetComponent<Rigidbody2D>();
        ve = -(TumuraContloller.walkspeed / (rb.mass * rb.drag)*1.1f);
    }
    private void FixedUpdate()
    {
        //if (Mathf.Abs(ve) < rb.velocity.x)
        //{
        //    ve = -rb.velocity.x;
        //}
        if (transform.eulerAngles.y == 180 && ve < 0 || transform.eulerAngles.y == 0 && ve > 0)
        {
            ve = -ve;
        }
        pos = new Vector2(ve, -dis2);
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - 16.5f); //FlyingTumuraの場所
        RaycastHit2D hit1 = Physics2D.Raycast(origin, Vector2.down, dis, LayerMask.GetMask("Stage"));
        RaycastHit2D hit2 = Physics2D.Raycast(origin, pos, pos.magnitude, LayerMask.GetMask("Stage"));

        Debug.DrawRay(origin,new Vector2(0,-dis),Color.red,0.1f);
        Debug.DrawRay(origin, pos, Color.blue, 0.1f);


        if (hit1.collider)
        {
            rb.AddForce(new Vector2(0.0f, 1.0f) * rb.mass * rb.gravityScale * 15f, ForceMode2D.Force); //重力を打ち消す
        }
        else if (hit2.collider == null)
        {
            TumuraContloller.Turn();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(ve);

    }
}
