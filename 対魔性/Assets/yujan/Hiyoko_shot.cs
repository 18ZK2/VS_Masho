using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiyoko_shot : MonoBehaviour
{
    EnemyContloller EnemyContloller;
    Quaternion quaternion;
    [SerializeField] float dis = 200.0f;
    public GameObject bullet,player;
    [SerializeField] AudioClip shotSE = null;
    [Header("回転")] [SerializeField] bool rot = false; //銃を回転させたい場合はtrue
    [Header("回転させる場合はRotOriginを代入")][SerializeField] GameObject RotOrigin = null;
    Animator ani;
    AudioSource ass;

    float bulletSpeed;
    // Start is called before the first frame update
    private void Shot()
    {
        ass.PlayOneShot(shotSE);
        GameObject b = Instantiate(bullet, transform);
        var mainModule = b.GetComponent<ParticleSystem>().main;
        mainModule.startLifetime = dis / bulletSpeed;
        b.transform.parent = null;
    }
    void Start()
    {

        ParticleSystem ps = bullet.GetComponent<ParticleSystem>();
        var mainModule = ps.main;
        bulletSpeed = mainModule.startSpeed.constant;
        EnemyContloller = transform.parent.gameObject.GetComponent<EnemyContloller>(); //if rotより先に書く(親が変わるため)
        if (rot == true) //回転させるとき
        {
            GameObject ro = Instantiate(RotOrigin, this.transform.position, Quaternion.identity) as GameObject; //回転軸(プレハブ)を作成
            ro.transform.parent = transform.parent; //RotOriginをTumuraの子にする
            gameObject.transform.parent = ro.transform; //GunをRotOriginの子にする
            ro.transform.localPosition= new Vector3(0, 0, 0); //回転軸を
            transform.localPosition = new Vector3(-10.0f,0,0);
        }
        player = GameObject.Find("Player");
        ani = GetComponent<Animator>();
        ass = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y); //Tumuraの場所
        RaycastHit2D hit = Physics2D.Raycast(origin, (-transform.right),dis, LayerMask.GetMask(new string[] { "Player", "Stage" })); //判定(当たらなければNull)
        Debug.DrawRay(origin, dis*(-transform.right), Color.blue, 0.1f);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player") //プレイヤーを発見
            {
                ani.SetTrigger("Shot");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (rot == true && player != null)
        {
            var vec = (player.transform.position - transform.parent.parent.position).normalized; //プレイヤーからTumuraへのベクトル parent.parentにする!!!!!(rootではだめ)
            if (vec.x > 0)
            {
                transform.parent.parent.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); //Tumuraを180°回転
                transform.parent.rotation = Quaternion.FromToRotation(Vector3.up, vec) * Quaternion.Euler(0, 180, -90);
            }
            else
            {
                transform.parent.parent.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                transform.parent.rotation = Quaternion.FromToRotation(Vector3.up, vec) * Quaternion.Euler(0, 0, -90);
            }
        }
    }
    
}