using System;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private List<Boid> neighbours;

    private Vector3 currentVelocity, desiredVelocity;

    public float speed;

    [SerializeField]private GameObject target;

    private void Start()
    {
        neighbours = new List<Boid>();
    }

    private void Update()
    {
        desiredVelocity += (target.transform.position - transform.position).normalized * speed;

        desiredVelocity += Alignment();
        desiredVelocity += Cohesion();
        desiredVelocity += Separation();

        Vector3 diff = desiredVelocity - currentVelocity;
        currentVelocity += diff * Time.deltaTime;

        currentVelocity = Vector3.ClampMagnitude(currentVelocity, speed);
        transform.position += currentVelocity * Time.deltaTime;
        transform.forward = currentVelocity;
    }

    private Vector3 Alignment()
    {
        if (neighbours.Count == 0)
            return Vector3.zero;

        Vector3 alignment = Vector3.zero;

        for (int i = 0; i < neighbours.Count; i++)
        {
            alignment += neighbours[i].currentVelocity;
        }

        alignment /= neighbours.Count;

        return alignment.normalized * speed;
    }

    private Vector3 Cohesion()
    {
        if (neighbours.Count == 0)
            return Vector3.zero;

        Vector3 center = Vector3.zero;

        for (int i = 0; i < neighbours.Count; i++)
        {
            center += neighbours[i].transform.position;
        }

        center /= neighbours.Count;

        return center.normalized * speed;
    }

    private Vector3 Separation()
    {
        if (neighbours.Count == 0)
            return Vector3.zero;

        Vector3 direction = Vector3.zero;
        Vector3 distance;

        for (int i = 0; i < neighbours.Count; i++)
        {
            distance = neighbours[i].transform.position - transform.position;
            direction += distance / distance.sqrMagnitude;
        }

        direction /= neighbours.Count;

        return -direction.normalized * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Boid boid = other.GetComponent<Boid>();

        if (boid != null)
        {
            neighbours.Add(boid);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Boid boid = other.GetComponent<Boid>();

        if (boid != null)
        {
            neighbours.Remove(boid);
        }

        // if (other.gameObject.tag == "Wasser")
        // {
        //     CalculateDirection();
        // }
    }
}