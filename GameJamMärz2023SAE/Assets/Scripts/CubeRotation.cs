using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float RotAngleY = 120;

    private void Update()
    {
        //Cards Rotation
        float rotationY = Mathf.SmoothStep(0, RotAngleY, Mathf.PingPong(Time.time * speed, 1));
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}