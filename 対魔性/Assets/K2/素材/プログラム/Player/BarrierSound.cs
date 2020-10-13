using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class BarrierSound : MonoBehaviour
{
    [SerializeField] AudioClip se;
    bool played = false;
    AudioSource ass;
    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>();
        ass.playOnAwake = false;
        ass.volume = 0.5f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ass.PlayOneShot(se);
    }
}
