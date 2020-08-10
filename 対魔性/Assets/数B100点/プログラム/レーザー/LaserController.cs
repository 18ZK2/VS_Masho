using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    // isLaunchで発射！
    public bool isLaunch = false;
    public float dig = 5.0f;

    [System.NonSerialized] float angle = 0; //大きさ

    //mag レーザーの長さ
    //dig　角速度　（回る速さ）
    //preAngle 初期角度
    //body 振り向いたときに回転の向きを変える
    [SerializeField] float mag = 10.0f, preAngle = 0f;
    [SerializeField] Transform fire=null;
    [SerializeField] AudioClip Charge = null, StartLaser = null,loopLaser = null;
    [SerializeField] Transform body = null;

    //レーザーの加熱用（発射時の根本のパーティクルが出きったタイミングを計る）
    bool sleeped = false;
    //レーザーのチャージ音を再生したか
    bool playerChargeSound = false;
    //レーザーの初期発射音を再生したか
    bool playedFirstLaser = false;
    float laserplaytime = 0f;
    Vector2 hitvec;
    
    LineRenderer LR;
    Collider2D fireCol;
    ParticleSystem.EmissionModule fireEmission;
    ParticleSystem ps;
    AudioSource ass;

    IEnumerator Heating()
    {
        //チャージ音再生
        if(!playerChargeSound)ass.PlayOneShot(Charge);
        playerChargeSound = true;

        //パーティクルが出切るまで待つ
        yield return new WaitForSeconds(ps.main.startLifetime.constant);

        if (!playedFirstLaser) ass.PlayOneShot(StartLaser);
        playedFirstLaser = true;
        sleeped = true;
        fire.position = LR.GetPosition(1);
        yield break;
    }

    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>();

        ps=GetComponent<ParticleSystem>();
        LR = GetComponent<LineRenderer>();
        LR.SetPosition(1, transform.position);
        fireCol = fire.GetComponent<Collider2D>();
        ParticleSystem fireP = fire.GetComponent<ParticleSystem>();
        fireEmission = fireP.emission;
        fireCol.enabled = fireEmission.enabled = false;
        angle = preAngle;
    }

    // Update is called once per frame
    void Update()
    {
        float r = dig;
        var emission = ps.emission;
        emission.enabled = isLaunch;
        //発射状態でない
        if (!isLaunch)
        {
            //初期化
            angle = preAngle;
            sleeped = false;
            playerChargeSound = false;
            playedFirstLaser = false;
            LR.enabled = false;
            fireCol.enabled = false;
            fireEmission.enabled = false;
            laserplaytime = 0f;
            ass.Stop();
            return;
        }
        //発射体制に移る
        else if (!sleeped)
        {
            
            StartCoroutine(Heating());
            return;
        }
        //発射
        laserplaytime += Time.deltaTime;
        //レーザー音が終わる寸前
        if (laserplaytime > loopLaser.length*0.9)
        {
            laserplaytime = 0f;
            ass.PlayOneShot(loopLaser);
        }

        //レイキャストを回転
        LR.enabled = true;
        hitvec.x = Mathf.Cos(angle * Mathf.Deg2Rad);
        hitvec.y = Mathf.Sin(angle * Mathf.Deg2Rad);
        if (body != null)
        {
            angle += (body.rotation.eulerAngles.y != 0) ? -r * Time.deltaTime : r * Time.deltaTime;
        }
        else
        {
            angle += r * Time.deltaTime;
        }
        //if (angle >= 360) angle -= 360.0f;
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, hitvec, mag, LayerMask.GetMask("Stage"));
        //Debug.DrawRay(origin, mag * hitvec.normalized, Color.black, 0.1f);
        LR.SetPosition(0, transform.position);
        if (hit.collider != null)
        {
            fireCol.enabled = true;
            fireEmission.enabled = true;
            LR.SetPosition(1, hit.point);
        }
        else
        {
            fireCol.enabled = false;
            fireEmission.enabled = false;
            LR.SetPosition(1, origin + hitvec.normalized * mag);
        }
        fire.position = LR.GetPosition(1);

    }
}
