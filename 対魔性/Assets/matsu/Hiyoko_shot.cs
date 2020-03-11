using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiyoko_shot : MonoBehaviour
{
    public GameObject bullet;
    Animator ani;
    // Start is called before the first frame update
    private void Shot()
    {
        Instantiate(bullet, transform);
        
    }
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ani.SetTrigger("Shot");
        }
    }
}