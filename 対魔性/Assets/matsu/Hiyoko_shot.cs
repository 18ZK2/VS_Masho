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
    [Header("回転させる場合はRotOriginを代入")][SerializeField] GameObject RotOrigin;
    Animator ani;
    AudioSource ass;
    [Header("回転")][SerializeField] bool rot = false; //銃を回転させたい場合はtrue
    // Start is called before the first frame update
    private void Shot()
    {
        ass.PlayOneShot(shotSE);
        Instantiate(bullet, transform).transform.parent = null;
    }
    void Start()
    {
        if (rot == true) //回転軸を作成
        {
            GameObject ro = Instantiate(RotOrigin, this.transform.position, Quaternion.identity) as GameObject;
            ro.transform.parent = transform.root; //RotOriginをTumuraの子にする
            gameObject.transform.parent = ro.transform; //GunをRotOriginの子にする
            ro.transform.localPosition= new Vector3(0, 0, 0);
            transform.localPosition = new Vector3(-10.0f,0,0);
        }
        player = GameObject.Find("Player");
        ani = GetComponent<Animator>();
        ass = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y); //Tumuraの場所
        RaycastHit2D hit = Physics2D.Raycast(origin, (-transform.right),dis); //判定(当たらなければNull)
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
        
        if (rot == true)
        {
            if (player != null)
            {
                var vec = (-transform.root.position + player.transform.position).normalized; //プレイヤーからTumuraへのベクトル
                if (vec.x > 0)
                {

                    transform.root.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); //Tumuraを180°回転
                    transform.parent.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec) * Quaternion.Euler(0, 180, -90);

                }
                else
                {
                    transform.root.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    transform.parent.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec) * Quaternion.Euler(0, 0, -90);
                }
                //transform.parent.Rotate(0, 0, 1.0f);

            }
        }
        if (transform.root.gameObject.GetComponent<EnemyContloller>().HP < 0) //親(Tumura)の体力が0未満になったとき
        {
            transform.parent.parent = null; //親と関係を解除
            Destroy(gameObject);
        }
    }
    
}