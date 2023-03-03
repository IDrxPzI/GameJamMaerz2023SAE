using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New GameSettings", menuName = "Tools/Game Settings", order = 0)]
public class GameSettings : ScriptableObject
{
    [Header("Player Movement")] public float moveSpeed = 2.0f;
    public float runSpeed = 3.0f;
    public float jumpHeight = 0.2f;
    public float gravityValue = -9.81f;
    public float dashSpeed = 10;

    [Header("Mouse Look")] [Range(0f, .2f)]
    public float mouseSensitivityX = .1f;

    [Range(0f, .2f)] public float mouseSensitivityY = .1f;

    public float minimumX = -360f;
    public float maximumX = 360f;

    public float minimumY = -60f;
    public float maximumY = 60f;

    // [Header("Sliders")]
    // [SerializeField] private Slider mouseSensitivity;
}