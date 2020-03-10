using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyshot : MonoBehaviour
{
    [SerializeField] const int MAX_ENEMY = 5;
    private Transform targetObj;
    private Transform enemyList;
    //private float colliderOffset = 0f;
    private Animator shoter = null;
    private EnemyContloller ec;
    public GameObject tumura;
    //float x, y, z;

    int EnemyCount()
    {
        int count;
        EnemyContloller[] childs = enemyList.GetComponentsInChildren<EnemyContloller>();
        count = childs.Length;
        return count;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("Player").transform;
        enemyList = transform.Find("EnemyList");
        ec = GetComponent<EnemyContloller>();
        /*
         * colliderOffsetで自身の当たり判定を引こうとしている？
         */
        //colliderOffset = GetComponent<CapsuleCollider>().radius + targetObj.GetComponent<CapsuleCollider>().radius;
        shoter = GetComponent<Animator>();
        //Vector3 respown_t = GameObject.Find("発射機").transform.position;
        //x = respown_t.x;
        //y = respown_t.y;
        //z = respown_t.z;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = 0f;
        if(targetObj!=null)distance = Vector3.Distance(transform.position, targetObj.position);// - colliderOffset;
        Debug.Log(EnemyCount());
        if (distance <= 400 && EnemyCount()<MAX_ENEMY)
        {
            shoter.SetBool("distance", true);
        }
        else if (distance > 400 || EnemyCount() > MAX_ENEMY)
        {
            shoter.SetBool("distance", false);
        }
        if (ec.HP <= 0)
        {
            //親が死ぬと子も死ぬので親が死ぬとき子を開放する
            EnemyContloller[] childs = enemyList.GetComponentsInChildren<EnemyContloller>();
            foreach (var child in childs) child.transform.parent = null;
        }
    }
    void animation_event()
    {
        float x = transform.position.x - targetObj.position.x;
        GameObject g = Instantiate(tumura, transform);
        g.transform.parent = enemyList;
        if (x < 0) g.transform.Rotate(0, 180, 0);
        //五連続発射になるのでNG
        //tumuraを入れる配列で管理するとよい
        //倒されるとnullになるのでnullの数を数え、5未満なら生成など
        /*for (int i = 0; i > 5; i++)
        {
            GameObject enemy = Instantiate(tumura,transform);
            //enemy.transform.position = new Vector3(x, y, z);
        }*/
    }
}
