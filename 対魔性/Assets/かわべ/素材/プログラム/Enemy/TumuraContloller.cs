using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumuraContloller : MonoBehaviour
{
    public float walkspeed = -500;
    [SerializeField] bool turning = false;

    float beforHP;

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
        anm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        em = GetComponent<EnemyContloller>();

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
        
        anm.SetBool("immortal", !em.isDamage);


    }
    private void FixedUpdate()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        //レイヤーをとかって最適化していまふ
        RaycastHit2D[] hit2D = Physics2D.RaycastAll(origin, transform.right * -32f, 32f, LayerMask.GetMask("Stage"));
        Debug.DrawRay(origin, transform.right * -32);
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
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage") Turn();
    }


}
