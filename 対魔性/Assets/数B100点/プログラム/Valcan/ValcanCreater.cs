using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValcanCreater : MonoBehaviour
{
    float angle, y = 50.0f;
    Vector2 rotvec;
    bool isLaunch = true;
    [Header("連射回数")][SerializeField] int n = 3;
    [Header("パワー")][SerializeField] float f = 2000.0f;
    [Header("連射間隔")] [SerializeField] float delay = 0.2f;
    [Header("次に撃つまでの間隔")] [SerializeField] float delayN = 0.5f;

    [SerializeField] GameObject Valcan=null;
    // Start is called before the first frame update
    public IEnumerator Createvalcan()
    {
        int i = 0;
        //n回連射
        while (i < n)
        {
            if (transform.rotation.eulerAngles.y == 0) angle = transform.rotation.eulerAngles.z+180.0f;//Euler角から変換
            else angle = -transform.rotation.eulerAngles.z; //y==180の時はEulerのz=-zとなる
            //角度からベクトルを作成
            rotvec.x = y*Mathf.Cos(angle * Mathf.Deg2Rad);
            rotvec.y = y*Mathf.Sin(angle * Mathf.Deg2Rad);
            float rand = Random.Range(-15f, 15f); //乱数生成(15°から-15°まで回転)
            GameObject preval = Instantiate(Valcan,new Vector2(transform.position.x+rotvec.x, transform.position.y-5.0f+rotvec.y), Quaternion.identity) as GameObject;
            var vec = Quaternion.Euler(0, 0, rand) * rotvec;//かける順番あり(逆はだめ)
            preval.GetComponent<Rigidbody2D>().AddForce(f * vec, ForceMode2D.Impulse);
            i++;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(delayN);
        isLaunch = true;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isLaunch) StartCoroutine("Createvalcan");
        isLaunch = false;
    }
}
