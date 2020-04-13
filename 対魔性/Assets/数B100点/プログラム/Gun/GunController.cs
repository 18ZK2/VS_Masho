using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    Vector2 vec;
    [SerializeField] float f = 100.0f;
    [SerializeField] GameObject Valcan = null;
    [Header("連射間隔")] [SerializeField] float delay = 0.2f;
    private GameObject hassya;
    private Animator anim = null;
    public bool isShot = true;
    public IEnumerator CreateBullet()
    {
        GameObject preval = Instantiate(Valcan, vec, transform.rotation) as GameObject;
        preval.GetComponent<Rigidbody2D>().AddForce(f * -transform.right, ForceMode2D.Impulse);
        yield return new WaitForSeconds(delay);
        isShot = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        hassya = gameObject.transform.Find("hassya").gameObject;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        vec = hassya.transform.position;
        if (Input.GetKey(KeyCode.Space)&&isShot)
        {
            StartCoroutine("CreateBullet");
            isShot = false;
        }
    }
}
