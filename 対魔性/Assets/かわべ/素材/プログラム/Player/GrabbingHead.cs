using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingHead : MonoBehaviour
{
    [System.NonSerialized] public GameObject touchedObject;
    [SerializeField] float sleeptime = 0.5f;

    bool isActive = false;
    Rigidbody2D rb;
    SpringJoint2D joint;
    ParticleSystem ps;

    IEnumerator SleepHead()
    {
        // 発射直後は反応なし
        yield return new WaitForSeconds(sleeptime);
        rb.WakeUp();
        isActive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<SpringJoint2D>();
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(SleepHead());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //変数isActiveがfalseならオブジェクトに触れても反応しない
        if (!isActive) return;
        else
        {
            isActive = false;
            ps.TriggerSubEmitter(0);
            touchedObject = collision.gameObject;
        }
    }
}
