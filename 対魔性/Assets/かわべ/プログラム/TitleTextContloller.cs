using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTextContloller : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] float th;

    private void Update()
    {
        mat.SetFloat("_NoiseTh", th);
    }
}
