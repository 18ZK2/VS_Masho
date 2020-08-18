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
    //乱数生成
    private IEnumerator Ransu()
    {
        ava = UnityEngine.Random.Range(1, 3);
        yield return new WaitForSeconds(10000f);

    }
    
    private IEnumerator Modori()
    {
        yield return new WaitForSeconds(Zekkatime);
        anim.SetTrigger("zekka");
    }

    private IEnumerator Movet()
    {
        yield return new WaitForSeconds(knifet);
        anim.SetBool("running", speed > 100 && ava == 1);
        anim.SetBool("dcWave", speed > 100 && ava == 2);
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
        Debug.Log(ava);

        if (P_posi.y >= My_posi.y + 350)
        {
            StartCoroutine("Modori");

        }
        if (speed > 100.0)
        {
            StartCoroutine("Movet");

        }
        else
        {
            StopCoroutine("Movet");
            anim.SetBool("dcWabe", false);
            anim.SetBool("running", false);
        }
        if (Stamina == 0)
        {
            anim.SetTrigger("throw3");
        }

    }


}
