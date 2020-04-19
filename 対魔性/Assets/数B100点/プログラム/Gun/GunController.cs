using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{

    public bool isShot = true;
    public int magazine = 0;
    public Text ammo;
    public Text MAX_ammo;
  
    [SerializeField] int magazineSize = 25;
    [SerializeField] AudioClip ReloadSE = null,shotSE = null;
    [SerializeField] float f = 100.0f, accuracy = 15f;
    [SerializeField] GameObject Valcan = null;
    //[Header("連射間隔")] [SerializeField] float delay = 0.2f;

    private GameObject hassya;
    private Animator anim = null;
    private AudioSource ass;

    void Shot()
    {
        magazine--;
        ass.PlayOneShot(shotSE);

        float rand = Random.Range(-accuracy, accuracy);
        Quaternion q = Quaternion.Euler(0, 0, rand);

        GameObject preval = Instantiate(Valcan, hassya.transform.position, transform.rotation * q) as GameObject;
        preval.GetComponent<Rigidbody2D>().AddForce(f * preval.transform.right, ForceMode2D.Impulse);

        isShot = true;
       
    }
    void Reload()
    {
        magazine = magazineSize;
        //gameObject.SetActive(false);
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
        ammo.text = magazine.ToString();
        MAX_ammo.text= "/" + magazineSize.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isShot", isShot);
        if (magazine <= 0 && isShot)
        {
            anim.SetTrigger("Reload");
            isShot = false;
        }
        ammo.text = magazine.ToString() ;
        MAX_ammo.text = "/" + magazineSize.ToString();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isShot = false;
    }
}
