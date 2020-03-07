using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateContloller : MonoBehaviour
{
    Animator anm, pairAnm;
    Transform exit;
    [SerializeField] GameObject pairG;
    //[SerializeField] GateContloller pairGate;
    // Start is called before the first frame update
    void Start()
    {
        anm = GetComponent<Animator>();
        if (pairG != null)
        {
            pairAnm = pairG.GetComponent<Animator>();
            exit = pairG.transform.Find("pos");
        }
    }
    IEnumerator MovePlayer(Transform t)
    {
        PlayerContloller pc = t.GetComponent<PlayerContloller>();
        //操作を受け付けない
        pc.enabled = false;
        //ダッシュで突っ込んできた対策
        t.GetComponent<Animator>().SetBool("dash", false);
        //ワイヤーも消す
        t.Find("体/左腕").GetComponentInChildren<GrabbingBeam>().DeleteBeams();
        //速度をゼロに
        t.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        anm.SetBool("open", true);
        pairAnm.SetBool("open", true);
        anm.SetBool("close", false);
        pairAnm.SetBool("close", false);
        yield return new WaitForSeconds(1f);
        //操作受付
        pc.enabled = true;
        anm.SetBool("open", false);
        pairAnm.SetBool("open", false);
        anm.SetBool("close", true);
        pairAnm.SetBool("close",true);
        //移動
        t.position = exit.position;
        //スタミナ回復
        pc.Dashstamina = pc.MAX_STAMINA;
        StopCoroutine(MovePlayer(t));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && pairG != null)
        {
            StartCoroutine(MovePlayer(collision.transform.parent));
        }
    }
}
