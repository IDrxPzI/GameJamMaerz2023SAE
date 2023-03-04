using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float freq;
    [SerializeField] private float amp;
    private float frequency;


    public float speed = 1;
    public float RotAngleY = 120;

    private void Update()
    {
        float yMovement = Mathf.Sin(Time.time * freq) * amp;
        transform.Translate(0, yMovement * Time.deltaTime, 0);
        ///Core Rotation
        //transform.Rotate(0, 1f * rotateSpeed, 0);

        ///Cards Rotation
        float rY = Mathf.SmoothStep(0, RotAngleY, Mathf.PingPong(Time.time * speed, 1));
        transform.rotation = Quaternion.Euler(0, rY, 0);
    }
}