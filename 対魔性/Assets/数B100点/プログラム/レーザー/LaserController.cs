using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    // isLaunchで発射！
    public bool isLaunch = false;

    //mag レーザーの長さ
    //dig　角速度　（回る速さ）
    //preAngle 初期角度
    [SerializeField] float mag = 10.0f, dig = 5.0f, preAngle = 0f;
    [SerializeField] Transform fire=null;


    Vector2 hitvec;
    float angle=0; //大きさ
    LineRenderer LR;
    Collider2D fireCol;
    ParticleSystem.EmissionModule fireEmission;
    ParticleSystem ps;

    bool sleeped = false;
    IEnumerator Heating()
    {
        
        yield return new WaitForSeconds(ps.main.startLifetime.constant);
        sleeped = true;
        fire.position = LR.GetPosition(1);
        yield break;
    }


    // Start is called before the first frame update
    void Start()
    {
        ps=GetComponent<ParticleSystem>();
        LR = GetComponent<LineRenderer>();
        LR.SetPosition(1, transform.position);
        fireCol = fire.GetComponent<Collider2D>();
        ParticleSystem fireP = fire.GetComponent<ParticleSystem>();
        fireEmission = fireP.emission;

        angle = preAngle;
        fireCol.enabled = fireEmission.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        var emission = ps.emission;
        emission.enabled = isLaunch;
        if (!isLaunch)
        {
            LR.enabled = false;
            angle = preAngle;
            sleeped = false;
            return;
        }
        else if (!sleeped)
        {
            StartCoroutine(Heating());
            return;
        }
        LR.enabled = true;
        hitvec.x = Mathf.Cos(angle * Mathf.Deg2Rad);
        hitvec.y = Mathf.Sin(angle * Mathf.Deg2Rad);
        angle += dig;
        if (angle >= 360) angle -= 360.0f;
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, hitvec, mag, LayerMask.GetMask("Stage"));
        Debug.DrawRay(origin, mag * hitvec.normalized, Color.black, 0.1f);
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
