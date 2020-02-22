using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの操作について
public class PlayerContloller : MonoBehaviour
{

    public float speed;
    [System.NonSerialized] public Vector3 bodyVec;
    [System.NonSerialized] public Quaternion armRot;
    

    [SerializeField] float dashPow = 0;
    [SerializeField] GameObject cam = null;

    GrabbingBeam gb = null;
    bool dash;
    //左右移動用
    Vector2 walkVec;

    Animator anm;
    Rigidbody2D rb;
    

    private void Dash()
    {
        rb.AddForce(bodyVec.normalized * dashPow, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        anm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gb = transform.Find("体/左腕").GetComponentInChildren<GrabbingBeam>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //横方向入力
        walkVec = Vector2.right * Input.GetAxis("Horizontal");
        dash = Input.GetMouseButton(1) || Input.GetMouseButtonDown(1);

        //マウスの入力から向かうべき向きを作る
        //bodyVecが向かうべきベクトル
        Vector3 mousePos = Input.mousePosition;
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

        bodyVec = mousePos - pos;
        armRot = Quaternion.LookRotation(Vector3.forward, bodyVec);

        //マウスを押すとビーム発射
        if (Input.GetMouseButtonDown(0))
        {
            gb.MakeBeams(bodyVec.normalized);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gb.DeleteBeams();
        }

    }

    private void LateUpdate()
    {
        //プレイヤーの移動が終わった後にカメラを移動
        cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position, 2.0f * Time.deltaTime);
        Vector3 cp = cam.transform.position;
        cam.transform.position = new Vector3(cp.x, cp.y, -10);
    }

    private void FixedUpdate()
    {
        //浮遊
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - 33);
        if (Physics2D.Raycast(origin,Vector2.down,8f))
        {
            rb.AddForce(Vector2.up * rb.mass * rb.gravityScale * 10f, ForceMode2D.Force);
        }
        //走る
        rb.AddForce(walkVec*speed);

        //ダッシュ
        anm.SetBool("dash", dash);
    }
}
