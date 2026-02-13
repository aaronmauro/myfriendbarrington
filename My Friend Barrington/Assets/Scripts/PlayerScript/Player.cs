using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using FMODUnity;
using FMOD.Studio;

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
    [SerializeField]
    private float coyoteTime;
    private float coyoteTimeCounter;
    [SerializeField]
    private float idleTime;
    private float idleTimer;
    private bool isIdle;
    private bool isIdleAnimation;
    private bool isfalling;
    private float facingCameraTimer;
    
    // Visual frame flip
    [SerializeField] private Transform visual;
    [SerializeField] private float turnSmoothTime = 0.12f; 
    private float rootTurnVel; // used by SmoothDampAngle
    private float visualYVel = 0f;
    private float targetVisualY = 0f;

    // Visual Speed Text
    //private float currentSpeed;
    //[SerializeField]
    //private TMP_Text speedText;

    // Chekcing Inputs, player movement
    public bool playerInput = true;
    private bool isWalking;
    public bool isRight;
    public bool isPushingBox;
    private Vector2 moveInput;

    // Swinging status - DV
    private Grapple swing = null;

    // Checking ground
    [Header("GroundCheck")]
    public bool isGround;
    public LayerMask groundMask;
    //public LayerMask platformMask;
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

    // FMOD audio for walking
    [Header("Audio (FMOD)")]
    [SerializeField]
    private FMODUnity.EventReference playerWalkEvent; // use EventReference instead of string
    [SerializeField]
    private FMODUnity.EventReference playerJumpEvent; // add jump event reference
    private EventInstance walkEventInstance;
    private bool walkAudioPlaying = false;
    private bool fmodInitialized = false;

    // Dialogue
    static public bool dialogue = false;

    // Gizmo
    private Color gizmoColour = Color.yellow;

    // Platform
    private NewMonoBehaviourScript currentPlatform;
    private Vector3 lastPlatformPosition;

    public bool isInteracting;

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
        InputManager.GetInstance().playerAction += playerIdle;
        InputManager.GetInstance().playerAction += playerFalling;
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
        InputManager.GetInstance().playerAction -= playerIdle;
        InputManager.GetInstance().playerAction -= playerFalling;
        // Disable Action
        InputManager.GetInstance().jumpAction.action.Disable();
        InputManager.GetInstance().moveAction.action.Disable();

        // Stop & release FMOD instance
        if (fmodInitialized)
        {
            walkEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            walkEventInstance.release();
            fmodInitialized = false;
            walkAudioPlaying = false;
        }
    }

    // Setting up values at start
    void Start()
    {
        // Getting component from player
        rb = GetComponent<Rigidbody>();
        anim = GameObject.Find("barrington").GetComponent<Animator>();
        //gm = gameObject.findGameManager(); i guess we are not using game manager for now?

        // Setting Up boolean
        rb.freezeRotation = true;
        playerInput = true;
        //isIdle = true;
        rb.linearDamping = linearDrag;
        coyoteTimeCounter = 0f;
        
        // Initialize FMOD walk event instance from EventReference
        try
        {
            walkEventInstance = RuntimeManager.CreateInstance(playerWalkEvent);
            RuntimeManager.AttachInstanceToGameObject(walkEventInstance, transform, rb);
            fmodInitialized = true;
        }
        catch
        {
            Debug.LogWarning("FMOD: Failed to create/attach walk event instance. Check EventReference in inspector.");
            fmodInitialized = false;
        }
    }

    private void FixedUpdate()
    {
        // Invoke all normal player actions
        if (!playerInput || dialogue)
        {
            //freezePlayer(true); // this breaks the player death. not sure why its here, talk to me if its loadbearing - DV
            return;
        }
        else
        {
            InputManager.GetInstance().playerAction?.Invoke();
        }



        if (isGround && currentPlatform != null)
        {
            // Calculate the platform movement since last frame
            Vector3 platformDelta = currentPlatform.transform.position - lastPlatformPosition;

            // Smoothly apply horizontal motion to the player
            Vector3 targetPos = transform.position + new Vector3(platformDelta.x, 0f, platformDelta.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, 1.5f);

            // Update last platform position
            lastPlatformPosition = currentPlatform.transform.position;
        }

    }

    // Apply normal player input
    
    //Debug.Log(coyoteTimeCounter);
    //Debug.Log(coyoteTime);


    // Player moving method
    private void movePlayer()
{
    // Getting values from user input
    moveInput = InputManager.GetInstance().moveAction.action.ReadValue<Vector2>();
    moveInput = new Vector2(moveInput.x, 0); // THIS LINE SHOULD BE REVIEWED WHEN TESTING CONTROLLER MOVEMENT. IT WILL PROBABLY FUCK THINGS UP. - DV
    moveInput.Normalize();

    // Player cannot Input, end
    if (moveInput == Vector2.zero)
    {
        isWalking = false; // IMPORTANT: stop walk state when no input
        targetVisualY = 0f;
        return;
    }

    if (swing != null) // use swinging movement instead - DV
    {
        swing.moveSwing(moveInput);
        return;
    }

    Vector3 playerMovement = new Vector3(moveInput.x * speedAcceleration, rb.linearVelocity.y, rb.linearVelocity.z);

    // Player Movement
    rb.linearVelocity = playerMovement;
    isIdle = false;
    idleTimer = 0f;
    facingCameraTimer = 0f;
    isIdleAnimation = false;

    // Direction
    int dir = (int)Mathf.Sign(moveInput.x);

    // Update facing + walking 
    if (dir > 0)
    {
        isRight = true;
        if (isGround) isWalking = true;
        targetVisualY = 90f; 
    }
    else if (dir < 0)
    {
        isRight = false;
        if (isGround) isWalking = true;
        targetVisualY = -90f;
    }

    

    // Drag player
    //currentSpeed = rb.linearVelocity.magnitude;
    //speedText.text = "Current Speed: " + currentSpeed;
}
    private void startPlayer(InputAction.CallbackContext context)
    {
        //isWalking = true; // only walk on the ground foo - DV
        // lol i didn't realize it,
    }
    private void stopPlayer(InputAction.CallbackContext context)
    {
        // reset player velocity
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        isWalking = false;
    }
    private void jumpStart(InputAction.CallbackContext context)
    {
        if (!playerInput)
        {
            return;
        }
        isIdle = false;
        idleTimer = 0f;
        facingCameraTimer = 0f;
        isIdleAnimation = false;

        if (swing != null)
        {
            isGround = true;
            swing.DestroyHook();
            jumping();
        }
        else if (coyoteTimeCounter >= 0f)
        {
            jumping();
        }
    }
    private void jumpEnd(InputAction.CallbackContext context)
    {
        // end pressing jump button
        isJump = false;
        coyoteTimeCounter = 0f;
    }
    private void jumping()
    {
        // Jump
        // End code if not touching Ground

        // play Audio
        //AudioManager.instance.playPlayerSFX("PlatformJump");
        // FMOD: play one-shot jump event (attached so it follows the player)
        if (playerJumpEvent.Guid != null)
        {
            try
            {
                RuntimeManager.PlayOneShotAttached(playerJumpEvent, gameObject);
            }
            catch
            {
                Debug.LogWarning("FMOD: Failed to play jump event. Check EventReference in inspector.");
            }
        }

        // Turn on Bools
        isJump = true;
        jumpButtonHoldTimer = 0;
        isWalking = false;
        currentPlatform = null;


        anim.SetTrigger("PlayerJump");
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
        //AudioManager.instance.playPlayerWalking(isWalking);
        anim.SetBool("PlayerWalk", isWalking);
        anim.SetBool("PlayerIdle", isIdleAnimation);
        anim.SetBool("PlayerFalling", isfalling);
        anim.SetBool("PlayerPush", isPushingBox);

        // FMOD: start/stop walking loop based on isWalking and grounded state
        if (fmodInitialized)
        {
            if (isWalking && isGround && !walkAudioPlaying)
            {
                walkEventInstance.start();
                walkAudioPlaying = true;
            }
            else if ((!isWalking || !isGround) && walkAudioPlaying)
            {
                walkEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                walkAudioPlaying = false;
            }
        }
    }
    private void isGroundRayCast()
    {
        // Ground Check
        isGround = Physics.Raycast(new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z), Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

        // start pressing jump button
        if (isGround)
        {
            coyoteTimeCounter = coyoteTime;
            isfalling = false;
            //isWalking = true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }
    // aplly froce in air
    private void fixTheAir()
    {
        if (!isGround)
        {
            rb.AddForce(Vector3.down * fixedInAirVelocity, ForceMode.VelocityChange);
        }
    }
    public void freezePlayer(bool isFrozen)
    {
        if (isFrozen) {
            playerInput = false;
            // Stop player from moving
            rb.linearVelocity = Vector3.zero; // if freezePlayer is called every frame, this line has odd effects. be careful with calling freezePlayer. - DV
            isWalking = false;
            //AudioManager.instance.playPlayerWalking(isWalking);
            anim.SetBool("PlayerWalk", isWalking);
            anim.SetBool("PlayerIdle", true);

            // stop walking audio immediately when frozen
            if (fmodInitialized && walkAudioPlaying)
            {
                walkEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                walkAudioPlaying = false;
            }
        } else
        {
            playerInput = true;
        }
        
        
    }
    public void playerRotation()
{
    float targetY;

    if (rb.linearVelocity == Vector3.zero && isIdle && !isPushingBox)
    {
        // idle: face camera
        targetY = 0f;
    }
    else
    {
        // moving: face left/right 
        targetY = isRight ? -90f : 90f;
        isIdle = false; // prevent idle camera-facing from fighting movement
    }

    float currentY = transform.eulerAngles.y;

    float newY = Mathf.SmoothDampAngle(
        currentY,
        targetY,
        ref rootTurnVel,
        turnSmoothTime
    );

    transform.rotation = Quaternion.Euler(0f, newY, 0f);

    dangerDectect.direction = isRight;
}
    // play idle animation
    private void playerIdle()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > idleTime)
        {
            StartCoroutine(startPlayerIdle());
        }
        facingCameraTimer += Time.deltaTime;
        if (facingCameraTimer > 2f)
        {
            isIdle = true;
        }
        //Debug.Log(rb.linearVelocity.y);
        //Debug.Log(rb.linearVelocity.y <= -1);
        //Debug.Log(idleTimer);
    }

    // player idle
    private IEnumerator startPlayerIdle()
    {
        //isIdle = true;
        isIdleAnimation = true;
        // wait for animation to end
        yield return new WaitForSeconds(10f);
        isIdleAnimation = false;
        idleTimer = 0;
    }

    private void playerFalling()
    {
        if (rb.linearVelocity.y <= -1)
        {
            isfalling = true;
        }
        else
        {
            return;
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
    private void OnCollisionEnter(Collision collision)
    {
        NewMonoBehaviourScript platform = collision.gameObject.GetComponent<NewMonoBehaviourScript>();
        if (platform != null)
        {
            currentPlatform = platform;
            // Fix the landing jitter:
            // Snap lastPlatformPosition to current platform position immediately
            lastPlatformPosition = platform.transform.position;
        }
    }
    // when pushing box
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(GeneralGameTags.Box))
        {
            isPushingBox = true;
        }
    }
    // when exit box
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<NewMonoBehaviourScript>() != null)
        {
            currentPlatform = null;
        }
        if (collision.gameObject.CompareTag(GeneralGameTags.Box))
        {
            isPushingBox = false;
        }
    }

    // Gizmo to show ground check raycast
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColour;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Vector3.down * (playerHeight * 0.5f + 0.2f));
    }

    public void Swing(Grapple swinging) // is this good practice? idk man im trying - DV
    {
        swing = swinging;
        if (swing != null)
        {
            anim.SetTrigger("PlayerSwing");
        }
    }

    public void HookJump() // backdooring my way into calling jump on some hook destroys - DV
    {
        jumping();
    }

    public void TeleportTo(Transform target)
    {
        transform.position = target.position;
    }

}