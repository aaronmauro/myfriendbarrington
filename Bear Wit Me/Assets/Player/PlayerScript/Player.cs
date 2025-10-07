using UnityEngine;

public class Player : MonoBehaviour
{
    // This is the player script
    // Setting serializable field for editor to edit in inspector
    [Header("Movement")]
    [SerializeField] 
    public float speed;
    [SerializeField]
    public float groundDrag;
    [SerializeField]
    public float jump, jumpCooldown;
    private bool isJump;
    [SerializeField]
    public float inAirBoost;

    // Chekcing Inputs, player movement
    private float verticalInput;
    private float horizontalInput;

    // Checking ground
    public bool isGround;

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

    static public bool dialogue = false;

    // Setting up values at start
    void Start()
    {
        // Getting component from player
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Setting Up boolean
        isJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Calling methods
        movePlayer();

        // Drag player
        if (isGround)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }

        // Jump
        if (Input.GetKey(KeyCode.Space) && isJump && isGround)
        {
            isGround = false;
            isJump = false;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.y);
            rb.AddForce(transform.up * jump, ForceMode.Impulse);

            Invoke(nameof(jumpStatus), jumpCooldown);
        }

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
        rb.AddForce(new Vector3(horizontalInput, 0, verticalInput) * speed);
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


}
