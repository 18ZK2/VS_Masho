using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingHead : MonoBehaviour
{
    public GameObject gimmickEffect;
    [System.NonSerialized] public bool touched;
    [System.NonSerialized] public GameObject touchedObject = null, exitObject = null;
    [SerializeField] AudioClip[] SEs = new AudioClip[2];

    bool isActive = true;
    Rigidbody2D rb;
    SpringJoint2D joint;
    ParticleSystem ps;
    AudioSource asc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<SpringJoint2D>();
        ps = GetComponent<ParticleSystem>();
        asc = GetComponent<AudioSource>();
        asc.PlayOneShot(SEs[0]);
        touched = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (isActive)
        {
            touchedObject = collision.gameObject;
            asc.PlayOneShot(SEs[1]);
            isActive = false;
            touched = true;
            ps.TriggerSubEmitter(0);

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isActive)
        {
            exitObject = collision.gameObject;
        }
    }
}
