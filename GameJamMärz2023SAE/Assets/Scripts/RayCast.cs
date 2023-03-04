using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows.Speech;

public class RayCast : MonoBehaviour
{
    [SerializeField] private LayerMask CollectablesLayerMask;
    [SerializeField] private GameObject canvas;

    private Vector3 forward;

    [SerializeField] private Transform parent;

    [SerializeField] private GameObject[] KeyCards;
    private bool rayCast;

    bool hitonce = false;

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

    private bool gotACard;


    void Update()
    {
        forward = transform.TransformDirection(Vector3.forward) * 2;
        Debug.DrawRay(transform.position, forward, Color.green);


        rayCast = Physics.Raycast(transform.position, forward, out hit, 2f, CollectablesLayerMask);

        if (rayCast)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }


        if (rayCast && Input.GetKeyDown(KeyCode.E))
        {
            if (!executeOnce)
            {
                executeOnce = true;
                gotACard = CollectCards();
            }

            executeOnce = false;
            if (gotACard)
            {
                InputCards();
            }
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

                if (Input.GetKeyDown(KeyCode.E) && hitonce /*&& has one key */)
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