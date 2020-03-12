using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiyoko_shot : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.gameObject.GetComponent<EnemyContloller>().HP < 0) //親(Tumura)の体力が0未満になったとき
        {
            transform.parent = null; //親と関係を解除
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ani.SetTrigger("Shot");
        }
    }
}