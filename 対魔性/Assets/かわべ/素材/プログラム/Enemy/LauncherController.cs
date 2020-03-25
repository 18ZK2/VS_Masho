using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    
    [SerializeField] int MaxMissileNum = 3;
    [SerializeField] Vector2 shotPowVec = Vector2.zero;
    [SerializeField] GameObject missile = null;
    [SerializeField] Transform shotPos = null;

    float beforHP;

    Transform missileList;
    Animator anm;
    EnemyContloller em;
    // Start is called before the first frame update
    void Start()
    {
        missileList = Instantiate(new GameObject() , transform).transform.parent = transform;
        anm = GetComponent<Animator>();
        em = GetComponent<EnemyContloller>();
    }
    void LaunchMissile()
    {
        Transform m = Instantiate(missile, shotPos).transform.parent = missileList;
        m.GetComponent<Rigidbody2D>().AddForce(shotPowVec * shotPos.up, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {

        anm.SetBool("immortal", !em.isDamage);
        if (missileList.childCount < MaxMissileNum) LaunchMissile();
    }
}
