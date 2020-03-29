using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetFalse",0.05f);//遅らせたほうがいい?
    }

    void SetFalse()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
