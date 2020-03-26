using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MissileController : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] float risingTime = 2.5f;
    [SerializeField] GameObject target = null;

    bool isHorming = false;
    Rigidbody2D rb;
    EnemyContloller ec;

    IEnumerator RiseMissile()
    {
        yield return new WaitForSeconds(1f);
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        for (float t = risingTime; t > 0f; t -= 0.1f)
        {
            rb.AddForce(transform.up * speed, ForceMode2D.Force);
            yield return new WaitForSeconds(0.1f);
        }
        isHorming = true;
        target = GameObject.FindWithTag("Player");
        StopCoroutine(RiseMissile());
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ec = GetComponent<EnemyContloller>();
        StartCoroutine(RiseMissile());
        Destroy(gameObject, 15f);
        //rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {
        if (!isHorming || target==null) return;
        Vector2 diff = target.transform.position - transform.position;
        Quaternion targetRot = Quaternion.LookRotation(Vector3.forward,diff);
        transform.rotation = targetRot;
        if (gameObject.tag == "Enemy") rb.AddForce(transform.up * speed / 5f, ForceMode2D.Force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "PlayerAttack")
        {
            ec.Damage(ec.HP);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "PlayerAttack" && gameObject.tag != "PlayerAttack" && collision.gameObject.tag == "Player" && isHorming)
        {
            ec.Damage(ec.HP);
        }
    }


}
