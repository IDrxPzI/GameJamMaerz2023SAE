using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float freq;
    [SerializeField] private float amp;
    private float frequency;

    private void Update()
    {
        float yMovement = Mathf.Sin(Time.time * freq) * amp;
        transform.Translate(0, yMovement * Time.deltaTime, 0);

        transform.Rotate(0, 1f * rotateSpeed, 0);
    }
}