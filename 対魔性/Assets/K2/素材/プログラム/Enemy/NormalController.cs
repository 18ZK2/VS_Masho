using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class NormalController : MonoBehaviour
{
    //laserCurve 体力によってレーザーの発射確率が変わる
    [SerializeField] int maxSlashCount = 3;
    [SerializeField] AnimationCurve laserCurve = new AnimationCurve();
    [SerializeField] float jumpPower = 1000f;
    [SerializeField] float dashPower = 2000f;
    [SerializeField] float attackRange = 64f;
    [SerializeField] bool isground = false, laser = false;
    [SerializeField] GameObject mnk;

    bool playing = true;
    IEnumerator e;
    Transform player; 
    Rigidbody2D rb;
    Animator anm;
    Vector3 vec;
    EnemyContloller em;
    //行動パターン
    IEnumerator Enum()
    {
        while (true)
        {

            float rnd = Random.Range(0f, 1f);
            if (attackRange < vec.magnitude) yield return null;
            else if (laserCurve.Evaluate(em.HP / maxHP) > rnd)
            {
                //レーザー発振
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 200, LayerMask.GetMask("Stage"));
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -transform.right, 200, LayerMask.GetMask("Stage"));
                Debug.Log(hit.collider != null);
                if (hit.collider != null)
                {
                    //逃げる
                    transform.rotation = (vec.x < 0) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
                }
                else if (hit2.collider != null)
                {
                    //逃げない
                    transform.rotation = (vec.x > 0) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
                }
                anm.SetTrigger("jump");
                yield return new WaitForSeconds(0.5f);
                anm.SetTrigger("attack");
                dashPower /= 2;
                yield return new WaitForSeconds(1.5f);
                dashPower *= 2;
                anm.SetTrigger("laser");
                transform.rotation = (vec.x > 0) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
                yield return new WaitForSeconds(5f);

            }
            else
            {
                for (int i = 0; i < maxSlashCount; i++)
                {
                    Debug.Log(vec.y);
                    if (Mathf.Abs( vec.y)<  50)
                    {
                        anm.SetTrigger("attack");
                        yield return new WaitForSeconds(0.5f);
                        transform.rotation = (vec.x > 0) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        anm.SetTrigger("jump");
                        yield return new WaitForSeconds(0.5f);
                        anm.SetTrigger("attack");
                        transform.rotation = (vec.x > 0) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
                        yield return new WaitForSeconds(1.5f);
                    }

                }
            }
        }

    }
    float beforHP;
    float maxHP;
    void Jump()
    {
        rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        isground = false;
    }
    void Dash()
    {
        rb.AddForce(-transform.right * dashPower, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anm = GetComponent<Animator>();
        em = GetComponent<EnemyContloller>();
        player = GameObject.Find("Player").transform;
        vec = transform.position - player.position;
        if (em != null)
        {
            maxHP = em.HP;
            beforHP = em.HP;
        }
        e = Enum();
        StartCoroutine(e);
    }
    // Update is called once per frame
    void Update()
    {
        vec = transform.position - player.position;
        if (em!=null && beforHP > em.HP)
        {
            beforHP = em.HP;
            anm.SetTrigger("damage");
        }
        anm.SetBool("tyakuchi", isground);
        anm.SetFloat("yspeed", rb.velocity.y);

        //ダメージの時行動パターン初期化
        if (!anm.enabled && playing)
        {
            StopCoroutine(e);
            e = null;
            playing = false;
        }
        if (anm.enabled && !playing)
        {
            e = Enum();
            StartCoroutine(e);
            playing = true;
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        AnimatorStateInfo stateInfo = anm.GetCurrentAnimatorStateInfo(0);
        if (collision.gameObject.tag == "Stage" && Mathf.Abs(rb.velocity.y) < 1e-5) isground = true;

    }
    private void OnDestroy()
    {
        Destroy(mnk);
    }
}
