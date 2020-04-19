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
    [SerializeField] GameObject Valcan = null;

    private GameObject hassya;
    private Animator anim = null;
    private AudioSource ass;
    private Text ammo;

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
        
        
    }
    void Reload()
    {
        magazine = magazineSize;
        ammo.text = magazine.ToString() + "/" + magazineSize.ToString();
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
        ammo.text = magazine.ToString()+ "/" + magazineSize.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isShot", isShot);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isShot = false;
    }
}
