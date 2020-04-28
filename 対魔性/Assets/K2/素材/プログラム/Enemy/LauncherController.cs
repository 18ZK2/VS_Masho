using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    // isLaunchで発射！
    public bool isLaunch = false;
    
    [SerializeField] int MaxMissileNum = 3;
    [SerializeField] float torque = 0f;
    [SerializeField] Vector2 shotPowVec = Vector2.zero;
    [SerializeField] GameObject missile = null;
    [SerializeField] Transform shotPos = null;
    [SerializeField] HingeJoint2D hinge = null;
    Animator anm;
    EnemyContloller em;
    // Start is called before the first frame update
    void Start()
    {
        anm = GetComponent<Animator>();
        em = GetComponent<EnemyContloller>();
    }
    void LaunchMissile()
    {
        if (shotPos.childCount >= MaxMissileNum) return;
        GameObject m = Instantiate(missile, shotPos);
        m.GetComponent<Rigidbody2D>().AddForce(shotPowVec * shotPos.up, ForceMode2D.Impulse);

    }
    // Update is called once per frame
    void Update()
    {
        anm.SetBool("launch", isLaunch);
        //発射機の角度
        anm.SetFloat("angle", transform.localEulerAngles.z);

        //アニメーションでtorqueを制御
        var motor = hinge.motor;
        motor.motorSpeed = torque;
        hinge.motor = motor;
        if (em.HP <= 0)
        {
            //親が死ぬと子も死ぬので親が死ぬとき子を開放する
            foreach (Transform child in shotPos) if (child != shotPos) child.parent = null;
        }
    }
}
