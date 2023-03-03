using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float speed;

    private float frequency;

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
       //transform.position = MathF.Sin();
    }
}