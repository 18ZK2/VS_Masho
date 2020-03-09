using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundContloller : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    Transform player;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rb != null) transform.Translate(speed * rb.velocity.x, 0, 0);

        if (Mathf.Abs(transform.localPosition.x) > 256f)
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
