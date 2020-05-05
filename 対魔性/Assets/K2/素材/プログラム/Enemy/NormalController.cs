using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalController : MonoBehaviour
{
    [SerializeField] float jumpPower = 1000f;
    Rigidbody2D rb;
    Animator anm;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anm = GetComponent<Animator>();
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        if (anm.GetCurrentAnimatorStateInfo(0).IsName("fallNormal") && rb.velocity.y == 0)
        {
            anm.SetTrigger("tyakuchi");
        }
    }
    private void FixedUpdate()
    {
        anm.SetFloat("yspeed", rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Stage")
        {
            anm.SetTrigger("tyakuchi");
        }
    }
}
