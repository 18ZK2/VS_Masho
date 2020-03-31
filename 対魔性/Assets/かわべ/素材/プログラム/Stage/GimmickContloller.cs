using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickContloller : MonoBehaviour
{
    public float HP;
    [SerializeField] GameObject hahen = null;
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
        if (HP <= 0)
        {
            gameObject.AddComponent<EnemyContloller>().MakeHahen(gameObject,hahen);
            Destroy(gameObject);
        }
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
            float damage = rb.mass * dx / 5f;
            if (damage > 0.5f)
            {
                Debug.Log("DamageGimmick");
                e.Damage(damage);
                if (e.HP < damage) HP -= e.HP;
                else HP -= damage;
            }
        }
    }
}
