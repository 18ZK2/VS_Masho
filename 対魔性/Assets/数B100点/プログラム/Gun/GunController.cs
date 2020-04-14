using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public bool isShot = true;
    public int magazine = 0;

    [SerializeField] int magazineSize = 25;
    [SerializeField] AudioClip ReloadSE = null;
    [SerializeField] float f = 100.0f;
    [SerializeField] GameObject Valcan = null;
    [Header("連射間隔")] [SerializeField] float delay = 0.2f;

    private GameObject hassya;
    private Animator anim = null;
    private AudioSource ass;
    private Vector2 vec;
    
    public IEnumerator CreateBullet()
    {
        magazine--;
        GameObject preval = Instantiate(Valcan, vec, transform.rotation) as GameObject;
        preval.GetComponent<Rigidbody2D>().AddForce(f * -transform.right, ForceMode2D.Impulse);
        yield return new WaitForSeconds(delay);
        isShot = true;
    }
    public void Reload()
    {
        ass.PlayOneShot(ReloadSE);
        magazine = magazineSize;
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        hassya = gameObject.transform.Find("hassya").gameObject;
        anim = GetComponent<Animator>();
        ass = GetComponent<AudioSource>();
        magazine = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        vec = hassya.transform.position;
        if(magazine < 0 && isShot)
        {
            anim.SetTrigger("Reload");
            isShot = false;
        }
        else if (Input.GetKey(KeyCode.Space) && isShot)
        {
            StartCoroutine("CreateBullet");
            isShot = false;
        }
        anim.SetBool("isShot",isShot);
    }
}
