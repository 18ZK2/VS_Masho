using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFindCam : MonoBehaviour
{
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.rootCanvas.worldCamera = Camera.main;
        canvas.rootCanvas.sortingLayerName = "Master_Front";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
