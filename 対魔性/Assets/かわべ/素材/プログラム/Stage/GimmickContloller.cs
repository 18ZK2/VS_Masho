using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickContloller : MonoBehaviour
{
    public float HP;

    Rigidbody2D rb;

    float dx;//微小距離
    Vector3 beforePos;//1フレーム前の位置
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (HP <= 0) Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        dx = (beforePos - transform.position).magnitude;
        beforePos = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyContloller e = collision.gameObject.GetComponent<EnemyContloller>();
            float damage = rb.mass * dx / 30f;
            e.Damage(damage);
            HP -= damage;
        }
    }
}
