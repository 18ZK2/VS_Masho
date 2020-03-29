using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFootController : MonoBehaviour
{
    public bool isLaunch = false;
    [SerializeField] float jointPow = 10000f;
    [SerializeField] HingeJoint2D footJoint = null;
    [SerializeField] Transform jointTrans;

    JointMotor2D motor;
    // Start is called before the first frame update
    void Start()
    {
        motor = footJoint.motor;

    }

    // Update is called once per frame
    void Update()
    {
        float l = isLaunch ? 1 : 0;
        float l_R = !isLaunch ? 1 : 0;
        motor.motorSpeed = jointPow * l;
        motor.motorSpeed = jointPow * l_R * -jointTrans.rotation.eulerAngles.z / 20f;
        footJoint.motor = motor;
    }
}
