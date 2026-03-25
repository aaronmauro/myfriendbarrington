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
    private float swingDirection = 90f;
    [SerializeField] Transform hand;

    Hook hook;
    bool pulling;
    Rigidbody rigid;
    Player player;
    private GrapplePoint highlightedPoint; // NEW

    public InputActionReference hookAction;

    private void OnEnable()
    {
        hookAction.action.Enable();
    }

    private void OnDisable()
    {
        hookAction.action.Disable();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        pulling = false;
        player = gameObject.findPlayer();
    }

    void Update()
    {
        UpdateIndicator(); // NEW

        if (hook == null && hookAction.action.triggered && !player.isInteracting)
        {
            GrapplePoint availablePoint = GetNearestGrapplePoint();
            if (availablePoint != null)
            {
                StopAllCoroutines();
                pulling = false;
                player.Shoot();
                hook = Instantiate(hookPrefab, shootTransform.position, Quaternion.identity).GetComponent<Hook>();
                hook.Initialize(this, shootTransform);
                hook.hand = hand;
                StartCoroutine(DestroyHookAfterLifetime());
            }
        }
        else if (hook != null && hookAction.action.triggered)
        {
            if (hook.hasLatched)
            {
                player.HookJump();
            }
            DestroyHook();
        }
    }

    // NEW
    private void UpdateIndicator()
    {
        GrapplePoint nearest = hook == null ? GetNearestGrapplePoint() : null;

        if (nearest == highlightedPoint) return;

        if (highlightedPoint != null)
            highlightedPoint.HideIndicator();

        highlightedPoint = nearest;

        if (highlightedPoint != null)
            highlightedPoint.ShowIndicator();
    }

    private void FixedUpdate()
    {
        if (!pulling || hook == null) return;

        Vector3 targetDirection = hook.transform.position - transform.position;
        float singleStep = 5f * Time.fixedDeltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.up, targetDirection, singleStep, 0f);
        newDirection = Quaternion.Euler(0, 0, swingDirection) * newDirection;
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (Vector3.Distance(transform.position, hook.transform.position) <= stopDistance)
        {
            // new behaviour - DV
        }
        else
        {
            rigid.AddForce((hook.transform.position - transform.position).normalized * pullSpeed, ForceMode.VelocityChange);
            if (player.isRight && !applyForce)
            {
                rigid.AddForce(Vector3.right * 2f, ForceMode.Impulse);
                forceTimer += Time.deltaTime;
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
        }
    }

    private void fixSpeed()
    {
        Vector3 getLinearVelocity = new Vector3(rigid.linearVelocity.x, rigid.linearVelocity.y, rigid.linearVelocity.z);
        if (getLinearVelocity.magnitude > maxHookSpeed)
        {
            Vector3 limitLinearVelocity = getLinearVelocity.normalized * maxHookSpeed;
            rigid.linearVelocity = new Vector3(limitLinearVelocity.x, limitLinearVelocity.y * 0.5f, limitLinearVelocity.z);
        }
    }

    GrapplePoint GetNearestGrapplePoint()
    {
        var points = FindObjectsOfType<GrapplePoint>();
        foreach (var point in points)
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
        if (player.isRight)
        {
            swingDirection = 90f;
        }
        else
        {
            swingDirection = -90f;
        }
    }

    public void DestroyHook()
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
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }

    public void moveSwing(Vector2 moveInput)
    {
        Vector3 playerMovement = transform.forward * moveInput.x * swingDirection * -2f;
        rigid.AddForce(playerMovement);
    }
}