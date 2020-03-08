using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerContloller pc;
    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        pc = GameObject.Find("Player").GetComponent<PlayerContloller>();
    }

    // Update is called once per frame
    void Update()
    {

        if (pc.PlayerHp <= 0) SceneManager.LoadScene("GameOver");
    }

    public void SoundEffect(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }
}
