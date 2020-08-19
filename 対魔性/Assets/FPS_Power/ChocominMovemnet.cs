using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocominMovemnet : MonoBehaviour
{

    float speed;
    public Animator anim;
    public float Zekkatime;
    private GameObject player;
    Rigidbody2D rigid;
    public float knifet;
    PlayerContloller script;
    int ava;
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
        yield return new WaitForSeconds(knifet);
        anim.SetBool("running", speed > 100 && ava == 1);
        if (ava == 2)
        {
            anim.SetTrigger("jump");
        }
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
        speed = rigid.velocity.magnitude;
        Vector2 P_posi = GameObject.Find("Player").transform.position;
        Vector2 My_posi = this.transform.position;
        StartCoroutine("Ransu");
        float Stamina = script.Dashstamina;


        //チョコミンよりy高くなったら必殺
        if (P_posi.y >= My_posi.y + 350)
        {
            StartCoroutine("Modori");

        }
        else
        {
            StopCoroutine("Modori");
        }
        //動いていたら走り切りor空中斜めうち
        if (speed > 100.0)
        {
            StartCoroutine("Movet");

        }
        else
        {
            StopCoroutine("Movet");
            anim.SetBool("running", false);
        }
        //プレイヤーのスタミナがゼロになればナイフ投げ
        if (Stamina == 0)
        {
            anim.SetTrigger("throw3");
        }

    }


}
