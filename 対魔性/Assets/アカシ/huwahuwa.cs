using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huwahuwa : MonoBehaviour
{
    public float huwa = .2f;
    [SerializeField] float MoveRange = 10f;
    [SerializeField] GameObject bakuha = null,pikadon = null;
    [SerializeField] GameObject head = null, missile = null;
    Rigidbody2D rb;
    bool dead = false;
    IEnumerator Dead()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(bakuha, transform).transform.parent = null;
            yield return new WaitForSeconds(0.7f);
        }
        for (int i = 0; i < 8; i++)
        {
            Instantiate(bakuha, transform).transform.parent = null;
            yield return new WaitForSeconds(0.25f);
        }
        Instantiate(pikadon, transform).transform.parent = null;
        
        GameManager game = GameObject.Find("GameManager").GetComponent<GameManager>();
        yield return new WaitForSeconds(5f);
        game.StartCoroutine( game.WipeLoadScene("Title"));
        Destroy(transform.parent.gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {

        if (head == null && missile == null && !dead)
        {
            dead = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(Dead());
        }
        else {
            Vector3 pos = new Vector3(0, MoveRange * Mathf.Sin(Time.time * huwa), 0);
            rb.MovePosition(pos + transform.position);
        }
    }
}