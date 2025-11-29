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
    [SerializeField]
    public float groundDrag;
    [SerializeField]
    public float jump, jumpCooldown;
    private bool isJump;
    [SerializeField]
    public float inAirBoost;
    private float currentSpeed;
    [SerializeField]
    private TMP_Text speedText;
    public bool isPushed;
    //private int pushedDirection;
    //private float pushedSpeed;

    // Chekcing Inputs, player movement
    private float verticalInput;
    private float horizontalInput;
    public bool playerInput;
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
    [SerializeField]
    public Camera cam;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private DangerDetect dangerDectect;
    public InputActionReference jumpAction;

    // Dialogue
    static public bool dialogue = false;

    // Gizmo
    private Color gizmoColour = Color.yellow;

    private void OnEnable()
    {
        jumpAction.action.Enable();
        jumpAction.action.performed += jumping;
    }

    private void OnDisable()
    {
        jumpAction.action.Disable();
        jumpAction.action.performed -= jumping;
    }
    // Setting up values at start
    void Start()
    {
        // Getting component from player
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();

        // Setting Up boolean
        isJump = true;
        //isInvincible = false;
        playerInput = true;
    }
    private void Update()
    {
        // Calling methods
        //if (playerInput)
        //{
        //    jumping();
        //}
        Debug.Log(isWalking);
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
        if (!playerInput)
        {
            freezePlayer();
            return; // sadly no simple line like if(!playerInput) freezePlayer(); it must have return :O
        }
        movePlayer();
        fixSpeed();

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
            movePlayer();
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
        // Getting player pressing which button (w,a,s,d)
        verticalInput = Input.GetAxisRaw("Vertical"); // do not use .GetAxis, unlimited speed
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Move player according to input direction and speed

        if (horizontalInput != 0/* && !isPushed*/)
        {
            rb.AddForce(new Vector3(horizontalInput, 0, /*verticalInput*/0) * speedAcceleration);
            if (isJump)
            {
                isWalking = true;
            }
            // Play Foot Foot Steps here
        }
        //else if (horizontalInput != 0 && isPushed)
        //{
        //    //pushingPlayer();
        //}
        //else if (horizontalInput == 0 && isPushed) // this may break if we ever put in controller inputs due to drift - DV
        //{
        //    //pushingPlayer();
        //}

        if (horizontalInput >= 0.01)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            dangerDectect.direction = true;
            //dangerDectect.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontalInput <= -0.99)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            dangerDectect.direction = false;
            //dangerDectect.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        // Drag player
        if (isGround)
        {
            //rb.linearDamping = groundDrag;
             if (horizontalInput == 0 && !isPushed)
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
                // Play Foot Foot Steps here
                isWalking = false;
            }
        }
        else if (!isGround)
        {
            //rb.linearDamping = 0;
            rb.AddForce(new Vector3(horizontalInput, 0, /*verticalInput*/0) * speedAcceleration * inAirBoost);
        }
        if (isPushed)
        {
            Invoke("pushedCooldown", 0.1f);
        }
        // Player speed text field
        currentSpeed = rb.linearVelocity.magnitude;
        speedText.text = "Current Speed: " + currentSpeed;

        isGroundRayCast();

        // Player walking Audio
        AudioManager.instance.playPlayerWalking(isWalking);
        anim.SetBool("PlayerWalk", isWalking);

    }
    private void jumping(InputAction.CallbackContext context)
    {
        // Jump
        //if (/*Input.GetKey(KeyCode.Space)*/jumpAction.action.triggered/* || Input.GetKey(KeyCode.W)*/)
        //{
        //}
        if (isJump && isGround)
        {
            isGround = false;
            isJump = false;

            // Play Jumping Sound
            AudioManager.instance.playPlayerSFX("PlatformJump");
            isWalking = false;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.y);
            rb.AddForce(transform.up * jump, ForceMode.Impulse);

            Invoke(nameof(jumpStatus), jumpCooldown); // cannot hold space now :<
        }
    }
    private void jumpStatus()
    {
        isJump = true;
    }

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

    private void pushedCooldown()
    {
        isPushed = false;
    }

    private void isGroundRayCast()
    {
        
        // Ground Check
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

        if (Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, platformMask))
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
        Invoke("pushedCooldown", 1f);
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
