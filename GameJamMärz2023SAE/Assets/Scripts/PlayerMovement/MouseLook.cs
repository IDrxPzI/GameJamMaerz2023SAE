using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    [SerializeField] private GameSettings gamesettings;
    [SerializeField] private RotationAxes axes = RotationAxes.MouseXAndY;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private Quaternion originalRotation;

    public void Start()
    {
        originalRotation = transform.localRotation;
    }

    public void Look(Vector2 rotate)
    {
        switch (axes)
        {
            case RotationAxes.MouseXAndY:
            {
                rotationX += rotate.x * gamesettings.mouseSensitivityX;
                rotationY += rotate.y * gamesettings.mouseSensitivityY;
                rotationX = ClampAngle(rotationX, gamesettings.minimumX, gamesettings.maximumX);
                rotationY = ClampAngle(rotationY, gamesettings.minimumY, gamesettings.maximumY);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);

                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
                break;
            }
            case RotationAxes.MouseX:
            {
                rotationX += rotate.x * gamesettings.mouseSensitivityX;
                rotationX = ClampAngle(rotationX, gamesettings.minimumX, gamesettings.maximumX);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
                break;
            }
            default:
            {
                rotationY += rotate.y * gamesettings.mouseSensitivityY;
                rotationY = ClampAngle(rotationY, gamesettings.minimumY, gamesettings.maximumY);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
                transform.localRotation = originalRotation * yQuaternion;
                break;
            }
        }
    }


    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}