using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    // This is the player script
    // Setting serializable field for editor to edit in inspector
    [Header("Movement")]
    [SerializeField]
    private float speedAcceleration;
    [SerializeField]
    private float speedMax;
    public float linearDrag;
    //public float gravity;
    public float jump, jumpCooldown;
    private bool isJump;
    private float jumpButtonHoldTimer;
    [SerializeField]
    private float maxJumpTimer;
    [SerializeField]
    private float jumpForceIncrease;
    public float fixedInAirVelocity;

    // Visual Speed Text
    //private float currentSpeed;
    //[SerializeField]
    //private TMP_Text speedText;

    // Chekcing Inputs, player movement
    public bool playerInput = true;
    private bool isWalking;
    public bool isRight;
    private Vector2 moveInput;

    // Swinging status - DV
    private Grapple swing = null;

    // Checking ground
    [Header("GroundCheck")]
    public bool isGround;
    public LayerMask groundMask;
    public LayerMask platformMask;
    public float playerHeight;

    // Getting components
    [Header("Components")]
    public Camera cam;
    [SerializeField]
    private Rigidbody rb;
    //[SerializeField]
    //private GameManager gm;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private DangerDetect dangerDectect;

    // Dialogue
    static public bool dialogue = false;

    // Gizmo
    private Color gizmoColour = Color.yellow;

    // Cool new trick
    private void OnEnable()
    {
        // add new input system
        InputManager.GetInstance().jumpAction.action.started += jumpStart;
        InputManager.GetInstance().jumpAction.action.canceled += jumpEnd;
        InputManager.GetInstance().moveAction.action.started += startPlayer;
        InputManager.GetInstance().moveAction.action.canceled += stopPlayer;
        // Add Delegate section
        InputManager.GetInstance().playerAction += fixSpeed;
        InputManager.GetInstance().playerAction += movePlayer;
        InputManager.GetInstance().playerAction += isGroundRayCast;
        InputManager.GetInstance().playerAction += jumpForce;
        InputManager.GetInstance().playerAction += playerAnimation;
        InputManager.GetInstance().playerAction += playerRotation;
        InputManager.GetInstance().playerAction += fixTheAir;
        // Enable Actions
        InputManager.GetInstance().jumpAction.action.Enable();
        InputManager.GetInstance().moveAction.action.Enable();
    }

    private void OnDisable()
    {
        // Remove new input system
        InputManager.GetInstance().jumpAction.action.started -= jumpStart;
        InputManager.GetInstance().jumpAction.action.canceled -= jumpEnd;
        InputManager.GetInstance().moveAction.action.started -= startPlayer;
        InputManager.GetInstance().moveAction.action.canceled -= stopPlayer;
        // Remove Delegate Section
        InputManager.GetInstance().playerAction -= fixSpeed;
        InputManager.GetInstance().playerAction -= movePlayer;
        InputManager.GetInstance().playerAction -= isGroundRayCast;
        InputManager.GetInstance().playerAction -= jumpForce;
        InputManager.GetInstance().playerAction -= playerAnimation;
        InputManager.GetInstance().playerAction -= playerRotation;
        InputManager.GetInstance().playerAction -= fixTheAir;
        // Disable Action
        InputManager.GetInstance().jumpAction.action.Disable();
        InputManager.GetInstance().moveAction.action.Disable();
    }
    // Setting up values at start
    void Start()
    {
        // Getting component from player
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //gm = gameObject.findGameManager(); i guess we are not using game manager for now?

        // Setting Up boolean
        rb.freezeRotation = true;
        playerInput = true;
        rb.linearDamping = linearDrag;
        // affect how fast player falls
        //Physics.gravity = new Vector3(0f, gravity, 0f);
    }
    
    private void FixedUpdate()
    {
        // well atleast merge move and speed line inside one? - invoke all the methods inside delegate
        InputManager.GetInstance().playerAction?.Invoke();

        // Player Movement
        //moveInput = InputManager.GetInstance().moveAction.action.ReadValue<Vector2>();
        //rb.linearVelocity = new Vector3(moveInput.x * speedAcceleration, rb.linearVelocity.y, rb.linearVelocity.z);
        //if (isJump)
        //{
        //    rb.linearVelocity = new Vector3(moveInput.x * speedAcceleration, jump, rb.linearVelocity.z);
        //}
    }

    // Player moving method
    private void movePlayer()
    {
        if (!playerInput || dialogue)
        {
            freezePlayer();
            return; // sadly no simple line like if(!playerInput) freezePlayer(); it must have return :O
        }

        // Getting values from user input
        moveInput = InputManager.GetInstance().moveAction.action.ReadValue<Vector2>();
        moveInput = new Vector2(moveInput.x,0); // THIS LINE SHOULD BE REVIEWED WHEN TESTING CONTROLLER MOVEMENT. IT WILL PROBABLY FUCK THINGS UP. - DV
        moveInput.Normalize();

        // Player cannot Input, end
        if (moveInput == Vector2.zero) return;

        if (swing != null) // use swinging movement instead - DV
        {
            swing.moveSwing(moveInput);
            return;
        }

        Vector3 playerMovement = new Vector3(moveInput.x * speedAcceleration, rb.linearVelocity.y, rb.linearVelocity.z);
        //Debug.Log(rb.linearVelocity.y + "-1");

        // Player Movement
        rb.linearVelocity = playerMovement;

        // Fianlly found something to fix when input is too small like controller and the face won't change :D
        float playerDir = Mathf.Sign(moveInput.x);

        // Rotate player based on input
        if (playerDir > 0)
        {
            isRight = true;
            if(isGround) isWalking = true; // only walk on the ground foo - DV
        }
        else if (playerDir < 0)
        {
            isRight = false;
            if(isGround) isWalking = true; // only walk on the ground foo - DV
        }
        // Drag player
        //currentSpeed = rb.linearVelocity.magnitude;
        //speedText.text = "Current Speed: " + currentSpeed;
    }
    private void startPlayer(InputAction.CallbackContext context)
    {
        //isWalking = true; // only walk on the ground foo - DV
    }
    private void stopPlayer(InputAction.CallbackContext context)
    {
        // reset player velocity
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        isWalking = false;
    }
    private void jumpStart(InputAction.CallbackContext context)
    {
        if (swing != null)
        {
            isGround = true;
            swing.DestroyHook();
        }
        // start pressing jump button
        jumping();
    }
    private void jumpEnd(InputAction.CallbackContext context)
    {
        // end pressing jump button
        isJump = false;
    }
    private void jumping()
    {
        // Jump
        // End code if not touching Ground
        if (!isGround) return;

        // play Audio
        AudioManager.instance.playPlayerSFX("PlatformJump");

        // Turn on Bools
        isJump = true;
        jumpButtonHoldTimer = 0;
        isWalking = false;

        // jump movement - being remove later
        rb.linearVelocity = new Vector3(rb.linearVelocity.x /* + moveInput.x*/, jump, rb.linearVelocity.z);

    }
    private void jumpForce()
    {
        // don't run the code if jump is not pressed
        if (!isJump) return;

        // timer for how long player pressed the button
        jumpButtonHoldTimer += Time.deltaTime;

        // apply force based on how long player pressed
        if (jumpButtonHoldTimer < maxJumpTimer)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x /* + moveInput.x*/, rb.linearVelocity.y + jumpForceIncrease, rb.linearVelocity.z);
        }
        //Debug.Log(rb.linearVelocity.y + "-2");
    }
    private void fixSpeed()
    {
        // Check for speed (wihtout jumping)
        Vector3 getLinearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        // If exceed the speed
        if (getLinearVelocity.magnitude > speedMax)
        {
            Vector3 limitLinearVelocity = getLinearVelocity.normalized * speedMax;
            //rb.linearVelocity = new Vector3(limitLinearVelocity.x, rb.linearVelocity.y, limitLinearVelocity.z);
        }
    }
    private void playerAnimation()
    {
        // Player walking Audio
        AudioManager.instance.playPlayerWalking(isWalking);
        anim.SetBool("PlayerWalk", isWalking);
    }
    private void isGroundRayCast()
    {
        // Ground Check
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);
    }
    // aplly froce in air
    private void fixTheAir()
    {
        if (!isGround)
        {
            rb.AddForce(Vector3.down * fixedInAirVelocity, ForceMode.VelocityChange);
        }
    }
    public void freezePlayer()
    {
        // Stop player from moving
        rb.linearVelocity = Vector3.zero;
        isWalking = false;
        AudioManager.instance.playPlayerWalking(isWalking);
        anim.SetBool("PlayerWalk", isWalking);
    }
    public void playerRotation()
    {
        if (isRight)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            dangerDectect.direction = true;
        }
        else if (!isRight)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            dangerDectect.direction = false;
        }
    }
    public void pushingPlayer(Vector3 dir, float force)
    {
        // Add force based on dirction and force given
        rb.AddForce(dir * force, ForceMode.Impulse);
    }

    // Bubble stream floating method
    public void inBubbleStream(float floatForce)
    {
        // Add force in bubble stream
        rb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
    }
    // Gizmo to show ground check raycast
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColour;
        Gizmos.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f));
    }

    public void Swing(Grapple swinging) // is this good practice? idk man im trying - DV
    {
        swing = swinging;
    }
}
