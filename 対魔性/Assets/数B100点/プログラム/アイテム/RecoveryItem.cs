using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RecoveryItem : MonoBehaviour
{
    GameManager GameManager;
    [SerializeField] AudioClip SE1 = null;
    [SerializeField] AudioMixerGroup group = null;
    public int HealPonit = 1; //回復量
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerContloller pc = collision.gameObject.GetComponentInParent<PlayerContloller>();
            if (pc.PlayerHp < pc.MaxPlayerHp) //HPが最大でない
            {
                GameObject soundEffect = Instantiate(new GameObject(), transform);
                soundEffect.transform.parent = null;
                AudioSource ass = soundEffect.AddComponent<AudioSource>();
                ass.outputAudioMixerGroup = group;
                ass.PlayOneShot(SE1);
                Destroy(soundEffect, 3f);
                pc.PlayerHp += HealPonit;
            }
            Destroy(gameObject);
        }
        
    }
}
