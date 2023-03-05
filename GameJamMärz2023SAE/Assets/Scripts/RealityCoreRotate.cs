using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityCoreRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10;
    [SerializeField] private float freq;
    [SerializeField] private float amp;

    private float newSpeed = 10;

    void Update()
    {
        //var yMovement = Mathf.Sin(Time.time * freq) * amp;
        //transform.Translate(0, yMovement * Time.deltaTime, 0);
        for (int i = 0; i < Singleton.keysCollected; i++)
        {
            rotateSpeed = newSpeed - 2;
        }

        transform.Rotate(0, 1f * rotateSpeed, 0);
    }
}