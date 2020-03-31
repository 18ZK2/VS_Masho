     using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTextContloller : MonoBehaviour
{
    [SerializeField] Color damageColor = Color.white;

    [SerializeField] Material mat = null;
    [SerializeField] float th = 1;
    SpriteRenderer[] bodyRenderer;

    private void ChangeBodyColor()
    {
        foreach(var b in bodyRenderer)
        {
            b.color = damageColor;
        }
    }
    private void Start()
    {
        bodyRenderer = GetComponentsInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        mat.SetFloat("_NoiseTh", th);
        ChangeBodyColor();
    }
}
