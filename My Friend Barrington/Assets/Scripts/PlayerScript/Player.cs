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
    public float groundDrag;
    public float jump, jumpCooldown;
    private bool isJump;
    private float jumpButtonHoldTimer;
    [SerializeField]
    private float maxJumpTimer;
    [SerializeField]
    private float jumpForceIncrease;
    //[SerializeField]
    //private float maxJumpingForce;
    public float inAirBoost;
    private float currentSpeed;
    [SerializeField]
    private TMP_Text speedText;
    //public bool isPushed;
    //private int pushedDirection;
    //private float pushedSpeed;

    // Chekcing Inputs, player movement
    private float verticalInput;
    private float horizontalInput;
    public bool playerInput = true;
    private bool isWalking;

    // Checking ground
    [Header("GroundCheck")]
    public bool isGround;
    public LayerMask groundMask;
    public LayerMask platformMask;
    public float playerHeight;
    public bool moveRespawn;

    /*
    // Invincibility
    [Header("Invincible")]
    public bool isInvincible;
    [SerializeField]
    private float invinicbleDuration;
    private float invincibleTimer;
    */

    // Getting components
    [Header("Components")]
    public Camera cam;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private DangerDetect dangerDectect;

    // Input Action Map
    public InputActionReference jumpAction;
    public InputActionReference moveAction;

    // Delegate
    public delegate void PlayerAction();
    private PlayerAction playerAction;

    // Dialogue
    static public bool dialogue = false;

    // Gizmo
    private Color gizmoColour = Color.yellow;

    // Cool new trick
    private void OnEnable()
    {
        //jumpAction.action.performed += jumping;
        jumpAction.action.started += jumpStart;
        jumpAction.action.canceled += jumpEnd;
        moveAction.action.canceled += stopPlayer;
        playerAction += fixSpeed;
        playerAction += movePlayer;
        playerAction += isGroundRayCast;
        playerAction += jumpForce;
        jumpAction.action.Enable();
        moveAction.action.Enable();
        //moveAction.action.performed += movePlayer;
    }

    private void OnDisable()
    {
        //jumpAction.action.performed -= jumping;
        jumpAction.action.started -= jumpStart;
        moveAction.action.canceled -= jumpEnd;
        moveAction.action.canceled -= stopPlayer;
        playerAction -= fixSpeed;
        playerAction -= movePlayer;
        playerAction -= isGroundRayCast;
        playerAction -= jumpForce;
        jumpAction.action.Disable();
        moveAction.action.Disable();
        //moveAction.action.performed -= movePlayer;
    }
    // Setting up values at start
    void Start()
    {
        // Getting component from player
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gm = gameObject.findGameManager();

        // Setting Up boolean
        rb.freezeRotation = true;
        //isJump = true;
        //isInvincible = false;
        playerInput = true;
        rb.linearDamping = groundDrag;
    }
    private void Update()
    {

        // Calling methods
        //if (playerInput)
        //{
        //    jumping();
        //}
        //Debug.Log(isWalking);
        //Debug.Log(isGround);
        //Debug.Log(isJump);
    }
    private void FixedUpdate()
    {
        // Calling methods
        //if (playerInput)
        //{
        //    movePlayer();
        //    fixSpeed();
        //}
        //else
        //{
        //    freezePlayer();
        //}

        //movePlayer();
        //fixSpeed();
        // well atleast merge move and speed line inside one?
        playerAction?.Invoke();

        // Check invincible
        /*
        if (isInvincible)
        {
            gm.isInvincible = true;
            invincibleTimer += Time.deltaTime;
            if (invincibleTimer > invinicbleDuration)
            {
                gm.isInvincible = false;
            }
        }
        */
        if (!dialogue && playerInput)
        {
            //movePlayer();
        }
        //Debug.Log(playerInput);
        //Debug.Log(jumpAction.action.triggered);
    }
    // Update is called once per frame
    //void Update()
    //{

    //}
    // Player moving method
    private void movePlayer()
    {
        if (!playerInput)
        {
            freezePlayer();
            return; // sadly no simple line like if(!playerInput) freezePlayer(); it must have return :O
        }
        if (dialogue)
        {
            freezePlayer();
            return; // sadly no simple line like if(!playerInput) freezePlayer(); it must have return :O
        }
        // Getting player pressing which button (w,a,s,d)
        //verticalInput = Input.GetAxisRaw("Vertical"); // do not use .GetAxis, unlimited speed
        //horizontalInput = Input.GetAxisRaw("Horizontal");

        // Getting values from user input
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        Vector3 playerMovement = new Vector3(moveInput.x * speedAcceleration, rb.linearVelocity.y, rb.linearVelocity.z);
        //Debug.Log(moveInput.x);

        // Player cannot Input, end
        if (moveInput == Vector2.zero) return;

        // Player Movement
        rb.linearVelocity = playerMovement;
        //if (isJump)
        //{
        //    isWalking = true;
        //}

        // Move player according to input direction and speed
        //if (horizontalInput != 0/* && !isPushed*/)
        //{
        //    rb.AddForce(new Vector3(horizontalInput, 0, /*verticalInput*/0) * speedAcceleration);
        //    // Play Foot Steps here
        //}
        //else if (horizontalInput != 0 && isPushed)
        //{
        //    //pushingPlayer();
        //}
        //else if (horizontalInput == 0 && isPushed) // this may break if we ever put in controller inputs due to drift - DV
        //{
        //    //pushingPlayer();
        //}

        // Fianlly found something to fix when input is too small like controller and the face won't change :D
        float playerDir = Mathf.Sign(moveInput.x);

        if (playerDir > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            dangerDectect.direction = true;
            //dangerDectect.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (playerDir < 0)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            dangerDectect.direction = false;
            //dangerDectect.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        // Drag player

        //if (isPushed)
        //{
        //    Invoke("pushedCooldown", 0.1f);
        //}
        // Player speed text field
        currentSpeed = rb.linearVelocity.magnitude;
        speedText.text = "Current Speed: " + currentSpeed;

        //isGroundRayCast();

        // Player walking Audio
        AudioManager.instance.playPlayerWalking(isWalking);
        anim.SetBool("PlayerWalk", isWalking);

    }

    private void stopPlayer(InputAction.CallbackContext context)
    {
        rb.linearVelocity = Vector3.zero;
        //if (isGround && isPushed)
        //{
        //    Debug.Log("this is true");
        //    rb.linearVelocity = Vector3.zero;
        //    // Play Foot Foot Steps here

        //}
        //else if (!isGround)
        //{
        //    //rb.linearDamping = 0;
        //    //rb.AddForce(new Vector3(horizontalInput, 0, /*verticalInput*/0) * speedAcceleration * inAirBoost);
        //    //rb.linearVelocity = playerMovement * inAirBoost;
        //}
    }
    private void jumpStart(InputAction.CallbackContext context)
    {
        jumping();
    }

    private void jumpEnd(InputAction.CallbackContext context)
    {
        isJump = false;
    }

    private void jumping()
    {
        // Jump
        //if (/*Input.GetKey(KeyCode.Space)*/jumpAction.action.triggered/* || Input.GetKey(KeyCode.W)*/)
        //{
        //}
        if (!isGround) return;
        //{
        //    Debug.Log("stop jumping plz");
        //    return;
        //}

        isJump = true;

        jumpButtonHoldTimer = 0;

        //float jumpInput = jumpAction.action.ReadValue<float>();
        // Play Jumping Sound
        //Debug.Log(jumpInput);
        AudioManager.instance.playPlayerSFX("PlatformJump");
        isWalking = false;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jump, rb.linearVelocity.z);
        //rb.AddForce(transform.up * jump, ForceMode.Impulse);

        //isGround = false;
        //isJump = false;

        //Invoke(nameof(jumpStatus), jumpCooldown); // cannot hold space now :<

    }

    private void jumpForce()
    {
        if (!isJump) return;

        jumpButtonHoldTimer += Time.deltaTime;

        if (jumpButtonHoldTimer < maxJumpTimer)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y + jumpForceIncrease, rb.linearVelocity.z);
        }
    }

    //private void jumpStatus()
    //{
    //    isJump = true;
    //}

    private void fixSpeed()
    {
        // Check for speed (wihtout jumping)
        Vector3 getLinearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        // If exceed the speed
        if (getLinearVelocity.magnitude > speedMax)
        {
            Vector3 limitLinearVelocity = getLinearVelocity.normalized * speedMax;
            rb.linearVelocity = new Vector3(limitLinearVelocity.x, rb.linearVelocity.y, limitLinearVelocity.z);
        }
    }
    /*
    public void isPushedDirection(int direction, float force)
    {
        pushedDirection = direction;
        pushedSpeed = force;
    }
    */

    //private void pushedCooldown()
    //{
    //    isPushed = false;
    //}

    private void isGroundRayCast()
    {
        
        // Ground Check
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

        if (isGround)
        {
            moveRespawn = false;
        }
        else
        {
            moveRespawn = true;
        }
    }

    public void pushingPlayer(Vector3 dir, float force)
    {
        /*
        if (pushedDirection == 0)
        {
            rb.AddForce(Vector3.right * pushedSpeed, ForceMode.Impulse);
            
        }
        else if (pushedDirection == 1)
        {
            rb.AddForce(Vector3.left * pushedSpeed, ForceMode.Impulse);
            //Invoke("pushedCooldown", 1f);
        }
        else if (pushedDirection == 2)
        {
            rb.AddForce(Vector3.up * pushedSpeed, ForceMode.Impulse);
            //Invoke("pushedCooldown", 1f);
        }
        else if (pushedDirection == 3)
        {
            rb.AddForce(Vector3.down * pushedSpeed, ForceMode.Impulse);
            //Invoke("pushedCooldown", 1f);
        }
        */
        rb.AddForce(dir * force, ForceMode.Impulse);
        //Invoke("pushedCooldown", 1f);
    }

    // Bubble stream floating method
    public void inBubbleStream(float floatForce)
    {
        rb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
    }

    public void freezePlayer()
    {
        rb.linearVelocity = Vector3.zero;
        isWalking = false;
        AudioManager.instance.playPlayerWalking(isWalking);
        anim.SetBool("PlayerWalk", isWalking);
    }


    // Gizmo to show ground check raycast
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColour;
        Gizmos.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f));
    }
}
