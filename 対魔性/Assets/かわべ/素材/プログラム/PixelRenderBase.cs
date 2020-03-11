using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelRenderBase : MonoBehaviour
{
    private Vector3 cashPos;

    private void LateUpdate()
    {
        cashPos = transform.localPosition;
        transform.localPosition = new Vector3(
            (int)cashPos.x,
            (int)cashPos.y,
            (int)cashPos.z);
    }
    private void OnRenderObject()
    {
        transform.localPosition = cashPos;
    }
}
