using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerContloller pc =  collision.gameObject.GetComponent<PlayerContloller>();
            pc.Damage(pc.PlayerHp);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerContloller pc = collision.gameObject.GetComponent<PlayerContloller>();
            pc.Damage(pc.PlayerHp);
        }
    }
}
