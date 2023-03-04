using System;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private List<Boid> neighbours;

    private Vector3 currentVelocity, desiredVelocity;

    public float speed;

    public float radius;
    public float alpha;

    public LayerMask layerMask;

    private Vector3 midPosition;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, alpha);
        Gizmos.DrawSphere(
            new Vector3(transform.position.x - 0.5f, transform.position.y + 0.40f, transform.position.z - 0.45f),
            radius);
    }

    public Vector3 SteerTowards(Vector3 _vector)
    {
        Vector3 v = _vector.normalized * speed - desiredVelocity;
        return Vector3.ClampMagnitude(v, 5);
    }
    bool CheckForHit()
    {
        // Vector3 fwd = transform.TransformDirection(Vector3.forward);
        // Debug.DrawRay(new Vector3(transform.position.x - 0.5f, transform.position.y + 0.40f, transform.position.z),
        //     fwd * 10, Color.green);
        // RaycastHit objectHit;
        midPosition = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.40f,
            transform.position.z - 0.45f);
        RaycastHit hit;
        if (Physics.SphereCast(midPosition, radius, transform.forward, out hit, 5f, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        neighbours = new List<Boid>();
    }

    private void Update()
    {
        CalculateDirection();
        CheckForHit();
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

        if (other.gameObject.tag == "Wasser")
        {
            CalculateDirection();
        }
    }

    private void CalculateDirection()
    {
        if (CheckForHit())
        {
            
        }
        
        desiredVelocity += transform.forward * speed;

        desiredVelocity += Alignment();
        desiredVelocity += Cohesion();
        desiredVelocity += Separation();

        Vector3 diff = desiredVelocity - currentVelocity;
        currentVelocity += diff * Time.deltaTime;

        currentVelocity = Vector3.ClampMagnitude(currentVelocity, speed);
        transform.position += currentVelocity * Time.deltaTime;
        transform.forward = currentVelocity;
    }
}