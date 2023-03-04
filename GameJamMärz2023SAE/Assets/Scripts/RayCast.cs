using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class RayCast : MonoBehaviour
{
    [SerializeField] private LayerMask CollectablesLayerMask;
    [SerializeField] private LayerMask CollectorLayerMask;
    [SerializeField] private GameObject canvas;

    private Vector3 forward;

    [SerializeField] private Transform parent;

    [SerializeField] private GameObject[] KeyCards;

    private bool rayCast;
    private bool rayCastCollector;
    private bool gotACard;
    private bool hitonce = false;
    private bool executeOnce = false;

    private void Start()
    {
        KeyCards = new GameObject[4];
        for (int i = 0; i < KeyCards.Length; i++)
        {
            KeyCards[i] = parent.GetChild(i).gameObject;
        }
    }


    void Update()
    {
        forward = transform.TransformDirection(Vector3.forward) * 2;
        Debug.DrawRay(transform.position, forward, Color.green);

        RaycastHit hit;
        rayCast = Physics.Raycast(transform.position, forward, out hit, 2f, CollectablesLayerMask);
        rayCastCollector = Physics.Raycast(transform.position, forward, 2f, CollectorLayerMask);

        if (rayCast || rayCastCollector)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }


        if (!executeOnce)
        {
            if (rayCast && Input.GetKeyDown(KeyCode.E))
            {
                executeOnce = true;
                gotACard = CollectCards();
                Destroy(hit.transform.gameObject);
            }

            executeOnce = false;
        }

        if (gotACard && Input.GetKeyDown(KeyCode.E) && rayCastCollector)
        {
            InputCards();
        }
    }

    bool CollectCards()
    {
        for (int i = 0; i < KeyCards.Length; i++)
        {
            if (!hitonce)
            {
                hitonce = true;
                Debug.Log("collected a card");
                return true;
            }
        }

        return false;
    }

    void InputCards()
    {
        int amount = 4;
        for (int i = 0; i < KeyCards.Length; i++)
        {
            for (int j = 0; j < amount; j++)
            {
                if (KeyCards[i].activeSelf)
                {
                    break;
                }

                if (hitonce)
                {
                    KeyCards[i].SetActive(true);
                    hitonce = false;
                    Debug.Log("successfully input a card");
                }
            }
        }

        gotACard = false;
    }
}