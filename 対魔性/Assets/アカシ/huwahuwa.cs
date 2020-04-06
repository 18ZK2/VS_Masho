using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huwahuwa : MonoBehaviour
{
    public float huwa = .2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        transform.position = new Vector3(transform.position.x
         , 10 + Mathf.Sin(Time.frameCount * huwa), transform.position.z);

    }
}