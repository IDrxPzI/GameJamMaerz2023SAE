using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource Music;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
