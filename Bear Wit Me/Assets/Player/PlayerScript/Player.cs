using System.Collections;
using TMPro;
using UnityEngine;


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
    private int pushedDirection;
    private float pushedSpeed;

    // Chekcing Inputs, player movement
    private float verticalInput;
    private float horizontalInput;

    // Checking ground
    [Header("GroundCheck")]
    public bool isGround;
    public LayerMask groundMask;
    public LayerMask platformMask;
    public float playerHeight;
    public bool moveRespawn;

    // Invincibility
    [Header("Invincible")]
    public bool isInvincible;
    [SerializeField]
    private float invinicbleDuration;
    private float invincibleTimer;

    // Getting components
    [Header("Components")]
    [SerializeField]
    public Camera cam;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private DangerDetect dangerDectect;

    // Dialogue
    static public bool dialogue = false;

    // Gizmo
    private Color gizmoColour = Color.yellow;

    // Setting up values at start
    void Start()
    {
        // Getting component from player
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Setting Up boolean
        isJump = true;
        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Calling methods
        movePlayer();
        fixSpeed();
        jumping();

        // Player speed text field
        currentSpeed = rb.linearVelocity.magnitude;
        speedText.text = "Current Speed: " + currentSpeed;

        isGroundRayCast();

        // Check invincible
        if (isInvincible)
        {
            gm.isInvincible = true;
            invincibleTimer += Time.deltaTime;
            if (invincibleTimer > invinicbleDuration)
            {
                gm.isInvincible = false;
            }
        }
    }
    // Player moving method
    private void movePlayer()
    {
        // Getting player pressing which button (w,a,s,d)
        verticalInput = Input.GetAxisRaw("Vertical"); // do not use .GetAxis, unlimited speed
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Move player according to input direction and speed

        if (horizontalInput != 0 && !isPushed)
        {
            rb.AddForce(new Vector3(horizontalInput, 0, /*verticalInput*/0) * speedAcceleration);
        }
        else if (horizontalInput != 0 && isPushed)
        {
            pushingPlayer();
        }
        else if (horizontalInput == 0 && isPushed) // this may break if we ever put in controller inputs due to drift - DV
        {
            pushingPlayer();
        }

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
    }
    private void jumping()
    {
        // Jump
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (isJump && isGround)
            {
                isGround = false;
                isJump = false;

                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.y);
                rb.AddForce(transform.up * jump, ForceMode.Impulse);

                Invoke(nameof(jumpStatus), jumpCooldown);
            }
        }
    }
    private void jumpStatus()
    {
        isJump = true;
    }
    private void FixedUpdate()
    {
        if (!dialogue)
        {
            movePlayer();
        }
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

    public void isPushedDirection(int direction, float force)
    {
        pushedDirection = direction;
        pushedSpeed = force;
    }

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

    private void pushingPlayer()
    {
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColour;
        Gizmos.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f));
    }
}
