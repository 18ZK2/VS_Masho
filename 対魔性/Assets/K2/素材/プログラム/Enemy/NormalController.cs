using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class NormalController : MonoBehaviour
{
    [SerializeField] float jumpPower = 1000f;
    [SerializeField] bool isground = false;
    Rigidbody2D rb;
    Animator anm;
    EnemyContloller em;
    float beforHP;
    
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
        isground = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (em!=null && beforHP > em.HP)
        {
            beforHP = em.HP;
            anm.SetTrigger("damage");
        }
        anm.SetBool("tyakuchi", isground);
        anm.SetFloat("yspeed", rb.velocity.y);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        AnimatorStateInfo stateInfo = anm.GetCurrentAnimatorStateInfo(0);
        //stateInfo.IsName("fall") || stateInfo.IsName("fallStart")
        if (collision.gameObject.tag == "Stage" && Mathf.Abs(rb.velocity.y) < 1e-5) isground = true;

    }
}
