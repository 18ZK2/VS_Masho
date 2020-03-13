using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiyoko_shot : MonoBehaviour
{
    [SerializeField] float dis = 200.0f;
    public GameObject bullet;
    [SerializeField] AudioClip shotSE = null;
    Animator ani;
    AudioSource ass;
    // Start is called before the first frame update
    private void Shot()
    {
        ass.PlayOneShot(shotSE);
        Instantiate(bullet, transform);
    }
    void Start()
    {
        ani = GetComponent<Animator>();
        ass = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y); //FlyingTumuraの場所
        RaycastHit2D hit = Physics2D.Raycast(origin, (-transform.right),dis, LayerMask.GetMask("Player"));
        Debug.DrawRay(origin, dis*(-transform.right), Color.blue, 0.1f);
        if (hit.collider)
        {
            ani.SetTrigger("Shot");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.parent.gameObject.GetComponent<EnemyContloller>().HP < 0) //親(Tumura)の体力が0未満になったとき
        {
            transform.parent = null; //親と関係を解除
            Destroy(gameObject);
        }
    }
    
}