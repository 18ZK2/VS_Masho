using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MameController : MonoBehaviour
{
    GimmickContloller gimmick;
    EnemyContloller ec;
    // Start is called before the first frame update
    void Start()
    {
        
        if (transform.parent != null && transform.parent.GetComponent<GimmickContloller>()!=null)
        {
            gimmick = transform.parent.GetComponent<GimmickContloller>();
            Joint2D j = GetComponent<Joint2D>();
            j.connectedBody = gimmick.transform.GetComponent<Rigidbody2D>();
        }
        ec = GetComponent<EnemyContloller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gimmick != null)
        {
            if (ec.HP < 0)
            {
                gimmick.enabled = true;
                gimmick.tag = "Gimmick";
            }
            else if (ec.HP > 0)
            {
                gimmick.enabled = false;
                gimmick.tag = "Untagged";
            }
        }
    }
}
