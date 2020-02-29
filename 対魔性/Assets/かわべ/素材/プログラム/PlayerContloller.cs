using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの操作について
public class PlayerContloller : MonoBehaviour
{
    public float PlayerHp=15;
    public float MaxPlayerHp = 40;
    public float speed;

    private const int MAX_DASH_COUNT = 2;
    private int Dashcount = 0;
    private bool isDash = false;

    [System.NonSerialized] public bool isDamage = true;
    [System.NonSerialized] public Vector3 bodyVec;
    [System.NonSerialized] public Quaternion armRot;

    [SerializeField] float camSpeed = 0.5f;
    [SerializeField] float dashPow = 0;
    //ダメージ後の無敵時間
    [SerializeField] float immortalTime = 0.5f;
    [SerializeField] GameObject cam = null;

    GrabbingBeam gb = null;
    bool dash;
    //左右移動用
    Vector2 walkVec;

    Animator anm;
    Rigidbody2D rb;

    private IEnumerator Immortal()
    {
        isDamage = false;
        yield return new WaitForSeconds(immortalTime);
        isDamage = true;
        StopCoroutine(Immortal());
    }
    private void VecZero()
    {
        rb.velocity = Vector2.zero;
    }
    public void Damage(float attackPt)
    {
        if (isDamage)
        {
            PlayerHp -= attackPt;
            StartCoroutine(Immortal());
        }
    }
    private void Dash()
    {
        rb.AddForce(bodyVec.normalized * dashPow, ForceMode2D.Impulse);
        Dashcount++;
        isDash = false;
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
        dash = (Input.GetMouseButton(1) && Dashcount < MAX_DASH_COUNT) || Input.GetMouseButtonDown(1);

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
        cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position, camSpeed * Time.deltaTime);
        Vector3 cp = cam.transform.position;
        cam.transform.position = new Vector3(cp.x, cp.y, -10);
    }

    private void FixedUpdate()
    {
        //浮遊
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(origin, Vector2.down *40f, 40f, LayerMask.GetMask("Stage"));
        foreach (var r in hit2Ds){
            if (r.collider != null)
            {
                rb.AddForce(Vector2.up * rb.mass * rb.gravityScale * 10f, ForceMode2D.Force);
            }
        }
        //走る
        rb.AddForce(walkVec*speed);

        //ダッシュ
        anm.SetBool("dash", dash);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="New Sprite")
        {
            Dashcount = 0;
        }
    }
}
