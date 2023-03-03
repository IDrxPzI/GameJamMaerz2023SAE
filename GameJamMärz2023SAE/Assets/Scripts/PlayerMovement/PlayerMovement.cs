using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private GameSettings gamesettings;

    private CharacterController playerController;

    private float runSpeed;
    private float runTime = 2f;

    private Vector2 move;
    private Vector2 look;

    private Vector3 playerVelocity;
    //private Vector3 startPosition = new Vector3(0, 1, -11.6f);

    private MouseLook[] mouselook;

    private bool inWater;
    private bool jumped;
    private bool running;
    [SerializeField] private bool isGrounded;
    public static bool triggered { get; private set; }
    public static bool openMenu { get; private set; }
    public static int getInput { get; private set; }

    private void Awake()
    {
        //transform.position = startPosition;

        playerController = GetComponent<CharacterController>();
        mouselook = GetComponentsInChildren<MouseLook>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    #region InputActions

    /// <summary>
    /// enables interaction with buttons{keycode.E} _ {ButtonWest/Gamepad}
    /// </summary>
    /// <param name="context"></param>
    public void OnInteract(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                triggered = true;
                getInput = 1;
                break;
            case InputActionPhase.Canceled:
                triggered = false;
                getInput = 0;
                break;
        }
    }

    /// <summary>
    /// enables movement W,A,S,D _ leftstick(Gamepad)
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// enables cameramovement mouse _ rightstick
    /// </summary>
    /// <param name="context"></param>
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// enables running, because dashing doesnt work 
    /// </summary>
    /// <param name="context"></param>
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
            running = true;
        else if (context.canceled)
            running = false;
    }

    /// <summary>
    /// enables jumping, spacebar _ ButtonSouth(Gamepad)
    /// </summary>
    /// <param name="context"></param>
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            jumped = true;
        else if (context.canceled)
            jumped = false;
    }

    /// <summary>
    /// enables the Menu with, Escape _ startButton(Gamepad)
    /// </summary>
    /// <param name="context"></param>
    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        if (context.started)
            openMenu = true;
        else if (context.canceled)
            openMenu = false;
    }

    #endregion

    private void Move(Vector2 direction)
    {
        isGrounded = playerController.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        if (inWater)
        {
            if (jumped)
            {
                playerVelocity.y += Mathf.Sqrt(gamesettings.jumpHeight * -3.0f * gamesettings.gravityValue);
            }

            jumped = false;
        }


        Vector3 _move = new Vector3(direction.x, 0, direction.y);
        playerController.Move(transform.TransformDirection(_move) * Time.deltaTime * playerSpeed);

        if (jumped && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(gamesettings.jumpHeight * -3.0f * gamesettings.gravityValue);
        }

        //forces player on the ground
        playerVelocity.y += gamesettings.gravityValue * Time.deltaTime;
        playerController.Move(playerVelocity * Time.deltaTime);

        if (running)
        {
            float startTime = Time.time;

            //lets the player run
            if (Time.time < startTime + runTime)
            {
                playerController.Move(transform.TransformDirection(_move.x, 0, _move.z) * Time.deltaTime *
                                      gamesettings.runSpeed);
            }
        }
    }


    private void Update()
    {
        Move(move);

        foreach (var mLook in mouselook)
        {
            mLook.Look(look);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Moon")
        {
            playerSpeed = 10;
            gamesettings.gravityValue = -3;
            gamesettings.jumpHeight = 1.5f;
        }

        if (other.gameObject.tag == "Wasser")
        {
            playerSpeed = 5;
            inWater = true;
            gamesettings.gravityValue = -6;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inWater = false;
        playerSpeed = 10;
        gamesettings.gravityValue = -12f;
        gamesettings.jumpHeight = 0.7f;
    }
}