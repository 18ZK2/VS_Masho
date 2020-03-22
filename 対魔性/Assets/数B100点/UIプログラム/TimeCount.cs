using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    Text timerText;
    public static float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer = Mathf.Ceil(timer * 1000.0f) / 1000.0f;
        timerText.text = "Time:" + timer;
    }
}
