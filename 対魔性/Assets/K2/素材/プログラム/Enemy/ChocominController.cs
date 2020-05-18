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
    AudioSource ass;
    Transform Larm, Rarm;

    void ShootBullet(int i)
    {
        Quaternion q = (bullets[i].useLarm) ? Larm.rotation : Rarm.rotation;
        Vector3 pos = (bullets[i].useLarm) ? Larm.position : Rarm.position;
        GameObject b = Instantiate(bullets[i].bullet, pos, q);
        b.GetComponent<Rigidbody2D>().velocity = b.transform.right * bullets[i].speed;
        b.transform.parent = null;
        ass.PlayOneShot(bullets[i].SE);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Larm = transform.Find("Larm");
        Rarm = transform.Find("Rarm");
        ass = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
