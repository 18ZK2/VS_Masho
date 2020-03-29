using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValcanCreater : MonoBehaviour
{
    bool isLaunch = true;
    [Header("回数")][SerializeField] int n = 3;
    [SerializeField] float f = 100.0f;
    [SerializeField] GameObject Valcan=null;
    // Start is called before the first frame update
    public IEnumerator Createvalcan()
    {
        int i = 0;
        while (i < n)
        {
            float rand = Random.Range(-0.8f, 0.8f); //乱数生成
            GameObject preval = Instantiate(Valcan, 　new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
            preval.GetComponent<Rigidbody2D>().AddForce(-transform.right.x * f* new Vector2(1,rand), ForceMode2D.Impulse);
            i++;
            yield return new WaitForSeconds(.2f);
        }
        yield return new WaitForSeconds(.5f);
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
