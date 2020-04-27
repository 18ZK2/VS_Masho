using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKItem : MonoBehaviour
{
    //[Header("saveIndexには書き換えたい値の番号を入力")]
    
    [Header("4:MaxHp 5:speed 6:dashPow 7:MaxStamina 8:recovSpeed")]
    [Header("0:PlayerValue 1:makedData 2:CanUseGun 3:CanUseAx")]
    [SerializeField] int saveIndex = 1;
    [Header("2,3版はbool その他はfloat")]
    [Header("valueには書き換えたい値を入力")]
    [SerializeField] string value = "";
    [SerializeField] AudioClip SE1;

    PlayerContloller pc;
    AudioSource AudioSource;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.SoundEffect(SE1);
            pc = collision.gameObject.GetComponentInParent<PlayerContloller>();
            if (!(saveIndex==2 || saveIndex == 3))
            {
                float val = (float)System.Convert.ToDouble(value);
                switch (saveIndex)
                {
                    case 4:
                        val += pc.MaxPlayerHp;
                        break;
                    case 5:
                        val += pc.speed;
                        break;
                    case 6:
                        val += pc.dashPow;
                        break;
                    case 7:
                        val += pc.MAX_STAMINA;
                        break;
                    case 8:
                        val += pc.recoverySpeed;
                        break;
                    default:
                        break;
                }
                gm.Save(saveIndex, System.Convert.ToString(val));
            }
            else
            {
                gm.Save(saveIndex, value);
            }
            gm.Load();
            Destroy(gameObject);
        }
        

    }
}
