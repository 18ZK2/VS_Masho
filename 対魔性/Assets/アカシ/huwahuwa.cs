using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huwahuwa : MonoBehaviour
{
    public float huwa = .2f;
    [SerializeField] float MoveRange = 10f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Vector3 pos = new Vector3(0, MoveRange*Mathf.Sin(Time.time * huwa),0);
        rb.MovePosition(pos + transform.position);
    }
}