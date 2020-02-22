﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixenRenderBase : MonoBehaviour
{
    private Vector3 cashPos;

    private void Update()
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
