using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpController : MonoBehaviour
{
    [SerializeField] EnemyContloller EnemyContloller=null;
    Slider slider;
    Vector3 def;
    // Start is called before the first frame update
    void Start()
    {
        def = transform.parent.transform.localRotation.eulerAngles;
        slider = transform.GetComponentInChildren<Slider>();
        slider.maxValue = EnemyContloller.HP;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = EnemyContloller.HP; //EnemyControllerからHPを参照
    }
    private void LateUpdate()
    {
        //　カメラと同じ向きに設定
        transform.rotation = Camera.main.transform.rotation;
    }
}
