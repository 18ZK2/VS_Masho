using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContloller : MonoBehaviour
{
    public float HP = 1.0f;
    [System.NonSerialized] public bool isDamage = true;

    //接触ダメージ
    [SerializeField] float attackPt = 1f;
    //ダメージ後の無敵時間
    [SerializeField] float immortalTime = 0.5f;
    
    //必ず落とす
    [SerializeField] GameObject mustDrop = null;
    //確率で落とす
    [SerializeField] GameObject[] dropItem = null;
    //dropProba が ランダムな値0~1より大きいとドロップ
    [SerializeField] float[] dropProba = null;

    

    private IEnumerator Immortal()
    {
        isDamage = false;
        yield return new WaitForSeconds(immortalTime);
        isDamage = true;
        StopCoroutine(Immortal());
    }

    //呼び出される
    public void Damage(float attackPow)
    {
        if (isDamage)
        {
            HP -= attackPow;
            HP = Mathf.Round(HP * 10.0f) / 10.0f; //10倍して四捨五入->10で割る
            StartCoroutine(Immortal());
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 1)
        {
            //死ぬとき
            if (dropItem.Length != 0)//ドロップアイテムが設定されているとき
            {
                int i = Random.Range(0, dropItem.Length);//落とすアイテムの種類を抽選
                bool isDrop = dropProba[i] > Random.Range(0f, 1f);//落とすかどうかを抽選
                if (isDrop) Instantiate(dropItem[i], transform);
            }
            if (mustDrop != null) Instantiate(mustDrop, transform);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerContloller pc = collision.gameObject.GetComponent<PlayerContloller>();
            pc.Damage(attackPt);
        }
    }
}
