using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocominMovemnet : MonoBehaviour
{
    [SerializeField] float movetJudge = 100f;
    [SerializeField] float ZekkaYJudge = 350f;
    [SerializeField] float knifeJudege = 100f;
    float speed;
    public Animator anim;
    public float Zekkatime;
    private GameObject player;
    Rigidbody2D rigid;
    public float knifet;
    PlayerContloller script;
    [SerializeField] int ava;

    Quaternion RotJudge(bool reburse)
    {
        Quaternion q;
        float x = player.transform.position.x - transform.position.x;
        if (reburse)
        {
            q = (x > 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        }
        else
        {
            q = (x > 0) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        }
        return q;
    }

    //乱数生成(1or2)
    private IEnumerator Ransu()
    {
        ava = UnityEngine.Random.Range(1, 3);
        yield return new WaitForSeconds(1000f);

    }
    //必殺のコルーチン
    private IEnumerator Modori()
    {
        yield return new WaitForSeconds(Zekkatime);
        anim.SetTrigger("zekka");
    }
    //斜め打ちと走り切りのコルーチン
    private IEnumerator Movet()
    {
        
        //anim.SetBool("running", speed > movetJudge && ava == 1);
        anim.SetBool("running", true);
        yield return new WaitForSeconds(knifet);
        if (ava == 2)
        {
            anim.SetTrigger("jump");
        }
        anim.SetBool("running", false);
        yield return new WaitForSeconds(knifet);
        Debug.Log("うんち");
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        rigid = player.GetComponent<Rigidbody2D>();
        script = player.GetComponent<PlayerContloller>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = RotJudge(false);
        speed = rigid.velocity.magnitude;
        Debug.Log("PlayerSpeed"+speed.ToString("0000.0"));
        Vector2 P_posi = player.transform.position;
        Vector2 My_posi = this.transform.position;
        StartCoroutine("Ransu");
        float Stamina = script.Dashstamina;


        //チョコミンよりy高くなったら必殺
        if (P_posi.y >= My_posi.y + ZekkaYJudge)
        {
            StartCoroutine("Modori");

        }
        else
        {
            StopCoroutine("Modori");
        }
        //動いていたら走り切りor空中斜めうち
        if (speed > movetJudge)
        {
            StartCoroutine("Movet");

        }
        else
        {
            StopCoroutine("Movet");
            anim.SetBool("running", false);
        }
        //プレイヤーのスタミナがゼロになればナイフ投げ
        if (Stamina < knifeJudege)
        {
            anim.SetTrigger("throw3");
        }

    }


}
