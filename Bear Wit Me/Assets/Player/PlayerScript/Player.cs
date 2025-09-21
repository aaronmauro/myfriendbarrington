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

    // Chekcing Inputs
    private float verticalInput;
    private float horizontalInput;

    // Checking ground
    public bool isGround;

    // Getting components
    [Header("Components")]
    [SerializeField]
    public Camera cam;
    [SerializeField]
    private Rigidbody rb;

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
}
