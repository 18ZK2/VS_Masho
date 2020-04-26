﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーの操作について
public class PlayerContloller : MonoBehaviour
{

    public bool canUseGun = false;
    public bool canUseAx = false;

    public float PlayerHp=15;
    public float MaxPlayerHp = 40;
    public float speed = 1000;
    public float dashPow = 1000;

    public float MAX_STAMINA = 200;
    public float Dashstamina= 200;
    public float recoverySpeed = 2f;

    [System.NonSerialized] public bool isDamage = true;
    [System.NonSerialized] public Vector3 bodyVec;
    [System.NonSerialized] public Quaternion armRot;

    [SerializeField] float camSpeed = 0.5f;

    //ダメージ後の無敵時間
    [SerializeField] float immortalTime = 0.5f;
    
    [Header("カメラ関係")]
    [SerializeField] Vector3 camOffset = Vector3.zero;
    [SerializeField] bool useLimit = false;
    [Header("X要素に下限　Y要素に上限")]
    [SerializeField] Vector2 limitFromFirstPosY = new Vector2(-768, 768);
    [SerializeField] Vector2 limitFromFirstPosX = new Vector2(0, 0);

    [Header("サブウェポン")]
    [SerializeField] GameObject Gun = null;
    [SerializeField] GameObject Ax = null;
    [System.NonSerialized] public GameObject gunObj;
    [System.NonSerialized] public GameObject axObj;
    GunController gun;
    HitominController hitomin;
    Animator axanm;

    bool dash;
    int weaponNum = 0;
    //左右移動用
    Vector2 walkVec;
    Vector3 firstPos,camBeforePos;

    GameObject cam;
    Animator anm;
    Rigidbody2D rb;
    GrabbingBeam gb = null;
    Text ammo;

    public IEnumerator StaninaRecovery()
    {
        yield return new WaitForSeconds(1.0f);
        if (MAX_STAMINA < Dashstamina) Dashstamina = MAX_STAMINA;
        while (Dashstamina < MAX_STAMINA)
        {
            yield return null;
            Dashstamina += recoverySpeed;
        }
        
    }
    private IEnumerator Immortal()
    {
        isDamage = false;
        yield return new WaitForSeconds(immortalTime);
        isDamage = true;
        StopCoroutine(Immortal());
    }
    private void VecZero()
    {
        rb.velocity = Vector2.zero;
    }
    public void Damage(float attackPt)
    {
        if (isDamage)
        {
            gb.DeleteBeams();
            PlayerHp -= attackPt;
            StartCoroutine(Immortal());
        }
    }
    private void Dash()
    {
        rb.AddForce(bodyVec.normalized * dashPow, ForceMode2D.Impulse);
        Dashstamina -=100;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        anm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gb = transform.Find("体/左腕").GetComponentInChildren<GrabbingBeam>();
        ammo = GameObject.Find("Ammo").GetComponent<Text>();
        firstPos = transform.position;
        Dashstamina = MAX_STAMINA;

        gunObj = Instantiate(Gun, transform.position, transform.rotation);
        gun = gunObj.GetComponent<GunController>();

        axObj = Instantiate(Ax, transform.position, transform.rotation);
        hitomin = axObj.GetComponentInChildren<HitominController>();
        axanm = axObj.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    [Obsolete]
    void Update()
    {
        //横方向入力
        walkVec = Vector2.right * Input.GetAxis("Horizontal");
        dash = (Input.GetMouseButton(1) || Input.GetMouseButtonDown(1))&& Dashstamina >=100;
        
        //マウスの入力から向かうべき向きを作る
        //bodyVecが向かうべきベクトル
        Vector3 mousePos = Input.mousePosition;
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

        bodyVec = mousePos - pos;
        armRot = Quaternion.LookRotation(Vector3.forward, bodyVec);

        //マウスを押すとビーム発射
        if (Input.GetMouseButtonDown(0))
        {
            gb.MakeBeams(bodyVec.normalized);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gb.DeleteBeams();
        }
        if (Input.GetMouseButtonDown(1) )//|| Dashstamina == MAX_STAMINA)
        {
            StopCoroutine("stamina_gauge");
        }
        if (Input.GetMouseButtonUp(1))
        {
            StartCoroutine(StaninaRecovery());
        }


        //武器切り替え
        weaponNum += Input.GetKeyDown(KeyCode.W) ? 1 : Input.GetKeyDown(KeyCode.S) ? -1 : 0;
        //武器のアニメーション制御
        switch (weaponNum)
        {
            case -1:
                if (canUseAx) weaponNum = 2;
                else if (canUseGun) weaponNum = 1;
                else weaponNum = 0;
                break;
            case 0:
                ammo.text = "No Weapon";
                gunObj.SetActive(false);
                axObj.SetActive(false);
                break;
            case 1:
                if (!canUseGun)
                {
                    weaponNum = 0;
                }
                else
                {
                    if (!gunObj.activeInHierarchy) gunObj.SetActive(true);
                    if (Input.GetKey(KeyCode.Space) && gun.magazine > 0)
                    {
                        gun.isShot = true;
                    }
                    else
                    {
                        gunObj.transform.rotation = transform.localRotation;
                    }
                    gunObj.transform.position = transform.position;
                    axObj.SetActive(false);
                }
                break;
            case 2:
                if (!canUseAx)
                {
                    weaponNum = 0;
                }
                else
                {
                    if (!axObj.activeInHierarchy) axObj.SetActive(true);
                    ammo.text = "sm-zico-nkn";
                    axObj.transform.root.position = transform.position;
                    axanm.SetBool("charge", Input.GetKey(KeyCode.Space));
                    
                    if (Input.GetKeyUp(KeyCode.Space) && hitomin.charged)
                    {
                        axanm.SetTrigger("attack");
                    }
                    //攻撃中は回転しない
                    if(!hitomin.attacking)axObj.transform.root.rotation = transform.localRotation * Quaternion.Euler(0, 180, 0);
                    gunObj.SetActive(false);
                }
                break;
            case 3:
                weaponNum = 0;
                break;
            default:
                break;

        }
    }

    private void LateUpdate()
    {

        //プレイヤーの移動が終わった後にカメラを移動
        Vector3 cp = Vector3.Lerp(cam.transform.position, transform.position + camOffset, camSpeed * Time.deltaTime);
        float xlim = cp.x, ylim = cp.y;
        if (useLimit)
        {
            xlim = (limitFromFirstPosX.x < transform.position.x && transform.position.x < limitFromFirstPosX.y) ? cp.x : camBeforePos.x;
            ylim = (limitFromFirstPosY.x < transform.position.y && transform.position.y < limitFromFirstPosY.y) ? cp.y : camBeforePos.y;
        }
        cam.transform.position = new Vector3(xlim, ylim, -10);
        camBeforePos = cam.transform.position;
    }

    private void FixedUpdate()
    {
        //浮遊
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(origin, Vector2.down *40f, 40f, LayerMask.GetMask("Stage"));
        foreach (var r in hit2Ds){
            if (r.collider != null)
            {
                rb.AddForce(Vector2.up * rb.mass * rb.gravityScale * 10f, ForceMode2D.Force);
            }
        }
        //走る
        rb.AddForce(walkVec*speed);

        //ダッシュ
        anm.SetBool("dash", dash);
    }
}
