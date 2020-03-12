using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpController : MonoBehaviour
{   
    GameObject Enemy;
    EnemyContloller EnemyContloller;
    Slider slider;
    Vector3 def;
    // Start is called before the first frame update
    void Start()
    {
        def = transform.parent.transform.localRotation.eulerAngles;
        Enemy = transform.root.gameObject; //親オブジェクト取得
        EnemyContloller = Enemy.GetComponent<EnemyContloller>();
        slider = GameObject.Find("EnemyHealth").GetComponent<Slider>();
        slider.maxValue = EnemyContloller.HP;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 parent = transform.root.transform.localRotation.eulerAngles;
        transform.parent.transform.localRotation = Quaternion.Euler(def - parent); //子が回転しないよう調整
        slider.value = EnemyContloller.HP; //EnemyControllerからHPを参照
    }
}
