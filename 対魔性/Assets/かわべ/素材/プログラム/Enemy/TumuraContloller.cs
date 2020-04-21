using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumuraContloller : MonoBehaviour
{
    RaycastHit2D hit3D, hit4D;
    FlyingTumuraController ftc = null;
    public float walkspeed = -500;
    [SerializeField] bool turning = false;
    [Header("反転と坂")] [SerializeField] bool st = false; 
    bool exit = false;
    float beforHP;
    Vector2 vec = new Vector2(1,-0.8f);
    Animator anm;
    Rigidbody2D rb;
    EnemyContloller em;

    private void RotBody()
    {
        transform.Rotate(transform.up, 180);
    }
    public void Turn()
    {
        if (turning) return;
        if (rb.velocity.x > 0f)
        {
            anm.SetTrigger("turn");
        }
        else
        {
            anm.SetTrigger("turnBack");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        em = GetComponent<EnemyContloller>();
        anm = GetComponent<Animator>();
        beforHP = em.HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (beforHP > em.HP)
        {
            beforHP = em.HP;
            anm.SetTrigger("damage");
        }
    }
    private void FixedUpdate()
    {
        Vector2 vec2 = new Vector2(vec.x * -transform.right.x, vec.y);
        Vector2 origin = new Vector2(transform.position.x, transform.position.y+12.0f);
        Vector2 origin2 = new Vector2(transform.position.x-(16.0f*transform.right.x), transform.position.y + 10.0f);
        Vector2 origin3 = new Vector2(transform.position.x+3.0f, transform.position.y);

        //flyingではないとき
        if(st==true){

            hit4D = Physics2D.Raycast(origin3, vec2, 17f, LayerMask.GetMask("Stage")); //坂
            hit3D = Physics2D.Raycast(origin2, Vector2.down, 47.0f, LayerMask.GetMask("Stage")); //反転
            Debug.DrawRay(origin2, Vector2.down * 47.0f, Color.green);
            Debug.DrawRay(origin3, vec2.normalized * 17.0f, Color.yellow);
            if (hit4D.collider) rb.AddForce(-transform.right * 61.0f, ForceMode2D.Impulse);
            if (hit3D.collider) exit = hit3D;
            else if (exit)
            {
                exit = hit3D;
                Turn();
            }
        }
        //レイヤーをとかって最適化していまふ
        RaycastHit2D[] hit2D = Physics2D.RaycastAll(origin, -transform.right, 24f, LayerMask.GetMask("Stage"));
        Debug.DrawRay(origin, transform.right * -24);
        foreach (RaycastHit2D r in hit2D)
        {
            if (r.collider.gameObject.tag == "Stage")
            {
                Turn();
                break;
            }
        }
        rb.AddForce(transform.right * walkspeed, ForceMode2D.Force);
    }
    
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Stage")
    //    {
    //        Debug.Log("a");
    //        Turn();
    //    }
    //}


}
