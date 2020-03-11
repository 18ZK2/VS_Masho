using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleController : MonoBehaviour
{
    [SerializeField] GameObject panel = null, SetOn = null;
    [SerializeField] AudioClip SE = null;
    GameObject gameManager;
    AudioSource AudioSource;
    int TimeMax = 1, TimeMin = 0;
    bool Toggle=true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    public void ToggleClicked()
    {
        gameManager.GetComponent<GameManager>().SoundEffect(SE);
        Time.timeScale = TimeSpeed(); //ゲームスピード変更
        this.gameObject.SetActive(false); //自身を隠す
        panel.SetActive(Toggle);　//パネルを隠す or　現す
        Toggle = !Toggle; //反転
        SetOn.SetActive(Toggle); //SetOnを隠す or 現す
    }
    int TimeSpeed()
    {
        if (Toggle == true)
        {
            return TimeMin; //設定画面を開いたときはゲームスピード0
        }
        else
        {
            return TimeMax; //設定画面を閉じたときはゲームスピード1
        }
    }
}
