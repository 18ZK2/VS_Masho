using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitominController : MonoBehaviour
{
    public float attackPt = 0f;
    public bool charged = false,attacking = false;

    [SerializeField] GameObject chinori = null;
    [SerializeField] GameObject chishibuki = null;
    Text ammo;
    Animator anm;
    IEnumerator colutine;
    IEnumerator CleanUpBlade()
    {
        while (true)
        {
            foreach (var c in GetComponentsInChildren<Transform>())
            {
                if (c != transform) Destroy(c.gameObject);
                yield return new WaitForSeconds(0.8f);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ammo = GameObject.Find("Ammo").GetComponent<Text>();
        ammo.text = attackPt.ToString("f1");
        anm = GetComponentInParent<Animator>();
        anm.keepAnimatorControllerStateOnDisable = true;
    }
    private void OnEnable()
    {
        colutine = CleanUpBlade();
        StartCoroutine(colutine);
    }
    private void OnDisable()
    {
        StopCoroutine(colutine);
        colutine = null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        foreach(ContactPoint2D p in collision.contacts)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                EnemyContloller ec = collision.gameObject.GetComponent<EnemyContloller>();
                if (ec != null) ec.Damage(attackPt);
                var obj = Instantiate(chishibuki, p.point, transform.rotation);

                obj.transform.parent = null;
                if (collision.gameObject != null) obj.transform.parent = collision.gameObject.transform;
                Instantiate(chinori, p.point, transform.rotation).transform.parent = transform;
            }
        }
        
    }
}
