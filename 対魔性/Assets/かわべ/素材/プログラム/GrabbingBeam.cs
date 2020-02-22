using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingBeam : MonoBehaviour
{
    public int beamLength = 0;
    public float shotPow = 0;

    [SerializeField] float preShotPowRate = 300;
    [SerializeField] GameObject element = null;
    [SerializeField] GameObject head=null;
    [SerializeField] List<GameObject> list = new List<GameObject>();
    [SerializeField] Rigidbody2D armBody = null,playerBody = null;

    float autoShotPow = 0;
    GrabbingHead gHead;

    public void MakeBeams(Vector2 vec)
    {
        GameObject ele;
        SpringJoint2D eleJoint;
        Rigidbody2D eleBody = null;
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
        GameObject h = Instantiate(head, transform.position,Quaternion.identity);
        list.Add(h);
        h.transform.parent = null;
        gHead = h.GetComponent<GrabbingHead>();
        h.GetComponent<SpringJoint2D>().connectedBody = eleBody;
        h.GetComponent<Rigidbody2D>().AddForce(vec * autoShotPow, ForceMode2D.Impulse);

    }
    public void DeleteBeams()
    {
        //ビームを削除
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
        if (shotPow == 0) autoShotPow = preShotPowRate * beamLength;
        else autoShotPow = shotPow;
    }



    // Update is called once per frame
    private void Update()
    {
        if (gHead!=null && list[0] != null && gHead.touched)
        {
            //ステージとキャラを固定する
            list[0].GetComponent<SpringJoint2D>().connectedBody = playerBody;

        }
    }

}
