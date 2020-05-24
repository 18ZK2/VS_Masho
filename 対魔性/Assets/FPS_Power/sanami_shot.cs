using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sanami_shot : MonoBehaviour
{
    public float span = 3f;
    public float Dest_time = 5.0f;
    private float currentTime = 0f;
    public GameObject boul;
    public float speed = 1000;


    private void OnBecameInvisible()
    {

        enabled = false;
    }
    private void OnBecameVisible()
    {
        enabled = true;

    }
    void Start()
    {

    }


    void Update()
    {
        if (enabled == true)
        {
            currentTime += Time.deltaTime;
            if (currentTime > span)
            {

                GameObject bullets = Instantiate(boul) as GameObject;
                Vector3 force;
                force = this.gameObject.transform.forward * speed;
                bullets.GetComponent<Rigidbody2D>().AddForce(force);
                Destroy(bullets, Dest_time);
                currentTime = 0f;

            }

        }
        else
        {
            Debug.Log("画面外");
        }
    }
}


