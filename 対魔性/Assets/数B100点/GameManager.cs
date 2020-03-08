using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Material wipe = null;
    PlayerContloller pc;
    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        pc = GameObject.Find("Player").GetComponent<PlayerContloller>();
        wipe.SetFloat("_Radius", 1f);
    }
    IEnumerator WipeLoadScene()
    {
        //Destroy(pc.transform,1);
        for (float wipetime = 1f; wipetime > 0f; wipetime-=0.01f)
        {
            wipe.SetFloat("_Radius", wipetime);
            yield return null;
        }
        SceneManager.LoadScene("GameOver");
        StopAllCoroutines();
    }
    // Update is called once per frame
    void Update()
    {

        if (pc.PlayerHp <= 0) StartCoroutine(WipeLoadScene());
    }

    public void SoundEffect(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }
}
