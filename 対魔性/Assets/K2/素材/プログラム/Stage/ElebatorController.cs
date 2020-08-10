using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElebatorController : MonoBehaviour
{
    [SerializeField] float velocity = 5f;
    Rigidbody2D rb;
    BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!box.isTrigger) rb.velocity = transform.up * velocity;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")box.isTrigger = false;
    }
}
