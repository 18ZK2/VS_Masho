using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{

    public bool isShot = true;
    public int magazine = 0;
    
  
    [SerializeField] int magazineSize = 25;
    [SerializeField] AudioClip ReloadSE = null,shotSE = null;
    [SerializeField] float f = 100.0f, accuracy = 15f;
    [SerializeField] GameObject Valcan = null, magazineObj = null;
    [SerializeField] Gradient grad = null;

    [SerializeField]float heatGrad;
    private GameObject hassya;
    private Animator anim = null;
    private AudioSource ass;
    private Text ammo;
    private SpriteRenderer heatBullel;

    IEnumerator cd;
    IEnumerator CoolDown()
    {
        while (true)
        {

            //Debug.Log(heatGrad);
            heatBullel.color = grad.Evaluate(heatGrad);
            if (heatGrad > 0) heatGrad -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    void Shot()
    {
        
        ass.PlayOneShot(shotSE);

        float rand = Random.Range(-accuracy, accuracy);
        Quaternion q = Quaternion.Euler(0, 0, rand);

        GameObject preval = Instantiate(Valcan, hassya.transform.position, transform.rotation * q) as GameObject;
        preval.GetComponent<Rigidbody2D>().AddForce(f * preval.transform.right, ForceMode2D.Impulse);

        isShot = false;
        magazine--;
        ammo.text = magazine.ToString() + "/" + magazineSize.ToString();
        if (magazine < 1)
        {
            anim.SetTrigger("Reload");
        }
        if (heatGrad < 1) heatGrad += (float)1 / magazineSize;
        
        
    }
    void Reload()
    {
        magazine = magazineSize;
        ammo.text = magazine.ToString() + "/" + magazineSize.ToString();
    }
    void DropMagazine()
    {
        GameObject m = Instantiate(magazineObj, transform.position, transform.rotation);
        Destroy(m, 5);
        m.transform.parent = null;
    }
    public void ReloadSound()
    {
        ass.PlayOneShot(ReloadSE);
    }
    // Start is called before the first frame update
    void Start()
    {

        hassya = gameObject.transform.Find("hassya").gameObject;
        anim = GetComponent<Animator>();
        ass = GetComponent<AudioSource>();
        magazine = magazineSize;
        ammo = GameObject.Find("Ammo").GetComponent<Text>();
        anim.keepAnimatorControllerStateOnDisable = true;
        ammo.text = magazine.ToString()+ "/" + magazineSize.ToString();

        heatBullel = transform.Find("赤熱部").GetComponent<SpriteRenderer>();
        //StartCoroutine(CoolDown());
        cd = CoolDown();
        StartCoroutine(cd);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isShot", isShot);
        if (cd == null)
        {
            cd = CoolDown();
            StartCoroutine(cd);
        }
    }
    private void OnDisable()
    {
        if (cd != null) StopCoroutine(cd);
        cd = null;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isShot = false;
    }
    private void OnEnable()
    {
        if(ammo!=null)ammo.text = "m-m5";
    }
}
