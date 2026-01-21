using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



public class Grapple : MonoBehaviour
{
    [SerializeField] float pullSpeed = 0.5f;
    [SerializeField] float stopDistance = 4f;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] Transform shootTransform;
    [SerializeField] float maxHookSpeed = 20f;
    private float forceTimer;
    [SerializeField] private float forceIncreaseTime = 1.5f;
    [SerializeField] float lifetime = 0f;
    private bool applyForce;

    private GrapplePoint[] grapplePoints;

    Hook hook;
    bool pulling;
    Rigidbody rigid;
    Player player;

    public InputActionReference hookAction;

    private void OnEnable()
    {
      hookAction.action.Enable();
    }

    private void OnDisable()
    {
        hookAction.action.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Rigidbody component
        rigid = GetComponent<Rigidbody>();
        pulling = false;
        grapplePoints = FindObjectsOfType<GrapplePoint>();
        player = gameObject.findPlayer();
    }



    // Update is called once per frame
    void Update()
    {
        // Shoot or retract the hook based on player input
        if (hook == null && hookAction.action.triggered) //Input.GetKeyDown(KeyCode.E))
        {
            GrapplePoint availablePoint = GetNearestGrapplePoint();
            if (availablePoint != null)
            {
                StopAllCoroutines();
                pulling = false;
                hook = Instantiate(hookPrefab, shootTransform.position, Quaternion.identity).GetComponent<Hook>();
                hook.Initialize(this, shootTransform);
                StartCoroutine(DestroyHookAfterLifetime());
            }
        }
        else if (hook != null && hookAction.action.triggered)
        {
            DestroyHook();
        }
    }

    private void FixedUpdate()
    {
        if (!pulling || hook == null) return;
        if (Vector3.Distance(transform.position, hook.transform.position) <= stopDistance)
        {
            //DestroyHook(); new behaviour - DV
        }
        else
        {
            rigid.AddForce((hook.transform.position - transform.position).normalized * pullSpeed, ForceMode.VelocityChange);
            if (player.isRight && !applyForce)
            {
                rigid.AddForce(Vector3.right * 2f, ForceMode.Impulse);
                forceTimer += Time.deltaTime;
                //Debug.Log(forceTimer);
                if (forceTimer >= forceIncreaseTime)
                {
                    applyForce = true;
                }
            }
            else if (!player.isRight && !applyForce)
            {
                rigid.AddForce(-Vector3.right * 2f, ForceMode.Impulse);
                forceTimer += Time.deltaTime;
                if (forceTimer >= forceIncreaseTime)
                {
                    applyForce = true;
                }
            }
            fixSpeed();
        }
    }

    private void fixSpeed()
    {
        // Check for speed (wihtout jumping)
        Vector3 getLinearVelocity = new Vector3(rigid.linearVelocity.x, rigid.linearVelocity.y, rigid.linearVelocity.z);
        // If exceed the speed
        if (getLinearVelocity.magnitude > maxHookSpeed)
        {
            Vector3 limitLinearVelocity = getLinearVelocity.normalized * maxHookSpeed;
            rigid.linearVelocity = new Vector3(limitLinearVelocity.x, limitLinearVelocity.y * 0.5f, limitLinearVelocity.z);
        }
    }


    // Get the nearest grapple point within range
    GrapplePoint GetNearestGrapplePoint()
    {
        foreach (var point in grapplePoints)
        {
            if (point.IsInRange(transform.position))
                return point;
        }
        return null;
    }


    public void StartPull()
    {
        pulling = true;
        player.Swing(this);
    }

    private void DestroyHook()
    {
        if (hook == null) return;

        pulling = false;
        player.Swing(null);
        applyForce = false;
        forceTimer = 0f;
        Destroy(hook.gameObject);
        hook = null;
    }

    private IEnumerator DestroyHookAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        if (lifetime == 0f) yield break;
        DestroyHook();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,stopDistance);
    }

    public void moveSwing(Vector2 moveInput)
    {
        Vector3 playerMovement = new Vector3(moveInput.x * 200f, rigid.linearVelocity.y, rigid.linearVelocity.z);
        //Debug.Log(rb.linearVelocity.y + "-1");
        //Debug.Log("before: "+playerMovement.ToString());
        // HERE WE GO LADS
        //playerMovement = Vector3.RotateTowards(playerMovement.y, hook.transform.position - transform.position, 10f, 0f);
        //Debug.Log("after:  "+playerMovement.ToString());
        // Player Movement
        rigid.AddForce(playerMovement);
    }
}
