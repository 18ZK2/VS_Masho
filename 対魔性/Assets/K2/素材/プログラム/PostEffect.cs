using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect : MonoBehaviour
{
    public Material wipeCircle = null;

    private IEnumerator OpenCircle()
    {
        for (float wipetime = 0f; wipetime < 1f; wipetime += 0.01f)
        {
            wipeCircle.SetFloat("_Radius", wipetime);
            yield return null;
        }
        StopCoroutine(OpenCircle());
    }

    private void Start()
    {
        StartCoroutine(OpenCircle());
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, wipeCircle);
    }
}
