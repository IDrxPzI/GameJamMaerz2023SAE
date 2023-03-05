using System.Collections;
using TMPro;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class RayCast : MonoBehaviour
{
    [Header("LayerMasks")] [SerializeField]
    private LayerMask CollectablesLayerMask;

    [SerializeField] private LayerMask CollectorLayerMask;

    [Header("Canvas")] [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject pickUpPanel;
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private TMP_Text pickUpText;

    [Header("Animations")] [SerializeField]
    private Animation animation;

    [SerializeField] private AnimationClip AnimationClip;

    [Header("Gameobjects")] [SerializeField]
    private Transform parent;

    private GameObject[] KeyCards;

    private Vector3 forward;

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

        if (parent == null)
        {
            Transform go = GameObject.FindWithTag("Parent").transform;
            //parent = getc
        }

        DontDestroyOnLoad(canvas);
    }


    void Update()
    {
        forward = transform.TransformDirection(Vector3.forward) * 5f;
        Debug.DrawRay(transform.position, forward, Color.green);

        RaycastHit hit;
        rayCast = Physics.Raycast(transform.position, forward, out hit, 5f, CollectablesLayerMask);
        rayCastCollector = Physics.Raycast(transform.position, forward, 5f, CollectorLayerMask);

        if (rayCast || rayCastCollector)
        {
            panel.SetActive(true);
           
        }
        else
        {
            tmpText.SetText("Interact (E)");
            panel.SetActive(false);

        }

        if (!executeOnce)
        {
            if (gotACard && rayCast && Input.GetKeyDown(KeyCode.E))
            {
                tmpText.SetText("You already got a KeyCard");
                return;
            }

            if (rayCast && Input.GetKeyDown(KeyCode.E) && !gotACard)
            {
                StartCoroutine(PickedUpKeyCardCanvas());
                executeOnce = true;
                gotACard = CollectCards();
                Destroy(hit.transform.gameObject);
            }

            executeOnce = false;
        }

        if (!gotACard && Input.GetKeyDown(KeyCode.E) && rayCastCollector)
        {
            tmpText.SetText("You dont have a KeyCard yet");
            return;
        }

        if (Singleton.keysCollected == 4 && Input.GetKeyDown(KeyCode.E) && rayCastCollector)
        {
            Debug.Log("you just finished ze gem");
            tmpText.SetText("you just finished ze gem");
            EndGame();
        }

        if (gotACard && Input.GetKeyDown(KeyCode.E) && rayCastCollector)
        {
            InputCards();
        }
    }

    private void EndGame()
    {
        Debug.Log("you just finished ze gem");
        tmpText.SetText("you just finished ze gem");
    }

    IEnumerator PickedUpKeyCardCanvas()
    {
        pickUpText.SetText("You just got a KeyCard");
        pickUpPanel.SetActive(true);

        yield return new WaitForSeconds(3);
        pickUpPanel.SetActive(false);
    }

    bool CollectCards()
    {
        for (int i = 0; i < KeyCards.Length; i++)
        {
            if (!hitonce)
            {
                hitonce = true;
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
            //muss drin bleiben sonst geht kaputt
            for (int j = 0; j < amount; j++)
            {
                DontDestroyOnLoad(gameObject);
                if (KeyCards[i].activeSelf)
                {
                    break;
                }

                if (hitonce)
                {
                    tmpText.SetText("Successfully added a KeyCard");
                    animation.Play("AI_Stabalizer");
                    hitonce = false;
                    KeyCards[i].SetActive(true);
                    SceneManager.LoadScene(1);
                }
            }
        }

        Singleton.keysCollected++;
        gotACard = false;
    }
}