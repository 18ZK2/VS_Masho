using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateContloller : MonoBehaviour
{
    [SerializeField] Color disableColor = Color.gray;
    [SerializeField] GameObject pairG = null;
    [SerializeField] AudioClip[] SEs = new AudioClip[2];
    [SerializeField] bool isSceneChange = false;
    [SerializeField] Color SceneGateColor = Color.red;
    [SerializeField] string sceneName = "";

    Animator anm, pairAnm;
    Transform exit;
    AudioSource asc;
    SpriteRenderer[] sprites;

    public void SEplay(int i)
    {
        if (i < 2) asc.PlayOneShot(SEs[i]);
    }
    void changeColor(Color c)
    {
        foreach (var s in sprites)
        {
            s.color *= c;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        asc = GetComponent<AudioSource>();
        anm = GetComponent<Animator>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        if (isSceneChange) changeColor(SceneGateColor);
        if (pairG != null)
        {
            pairAnm = pairG.GetComponent<Animator>();
            exit = pairG.transform.Find("pos");
        }
        else if (!isSceneChange || sceneName == "")
        {
            changeColor(disableColor);
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
        anm.SetBool("close", false);
        if (pairAnm != null)
        {
            pairAnm.SetBool("open", true);
            pairAnm.SetBool("close", false);
        }
        yield return new WaitForSeconds(0.5f);
        if (isSceneChange)
        {
            Debug.Log(TimeCount.timer); //クリア時間
            //SceneManager.LoadScene(sceneName);
            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            GameManager.LoadHP = pc.PlayerHp;
            manager.StartCoroutine(manager.WipeLoadScene(sceneName));
            
        }
        else if (pairG != null && t!=null)
        {
            //移動
            t.position = exit.position;
            anm.SetBool("open", false);
            pairAnm.SetBool("open", false);
            anm.SetBool("close", true);
            pairAnm.SetBool("close", true);
            yield return new WaitForSeconds(2f);
            //操作受付
            pc.enabled = true;
            //スタミナ回復
            pc.Dashstamina = pc.MAX_STAMINA;
        }
        StopCoroutine(MovePlayer(t));
    }
    private void Update()
    {
        if (pairG != null)
        {
            changeColor(Color.white);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && (pairG != null || isSceneChange))
        {

            StartCoroutine(MovePlayer(collision.transform.parent));

        }
    }
}
