using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    [SerializeField] float AttackP=0.5f;
    private EnemyContloller ec;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f);

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ec = collision.gameObject.GetComponent<EnemyContloller>();
            if(ec!=null) ec.HP -= AttackP;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Gimmick"))
        {
            collision.gameObject.GetComponent<GimmickContloller>().HP -= AttackP;
        }
        if (collision.gameObject.CompareTag("Stage"))
        {
            Destroy(gameObject);
        }
    }
}
