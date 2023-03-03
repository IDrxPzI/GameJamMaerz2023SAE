using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Transform thisTransform;

    [Range(1f, 10f)][SerializeField] private float sensitivity = 3f; //camera sensitivity

    private Vector3 angle;

    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        CameraRotation();
        HideMouseCursor();
    }
    /// <summary>
    /// camera movement
    /// </summary>
    private void CameraRotation()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            angle.y += Input.GetAxis("Mouse X") * 100 * sensitivity * Time.deltaTime; //Input for camera movement
            angle.x += Input.GetAxis("Mouse Y") * 100 * sensitivity * Time.deltaTime; //Input for camera movement
        }

        angle.x = Mathf.Clamp(angle.x, -42, 30); //clamps camera movement so player cant look through/at themself

        //thisTransform.eulerAngles = angle;

        thisTransform.parent.eulerAngles = new Vector3(0, angle.y, 0); //camera movement
        thisTransform.localEulerAngles = new Vector3(angle.x, 0, 0); //camera movement
    }
    /// <summary>
    /// hides cursor for better gameplay
    /// </summary>
    private void HideMouseCursor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
