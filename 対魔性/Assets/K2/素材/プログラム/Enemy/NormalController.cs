using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class NormalController : MonoBehaviour
{
    [SerializeField] float jumpPower = 1000f;
    Rigidbody2D rb;
    Animator anm;
    EnemyContloller em;
    float beforHP;
    bool isground = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anm = GetComponent<Animator>();
        em = GetComponent<EnemyContloller>();
        if(em!=null)beforHP = em.HP;
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        if (em!=null && beforHP > em.HP)
        {
            beforHP = em.HP;
            anm.SetTrigger("damage");
        }
    }
    private void FixedUpdate()
    {
        anm.SetFloat("yspeed", rb.velocity.y);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isground = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        AnimatorStateInfo stateInfo = anm.GetCurrentAnimatorStateInfo(0);
        //stateInfo.IsName("fall") || stateInfo.IsName("fallStart")
        //Debug.Log(rb.velocity.y);
        if (rb.velocity.y == 0f && !isground)
        {
            isground = true;
            anm.SetTrigger("tyakuchi");
        }

    }
}
