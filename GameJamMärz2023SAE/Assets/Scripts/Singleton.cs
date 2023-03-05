using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
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

    public static int keysCollected { get; set; }


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