using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitominController : MonoBehaviour
{
    public float attackPt = 0f;
    public bool immrtalDamage = false;
    public bool charged = false, attacking = false;

    [SerializeField] GameObject chinori = null;
    [SerializeField] GameObject chishibuki = null;
    [SerializeField] AudioClip SE = null;

    Text ammo;
    Animator anm;
    AudioSource ass;
    Collider2D col;


    private void Sound()
    {
        ass.PlayOneShot(SE);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        ammo = GameObject.Find("Ammo").GetComponent<Text>();
        ammo.text = attackPt.ToString("f1");
        anm = GetComponent<Animator>();
        anm.keepAnimatorControllerStateOnDisable = true;
        ass = GetComponent<AudioSource>();
        col = GetComponentInChildren<Collider2D>();
    }
    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        foreach(ContactPoint2D p in collision.contacts)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                EnemyContloller ec = collision.gameObject.GetComponent<EnemyContloller>();
                if (ec != null)
                {
                    if (immrtalDamage) ec.HP -= attackPt;//連続ダメージはいる
                    else ec.Damage(attackPt);//無敵時間がつく
                }
                //エフェクト
                var obj = Instantiate(chishibuki, p.point, transform.rotation);
                obj.transform.parent = null;
                if (collision.gameObject != null) obj.transform.parent = collision.gameObject.transform;
                Transform c = Instantiate(chinori, p.point, transform.rotation).transform;
                Destroy(c.gameObject, 2);
                c.parent = col.transform;

            }
        }
        
    }
}
