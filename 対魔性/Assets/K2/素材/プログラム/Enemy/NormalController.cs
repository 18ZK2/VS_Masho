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
    [SerializeField] float hitlength = 75;
    [SerializeField] bool isground;
    [SerializeField] GameObject mnk = null;
    [SerializeField] AudioClip[] clips = null;

    bool playing = true;
    float dashPowTmp;
    IEnumerator e;
    Transform player; 
    Rigidbody2D rb;
    Animator anm;
    AudioSource ass;
    Vector3 vec;
    EnemyContloller em;
    RaycastHit2D hit;
    RaycastHit2D hit2;
    Quaternion RotJudge(bool reburse)
    {
        Quaternion q;
        if (reburse)
        {
            q = (vec.x > 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        }
        else
        {
            q = (vec.x > 0) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        }
        return q;
    }
    //行動パターン
    IEnumerator Enum()
    {
        while (true)
        {

            float rnd = Random.Range(0f, 1f);
            //プレイヤーと距離が遠いと何もしない
            if (attackRange < vec.magnitude) yield return null;
            //レーザー
            else if (laserCurve.Evaluate(em.HP / maxHP) > rnd)
            {
                Debug.Log("レーザー");
                
                if (hit2.collider != null)
                {
                    //逃げる
                    Debug.Log("逃げる");
                    transform.rotation = RotJudge(true);
                }
                else if (hit.collider != null)
                {
                    //逃げない
                    Debug.Log("逃げない");
                    transform.rotation = RotJudge(false);
                }
                anm.SetTrigger("jump");
                yield return new WaitForSeconds(0.5f);
                anm.SetTrigger("attack");
                dashPower /= 2;
                yield return new WaitForSeconds(1.5f);
                dashPower *= 2;
                transform.rotation = RotJudge(false);
                anm.SetTrigger("laser");
                yield return new WaitForSeconds(5f);

            }
            else
            {
                for (int i = 0; i < maxSlashCount; i++)
                {
                    if (Mathf.Abs( vec.y)<  50)
                    {
                        anm.SetTrigger("attack");
                        yield return new WaitForSeconds(0.5f);
                        transform.rotation = RotJudge(false);
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        anm.SetTrigger("jump");
                        yield return new WaitForSeconds(0.5f);
                        anm.SetTrigger("attack");
                        transform.rotation = RotJudge(false);
                        yield return new WaitForSeconds(1.5f);
                    }

                }
            }
        }

    }
    float beforHP;
    float maxHP;
    void Sounds(int i)
    {
        ass.PlayOneShot(clips[i]);
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        isground = false;
    }
    void Dash()
    {
        rb.AddForce(-transform.right * dashPower, ForceMode2D.Impulse);
    }
    void dashPowReset()
    {
        if (dashPower != dashPowTmp) dashPower = dashPowTmp;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anm = GetComponent<Animator>();
        em = GetComponent<EnemyContloller>();
        ass = GetComponent<AudioSource>();
        player = GameObject.Find("Player").transform;
        vec = transform.position - player.position;
        if (em != null)
        {
            maxHP = em.HP;
            beforHP = em.HP;
        }
        e = Enum();
        StartCoroutine(e);
        isground = false;
        dashPowTmp = dashPower;
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

        //レーザー発振
        hit = Physics2D.Raycast(transform.position, transform.right, hitlength, LayerMask.GetMask("Stage"));
        hit2 = Physics2D.Raycast(transform.position, -transform.right, hitlength, LayerMask.GetMask("Stage"));
        Debug.DrawRay(transform.position, transform.right * hitlength, Color.red);
        Debug.DrawRay(transform.position, -transform.right * hitlength, Color.blue);
        Debug.Log("右" + (hit.collider != null).ToString());
        Debug.Log("左" + (hit2.collider != null).ToString());

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
