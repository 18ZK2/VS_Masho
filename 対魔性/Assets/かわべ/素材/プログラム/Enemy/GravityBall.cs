using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBall : MonoBehaviour
{
    [SerializeField] float hormingPow = 1000f,distanceLimit = 500f;

    [SerializeField] AudioClip SE;
    [Header("音の減衰")][SerializeField] AnimationCurve soundCurve;

    Vector3 diff;

    Rigidbody2D rb;
    AudioSource ass;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ass = GetComponent<AudioSource>();
        GameObject t = GameObject.Find("Player");
        if (t != null) target = t.transform;
        ass.PlayOneShot(SE);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) diff = (target.position - transform.position);
        float mag = diff.magnitude;
        float volume = soundCurve.Evaluate(mag / distanceLimit);
        ass.volume = volume;
    }
    private void FixedUpdate()
    {
        rb.AddForce(diff.normalized * hormingPow, ForceMode2D.Force);

    }
}
