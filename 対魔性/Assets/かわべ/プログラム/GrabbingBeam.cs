using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingBeam : MonoBehaviour
{
    public int beamLength = 0;
    public float shotPow = 0;
    [SerializeField] GameObject element = null;
    [SerializeField] GameObject head=null;
    [SerializeField]List<GameObject> list = new List<GameObject>();
    Vector3 mousePos;

    void MakeBeams()
    {
        GameObject ele; ;
        SpringJoint2D eleJoint;
        Rigidbody2D eleBody = null;
        for (int i = 0; i < beamLength; i++)
        {


            //紐の要素を作り、後ろの要素と結合させる。

            ele = Instantiate(element, transform);
            eleJoint = ele.GetComponent<SpringJoint2D>();
            eleBody = ele.GetComponent<Rigidbody2D>();

            ele.transform.parent = transform;
            ele.name = i.ToString();
            list.Add(ele);

            if (i != 0 && list[i - 1]!=null)
            {
                Rigidbody2D beforeRigid = list[i - 1].GetComponent<Rigidbody2D>();
                eleJoint.connectedBody = beforeRigid;
                
            }
            else
            {
                eleJoint.autoConfigureDistance = false;
                
            }
        }
        GameObject h = Instantiate(head, transform);
        h.GetComponent<SpringJoint2D>().connectedBody = eleBody;
        h.GetComponent<Rigidbody2D>().AddForce(mousePos * shotPow, ForceMode2D.Impulse);

    }
    void DeleteBeams()
    {
        foreach(Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (gameObject.name != t.gameObject.name) Destroy(t.gameObject);
        }
        list.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    int shotnum = 0;
    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        
        if (Input.anyKeyDown)
        {
            
            if (shotnum % 2 == 0)
            {
                MakeBeams();
            }
            else
            {
                DeleteBeams();
            }
            shotnum++;
        }
        
    }
}
