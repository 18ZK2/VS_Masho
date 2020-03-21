using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    Text timerText;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = "Time:" + Mathf.Ceil(timer * 1000.0f)/1000.0f;
    }
}
