using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    Vector2 hitvec;
    float angle=0; //大きさ
    [SerializeField] float mag = 10.0f,dig=5.0f;
    [SerializeField] Transform target;
    LineRenderer LR;
    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
        LR.SetPosition(1, transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        hitvec.x =Mathf.Cos(angle * Mathf.Deg2Rad);
        hitvec.y =Mathf.Sin(angle * Mathf.Deg2Rad);
        angle += dig;
        if (angle >= 360) angle -= 360.0f;
        Vector2 origin = new Vector2(transform.position.x, transform.position.y); 
        RaycastHit2D hit = Physics2D.Raycast(origin, hitvec, mag, LayerMask.GetMask("Stage"));
        Debug.DrawRay(origin, mag * hitvec.normalized, Color.black, 0.1f);
        LR.SetPosition(0, transform.position);
        if (hit.collider != null)
        {
            LR.SetPosition(1, hit.point);
        }
        else LR.SetPosition(1,origin+hitvec.normalized*mag);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("g");
    }
}
