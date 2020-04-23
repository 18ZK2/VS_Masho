using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitominController : MonoBehaviour
{
    [SerializeField] GameObject chinori = null;
    [SerializeField] GameObject chishibuki = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        foreach(ContactPoint2D p in collision.contacts)
        {
            if (p.collider.gameObject.tag == "Enemy")
            {
                Instantiate(chishibuki, p.point, transform.rotation).transform.parent =collision.gameObject.transform;
                Instantiate(chinori, p.point, transform.rotation).transform.parent = transform;
            }
        }
        
    }
    private void OnDisable()
    {
        foreach(var c in GetComponentsInChildren<Transform>())
        {
            if (c != transform) Destroy(c.gameObject);
        }
    }
}
