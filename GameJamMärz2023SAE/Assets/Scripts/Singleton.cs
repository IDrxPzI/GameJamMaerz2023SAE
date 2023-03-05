using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static int keysCollected { get; set; }

    public GameObject parent;
    public GameObject canvas;

    private static Singleton instance = null;

//
    public static Singleton Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Singleton>();

            if (instance == null)
            {
                GameObject gameobject = new GameObject();
                gameobject.name = "Singleton";
                instance = gameobject.AddComponent<Singleton>();
                DontDestroyOnLoad(gameobject);
            }

            return instance;
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Debug.Log(keysCollected);
    }
}