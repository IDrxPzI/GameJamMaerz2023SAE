using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    [SerializeField] private GameObject InteractPanel;

    private void Update()
    {
        Raycast();
    }
    public void Raycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //Debug.DrawRay(transform.position, Vector3.forward, Color.red, 8);

        bool hitSomething = Physics.Raycast(ray, out hit, 4);
        Debug.Log(hitSomething);

        if (hitSomething && hit.transform.CompareTag("Collectables"))
        {
            InteractPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }
        else
        {
            InteractPanel.SetActive(false);
        }


    }
}
