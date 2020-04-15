using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [System.NonSerialized] public bool on=false;
    [SerializeField] Sprite button1;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (on) sr.sprite = button1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!on&& collision.gameObject.CompareTag("PlayerAttack")) 
        {
            on = true;
            sr.sprite = button1;
        }
    }
}
