using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiyoko_shot : MonoBehaviour
{
    Vector3 pos, pla;
    Quaternion quaternion;
    [SerializeField] float dis = 200.0f;
    public GameObject bullet,player;
    [SerializeField] AudioClip shotSE = null;
    Animator ani;
    AudioSource ass;
    [SerializeField] bool rot = false; //銃を回転させたい場合はtrue
    // Start is called before the first frame update
    private void Shot()
    {
        ass.PlayOneShot(shotSE);
        Instantiate(bullet, transform);
    }
    void Start()
    {
        player = GameObject.Find("Player");
        ani = GetComponent<Animator>();
        ass = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y); //FlyingTumuraの場所
        RaycastHit2D hit = Physics2D.Raycast(origin, (-transform.right),dis, LayerMask.GetMask("Player")); //判定
        Debug.DrawRay(origin, dis*(-transform.right), Color.blue, 0.1f);
        if (hit.collider) //プレイヤーを発見
        {
            ani.SetTrigger("Shot");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (rot == true)
        {
            var vec = (-transform.position + player.transform.position).normalized;
            transform.parent.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec)*Quaternion.Euler(0,0,-90);
        }
        if (transform.parent.gameObject.GetComponent<EnemyContloller>().HP < 0) //親(Tumura)の体力が0未満になったとき
        {
            transform.parent = null; //親と関係を解除
            Destroy(gameObject);
        }
    }
    
}