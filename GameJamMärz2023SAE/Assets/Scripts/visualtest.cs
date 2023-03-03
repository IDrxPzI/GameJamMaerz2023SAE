using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class visualtest : MonoBehaviour
{
    public Color color;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    [Range(0, 1)] public float alpha;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(color.r,color.g,color.b, alpha);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}