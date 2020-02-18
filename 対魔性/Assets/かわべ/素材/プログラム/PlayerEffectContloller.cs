using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの見た目について
public class PlayerEffectContloller : MonoBehaviour
{
    public bool isFrip;
    [SerializeField] const int effectSize = 3;
    [SerializeField] Transform[] effectPos = new Transform[effectSize];
    [SerializeField] GameObject[] effects = new GameObject[effectSize];
    [SerializeField] Color damageColor = Color.white;

    Animator anm;
    Rigidbody2D rb;
    Transform arm, body;
    SpriteRenderer armSR;
    SpriteRenderer[] bodyRenderer;
    PlayerContloller pc;

    private void RotateBody()
    {
        body.Rotate(body.forward, pc.armRot.eulerAngles.z);
        //body.localRotation = Quaternion.Euler(0, 0, pc.armRot.eulerAngles.z);
        
    }
    private void ResetRotateBody()
    {
        body.localRotation = Quaternion.Euler(0f, 0f, 0f);

    }
    private void ParticleShot(int i)
    {
        if (i < effectSize) Instantiate(effects[i], effectPos[i].position, body.transform.rotation);
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
        arm = transform.Find("体/左腕");
        armSR = arm.GetComponent<SpriteRenderer>();
        bodyRenderer = GetComponentsInChildren<SpriteRenderer>();
        body = transform.Find("体");
        pc = GetComponent<PlayerContloller>();
    }

    // Update is called once per frame
    void Update()
    {
        FlipBody();
        arm.transform.rotation = Quaternion.Euler(0, 0, pc.armRot.eulerAngles.z) * Quaternion.Euler(0, 0, 90f);
        ChangeBodyColor();
        anm.SetFloat("speed", Mathf.Abs(rb.velocity.x) / pc.walkspeed);
    }

    
}
