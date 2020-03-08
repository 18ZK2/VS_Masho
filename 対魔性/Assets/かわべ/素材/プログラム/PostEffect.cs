using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect : MonoBehaviour
{
    [SerializeField] Material wipeCircle = null;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, wipeCircle);
    }
}
