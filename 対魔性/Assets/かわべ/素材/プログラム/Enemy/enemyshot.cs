using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyshot : MonoBehaviour
{
    [SerializeField] Transform targetObj;
    private Animator shoter = null;
    public GameObject tumura;
    public float distance_ani;
    private int numberOfEnemys;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        /*
         * colliderOffsetで自身の当たり判定を引こうとしている？
         */
        //colliderOffset = GetComponent<CapsuleCollider>().radius + targetObj.GetComponent<CapsuleCollider>().radius;
        shoter = GetComponent<Animator>();
        numberOfEnemys = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log(count);
        var distance = Vector3.Distance(transform.position, targetObj.position);
        if (distance <= distance_ani)
        {
            shoter.SetBool("distance", true);
        }
        else if (distance > distance_ani)
        {
            shoter.SetBool("distance", false);
        }
    }
    void animation_event()
    {
        //倒されるとnullになるのでnullの数を数え、5未満なら生成など
        if(numberOfEnemys<5 ||count<5)
        {
            Instantiate(tumura,transform);
            numberOfEnemys++;
        }
        
    }
}
