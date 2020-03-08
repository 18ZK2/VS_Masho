using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの見た目について
public class PlayerEffectContloller : MonoBehaviour
{
    public bool isFrip;

    [SerializeField] const int effectSize = 3;
    [SerializeField] Color damageColor = Color.white;
    [SerializeField] Transform[] effectPos = new Transform[effectSize];
    [SerializeField] GameObject[] effects = new GameObject[effectSize];
    [SerializeField] AudioClip[] SEs = new AudioClip[effectSize];

    float beforHP;

    Animator anm;
    Rigidbody2D rb;
    SpriteRenderer armSR;
    SpriteRenderer[] bodyRenderer;
    AudioSource asc;

    Transform arm, body;
    PlayerContloller pc;

    private void RotateBody()
    {
        body.Rotate(body.forward, pc.armRot.eulerAngles.z);
    }
    private void ResetRotateBody()
    {
        body.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
    private void ParticleShot(int i)
    {
        if (i < effectSize)
        {
            asc.PlayOneShot(SEs[i]);
            Instantiate(effects[i], effectPos[i].position, body.transform.rotation);
        }
    }
    private void ChangeBodyColor()
    {
        foreach (var b in bodyRenderer)
        {
            b.color = damageColor;
        }
    }
    private void FlipBody()
    {
        if (!isFrip) return;
        //体の反転
        if (pc.bodyVec.x < 0f)
        {
            //左向き
            if (rb.velocity.x > 0) anm.SetBool("isLeft", true);
            else anm.SetBool("isLeft", false);
            armSR.flipY = true;
            transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            if (rb.velocity.x < 0) anm.SetBool("isLeft", true);
            else anm.SetBool("isLeft", false);
            armSR.flipY = false;
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        arm = transform.Find("体/左腕"); armSR = arm.GetComponent<SpriteRenderer>();
        bodyRenderer = GetComponentsInChildren<SpriteRenderer>();
        body = transform.Find("体");
        pc = GetComponent<PlayerContloller>();
        asc = GetComponent<AudioSource>();

        beforHP = pc.PlayerHp;
    }

    // Update is called once per frame
    void Update()
    {
        //無敵判定の点滅
        anm.SetBool("immortal", !pc.isDamage);

        //ノックバック
        if (beforHP > pc.PlayerHp)
        {
            beforHP = pc.PlayerHp;
            anm.SetTrigger("damage");
        }else if (beforHP < pc.PlayerHp)
        {
            beforHP = pc.PlayerHp;
        }

        //腕の画像切り替え
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) anm.SetFloat("armState", 1);
        else if (Input.GetMouseButtonUp(0)) anm.SetFloat("armState", 0);
        else anm.SetFloat("armState", 0);
        
        anm.SetFloat("speed", Mathf.Abs(rb.velocity.x) / pc.speed);

        //腕を回す
        arm.transform.rotation = Quaternion.Euler(0, 0, pc.armRot.eulerAngles.z) * Quaternion.Euler(0, 0, 90f);
        FlipBody();
        ChangeBodyColor();
    } 
}
