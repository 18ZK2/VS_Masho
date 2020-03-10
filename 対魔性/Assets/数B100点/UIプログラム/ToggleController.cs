using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleController : MonoBehaviour
{
    [SerializeField] GameObject panel,SetOn;
    bool Toggle=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleClicked()
    {
        this.gameObject.SetActive(false);
        panel.SetActive(Toggle);
        Toggle = !Toggle;
        SetOn.SetActive(Toggle);
    }
}
