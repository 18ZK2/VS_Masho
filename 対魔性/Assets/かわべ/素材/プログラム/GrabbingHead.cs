using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingHead : MonoBehaviour
{
    public bool touched = false;

    [SerializeField] float sleeptime = 0.5f;
    [SerializeField] Color HeadColor = Color.red;

    bool isActive = false;
    Rigidbody2D rb;
    SpringJoint2D joint;

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
        StartCoroutine(SleepHead());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive && collision.gameObject.tag=="Stage")
        {
            GetComponent<SpriteRenderer>().color = HeadColor;
                rb.bodyType = RigidbodyType2D.Static;
                touched = true;
        }
    }
}
