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

    // Update is called once per frame
    void Update()
    {
        if (played == false)
        {
            ass.PlayOneShot(se);
            played = true;
        }
    }
    private void OnDisable()
    {
        played = false;
        Debug.Log("aaa");
    }
}
