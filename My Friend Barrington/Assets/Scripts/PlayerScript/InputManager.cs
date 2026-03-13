using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput)), DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    private bool jumpPressed = false;
    private bool interactPressed = false;
    public bool submitPressed = false;

    // Player
    // Input Action Map
    public InputActionReference jumpAction;
    public InputActionReference moveAction;
    public InputActionReference skipLevelAction;
    public InputActionReference interactAction;
    public InputActionReference videoSkipAction;
    public InputActionReference videoNextAction;
    public InputActionReference pauseGame;
    //public InputActionReference OninteractAction;
    public InputActionReference submitAction;
    public InputActionReference cancelAction;
    public InputActionReference nextAction;
    public InputActionReference navigateAction;

    public delegate void PlayerAction();
    public PlayerAction playerAction;

    private static InputManager instance;

    // cached PlayerInput component on this persistent object
    private PlayerInput playerInputComponent;

    // keep a reference to the controls-changed handler so we can unsubscribe
    private System.Action controlsChangedHandler;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Debug.LogError("Found more than one Input Manager in the scene");
            Destroy(gameObject);
            return;
        }

        // cache PlayerInput and set initial cursor state based on current control scheme
        playerInputComponent = GetComponent<PlayerInput>();
        if (playerInputComponent != null)
        {
            UpdateCursorAndControllerState(playerInputComponent.currentControlScheme);

            // create and store handler so we can remove it on destroy
            controlsChangedHandler = () =>
            {
                if (playerInputComponent != null)
                    UpdateCursorAndControllerState(playerInputComponent.currentControlScheme);
            };

            //playerInputComponent.onControlsChanged += controlsChangedHandler;
        }

        // listen for device changes (gamepad plugged/unplugged etc.) to re-evaluate
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDestroy()
    {
        if (playerInputComponent != null && controlsChangedHandler != null)
        {
            //playerInputComponent.onControlsChanged -= controlsChangedHandler;
            controlsChangedHandler = null;
        }

        InputSystem.onDeviceChange -= OnDeviceChange;

        if (instance == this) instance = null;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        // Re-evaluate control scheme / cursor when devices are added/removed/reconnected
        if (change == InputDeviceChange.Added ||
            change == InputDeviceChange.Reconnected ||
            change == InputDeviceChange.Removed ||
            change == InputDeviceChange.Disconnected)
        {
            if (playerInputComponent != null)
            {
                UpdateCursorAndControllerState(playerInputComponent.currentControlScheme);
            }
        }
    }

    private void UpdateCursorAndControllerState(string currentScheme)
    {
        // Treat exactly "Keyboard&Mouse" as mouse/keyboard usage (matches Player.OnControlsChanged)
        bool usingKeyboardMouse = currentScheme == "Keyboard&Mouse";
        bool usingController = !usingKeyboardMouse;

        // Hide cursor and lock when using controller; show and unlock for keyboard & mouse
        Cursor.visible = usingKeyboardMouse;
        Cursor.lockState = usingController ? CursorLockMode.Locked : CursorLockMode.None;

        // Update static flag used by UI selection logic
        FirstSelectedButtonController.isController = usingController;

        // Notify player(s) so they can update UI for control scheme
        var players = FindObjectsOfType<Player>();
        foreach (var p in players)
        {
            p.OnControlsChanged();
        }
    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public bool GetJumpPressed()
    {
        bool result = jumpPressed;
        jumpPressed = false;
        return result;
    }

    public bool GetInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false; // fixed: reset submitPressed (was resetting interactPressed erroneously)
        return result;
    }


    public void RegisterSubmitPressed()
    {
        submitPressed = false;
    }


}