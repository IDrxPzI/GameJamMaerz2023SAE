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

    RaycastHit hit;

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


        rayCast = Physics.Raycast(transform.position, forward, out hit, 2f, CollectablesLayerMask);
        rayCastCollector = Physics.Raycast(transform.position, forward, out hit, 2f, CollectorLayerMask);

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
                //Destroy(hit.transform.gameObject);
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
                //KeyCards[i] = parent.GetChild(i).gameObject;
                hitonce = true;
                // Destroy(hit.transform.gameObject);
                Debug.Log("collected a card");
               // Destroy(hit.transform.gameObject);
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

                if (hitonce /*&& has one key */)
                {
                    KeyCards[i].SetActive(true);
                    hitonce = false;
                    Debug.Log("successfully input a card");

                    //Destroy(hit.transform.gameObject);
                }
            }
        }

        gotACard = false;
    }
}