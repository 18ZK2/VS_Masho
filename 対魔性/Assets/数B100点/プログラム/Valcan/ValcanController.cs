using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValcanController : MonoBehaviour
{
    [SerializeField] bool colDead = false;
    EnemyContloller ec;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f);
        ec = GetComponent<EnemyContloller>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || colDead)
        {
            var e = GetComponent<EnemyContloller>();
            e.Damage(e.HP);
        }
    }
}
