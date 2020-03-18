using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRot : MonoBehaviour
{
    bool col = true,rot=false;
    float sleeptime;
    [SerializeField] float rotspeed = 5.0f;
    IEnumerator Sleep()
    {
        for (int i = 0; i < sleeptime; i++)
        {
            yield return null;
        }
        rot = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        sleeptime = GetComponent<FireController>().n;

    }

    // Update is called once per frame
    void Update()
    {
        if (col == true) //一度だけコルーチンを実行
        {
            StartCoroutine("Sleep");
            col = !col;
        }
        if (rot == true)
        {
            transform.Rotate(0,0,rotspeed);
        }
    }
}
