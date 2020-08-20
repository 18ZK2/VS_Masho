using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullets
{
    public float speed;
    public GameObject bullet;
    public bool useLarm;
    public AudioClip SE;
};

public class ChocominController : MonoBehaviour
{

    public Bullets[] bullets = new Bullets[5];
    [SerializeField] float jumpPower = 1000;
    [SerializeField] float xspeed = 0f;
    [SerializeField] float landingLim = 5f;
    bool isground;
    bool running;

    AudioSource ass;
    Transform Larm, Rarm;
    Rigidbody2D rb;
    Animator anm;

    void ShootBullet(int i)
    {
        Quaternion q = (bullets[i].useLarm) ? Larm.rotation : Rarm.rotation;
        Vector3 pos = (bullets[i].useLarm) ? Larm.position : Rarm.position;
        GameObject b = Instantiate(bullets[i].bullet, pos, q);
        b.GetComponent<Rigidbody2D>().velocity = b.transform.right * bullets[i].speed;
        b.transform.parent = null;
        ass.PlayOneShot(bullets[i].SE);
        
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }
    void Run()
    {
        rb.AddForce(transform.right * xspeed, ForceMode2D.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {
        Larm = transform.Find("Larm");
        Rarm = transform.Find("Rarm");
        ass = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anm = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anm.SetBool("tyakuchi", isground);
    }
    private void FixedUpdate()
    {
        if (!isground) anm.SetFloat("yspeed", rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //地面についた！
        anm.SetFloat("yspeed", 0f);
        if (collision.gameObject.tag == "Stage") isground = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        float yvel = Mathf.Abs(rb.velocity.y);
        Debug.Log(yvel);
        //地面から離れた！
        if (collision.gameObject.tag == "Stage" && yvel > landingLim) isground = false;
    }
}
