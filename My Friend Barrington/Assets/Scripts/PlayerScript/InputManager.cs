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
    private bool submitPressed = false;

    // Player
    // Input Action Map
    public InputActionReference jumpAction;
    public InputActionReference moveAction;
    public InputActionReference skipLevelAction;
    public InputActionReference interactAction;

    public delegate void PlayerAction();
    public PlayerAction playerAction;

    private static InputManager instance;

    private void Awake()
    {
       if(instance == null)
       {
            instance = this;
            DontDestroyOnLoad(gameObject);
       }
        else
        {
            //Debug.LogError("Found more than one Input Manager in the scene");
            Destroy(gameObject);
        }
    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    //public void MovePressed(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        moveDirection = context.ReadValue<Vector2>();

    //    }
    //    else if (context.canceled)
    //    {
    //        moveDirection = context.ReadValue<Vector2>();
    //    }
    //}

    //public void JumpPressed(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        jumpPressed = true;
    //    }
    //    else if (context.canceled)
    //    {
    //        jumpPressed = false;
    //    }
    //}

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("OnInteract called! Phase: " + context.phase); // Add this line
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
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
        interactPressed = false;
        return result;
    }

    
    public void RegisterSubmitPressed()
    {
        submitPressed = false;
    }
}
