using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingBeam : MonoBehaviour
{
    
    [SerializeField] int beamLength = 0;
    [SerializeField] float shotPow = 0;
    [SerializeField] GameObject element = null;
    [SerializeField] GameObject head=null;
    [SerializeField] Rigidbody2D armBody = null,playerBody = null;

    List<GameObject> list = new List<GameObject>();
    GrabbingHead gHead;

    public void MakeBeams(Vector2 vec)
    {
        //ワイヤーを生成
        GameObject ele;
        SpringJoint2D eleJoint;
        Rigidbody2D eleBody = null;
        //ワイヤーの紐要素を作成し変数listに追加
        for (int i = 0; i < beamLength; i++)
        {


            //紐の要素を作り、後ろの要素と結合させる。

            ele = Instantiate(element, transform.position, Quaternion.identity);
            eleJoint = ele.GetComponent<SpringJoint2D>();
            eleBody = ele.GetComponent<Rigidbody2D>();

            ele.transform.parent = null;
            ele.name = i.ToString();
            list.Add(ele);

            if (i != 0 && list[i - 1]!=null)
            {
                Rigidbody2D beforeRigid = list[i - 1].GetComponent<Rigidbody2D>();
                eleJoint.connectedBody = beforeRigid;
                
            }
            else
            {
                eleJoint.connectedBody = armBody;
                eleJoint.autoConfigureDistance = false;

            }
        }

        //ワイヤーの他オブジェクトにくっつく部分を作成
        GameObject h = Instantiate(head, transform.position,Quaternion.identity);
        list.Add(h);
        h.transform.parent = null;
        gHead = h.GetComponent<GrabbingHead>();
        //ワイヤーの末端に接続
        h.GetComponent<SpringJoint2D>().connectedBody = eleBody;
        //発射
        h.GetComponent<Rigidbody2D>().AddForce(vec * shotPow, ForceMode2D.Impulse);

    }
    public void DeleteBeams()
    {
        //ワイヤーを削除
        foreach(GameObject g in list)
        {
            if (gameObject.name != g.gameObject.name) Destroy(g.gameObject);
        }
        list.Clear();
        gHead = null;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (gHead == null || list[0] == null) return;

        switch (gHead.touchedObjectTag)
        {
            //以下にワイヤーの末端が触れた時の挙動
            case "Stage":
                //ステージとキャラを固定する
                list[0].GetComponent<SpringJoint2D>().connectedBody = playerBody;
                //末端を固定
                list[beamLength].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                break;
            default:
                break;

        }
    }
}
