using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFootController : MonoBehaviour
{
    public bool isLaunch = false;
    [SerializeField] float jointPow = 10000f;
    [SerializeField] HingeJoint2D footJoint = null;
    [SerializeField] Transform jointTrans,bodyTrans;

    JointMotor2D motor;
    Transform player;
    LauncherController launcher;
    // Start is called before the first frame update
    void Start()
    {
        motor = footJoint.motor;
        player = GameObject.Find("Player").transform;
        launcher = GetComponentInChildren<LauncherController>();
    }

    // Update is called once per frame
    void Update()
    {
        float l = isLaunch ? 1 : 0;
        bool launchMissile = false;
        motor.motorSpeed = jointPow * l;
        footJoint.motor = motor;
        //ボスの体より高いところだとミサイル
        if (player != null && player.position.y > bodyTrans.position.y) launchMissile = true;
        if (launcher != null) launcher.isLaunch = launchMissile;
    }
}
