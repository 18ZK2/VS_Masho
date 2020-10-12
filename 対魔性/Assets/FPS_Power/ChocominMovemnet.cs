using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocominMovemnet : MonoBehaviour
{
    public Animator anim;
    [SerializeField] AnimationCurve throwTypeCurve = new AnimationCurve();
    [SerializeField] float movetJudge = 100f;
    [SerializeField] float ZekkaYJudge = 350f;
    [SerializeField] float airAttackYjudge = 100;
    [SerializeField] float zankuJudge = 50f;
    //[SerializeField] float sufleTime  = 15f;
    [SerializeField] float closeSlashJudge = 64f;
    [SerializeField] float Zekkatime = 20f;
    [SerializeField] float knifet = 20f;
    [SerializeField] float barrierTime = 5f;
    [SerializeField] Transform sphere;

    bool isAction = false;
    float beforeHp;
    Vector2 P_posi;
    Vector2 My_posi;
    GameObject player;
    EnemyContloller ec;
    Rigidbody2D rigid;
    PlayerContloller script;
    IEnumerator tmp = null;
    void StopIEnum()
    {
        isAction = false;
        StopCoroutine(tmp);
        tmp = null;
    }
    Quaternion RotJudge(bool reburse)
    {
        Quaternion q = transform.rotation;
        if (player != null)
        {
            float x = player.transform.position.x - transform.position.x;
            if (reburse)
            {
                q = (x > 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            }
            else
            {
                q = (x > 0) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
            }
            
        }
        return q;
    }

    //必殺のコルーチン
    private IEnumerator Modori()
    {
        Debug.Log("Zekka");
        isAction = true;
        anim.SetBool("running", true);
        yield return new WaitForSeconds(Zekkatime);
        anim.SetBool("running", false);
        anim.SetTrigger("zekka");
        yield return new WaitForSeconds(Zekkatime);
        StopIEnum();
    }
    //斜め打ちと走り切りのコルーチン
    private IEnumerator Movet()
    {
        isAction = true;
        anim.SetBool("running", true);
        float t = 0;
        while (t < knifet)
        {
            //超高いところにいたら
            if (P_posi.y >= My_posi.y + ZekkaYJudge)
            {
                
                tmp = Modori();
                StartCoroutine(Modori());
                StopIEnum();
                yield break;
            }
            //ダッシュできないと,または高いところにいる　空中攻撃
            else if (zankuJudge > script.Dashstamina || P_posi.y >= My_posi.y + airAttackYjudge)
            {
                int ava = UnityEngine.Random.Range(0, 2);
                Debug.Log("Air attack");
                anim.SetTrigger("jump");
                yield return new WaitForSeconds(0.5f);
                //x軸に近いと　波動　遠いと　ナイフ投げ
                if (closeSlashJudge > Mathf.Abs(P_posi.x - My_posi.x))
                {
                    anim.SetTrigger("zanku");
                }
                else if (ava == 0)
                {
                    anim.SetTrigger("throw");
                }
                else if (ava == 1)
                {
                    anim.SetTrigger("throw1");
                }
                yield return new WaitForSeconds(0.5f);
            }
            //近づくと切りつけ
            else if (closeSlashJudge > Mathf.Abs(P_posi.x - My_posi.x))
            {
                anim.SetTrigger("slash");
                yield return new WaitForSeconds(0.5f);
                t += 0.5f;
                break;
            }
            t += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        anim.SetBool("running", false);
        StopIEnum();
    }
    //バリア
    IEnumerator Barrier()
    {
        while (true)
        {
            yield return new WaitForSeconds(barrierTime);
            sphere.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f * barrierTime);
            sphere.gameObject.SetActive(false);

        }
    }
    //即時バリア
    IEnumerator M_Barrier()
    {
        Debug.Log("acton");
        sphere.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        sphere.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        sphere = transform.Find("Sphere");
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        rigid = player.GetComponent<Rigidbody2D>();
        script = player.GetComponent<PlayerContloller>();
        ec = GetComponent<EnemyContloller>();
        StartCoroutine(Barrier());
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = RotJudge(false);
        float speed = 0;
        float stamn = 0;
        if (player != null)
        {
            stamn = script.Dashstamina;
            speed = rigid.velocity.magnitude;
            P_posi = (player != null) ? player.transform.position : Vector3.zero;
        }
        if (beforeHp != ec.HP) StartCoroutine(M_Barrier());
        My_posi = transform.position;
        if (tmp == null)
        {
            //チョコミンよりy高くなったら必殺
            if (P_posi.y >= My_posi.y + ZekkaYJudge)
            {
                tmp = Modori();
            }
            //動いていたら走り切りor空中斜めうち
            else if (speed > movetJudge)
            {
                tmp = Movet();

            }
        }
        else if (!isAction) StartCoroutine(tmp);
        beforeHp = ec.HP;
    }


}
