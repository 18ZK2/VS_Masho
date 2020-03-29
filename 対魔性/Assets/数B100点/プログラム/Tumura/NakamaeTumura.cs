using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NakamaeTumura : MonoBehaviour
{
    Vector2 diff;
    GameObject player;
    TumuraContloller TumuraContloller;
    [Header("距離")] [SerializeField] float disx = 1000.0f,disy=100.0f;
    Rigidbody2D rb;
    [Header("Force")][SerializeField] float f = 12.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            diff = (player.transform.position - transform.position); //TumuraからPlayerまでのベクトル
            if (Mathf.Abs(diff.x) < disx && Mathf.Abs(diff.y) < disy)
            {
                rb.AddForce(new Vector2(diff.x * f, 0.00f));
                if (diff.x > 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

                }
            }
            
        }
        
    }
}
