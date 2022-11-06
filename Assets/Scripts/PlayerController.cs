using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public LayerMask playerLayerMask;

    public float speed = 10f;
    public float dashDelay;
    public float dashSpeed;
    public float dashTime;

    [Header("Player Grounded")]
    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.5f;
    public float PlayerGravity = -15f;
    public float FallStatusTimeout = 0.15f;
    public LayerMask GroundLayers;

    private bool safe = false;

    private CharacterController controller;
    private Animator animator;
    private float dashCooldown = 0;

    private float terminalVelocity = 53f;
    private float verticalVelocity;
    private float fallStatusTimeoutDelta;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        fallStatusTimeoutDelta = FallStatusTimeout;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            //animator.SetTrigger("Run");
            animator.SetBool("Run", true);
            controller.Move((direction * speed * Time.deltaTime) + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
        }
        else
            animator.SetBool("Run", false);

        RotateWithMouseVector();

        if(Input.GetButton("Jump") && dashCooldown <= 0)
        {
            StartCoroutine(Dash(direction));
        }
        dashCooldown -= Time.deltaTime;

        // Ground check
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        Gravity();
    }

    private void RotateWithMouseVector()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, maxDistance:100f, playerLayerMask))
        {
            Vector3 target = hit.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    IEnumerator Dash(Vector3 dir)
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            controller.Move(dir * dashSpeed * Time.deltaTime);

            yield return null;
        }
        dashCooldown = dashDelay;
    }

    private void Gravity()
    {
        if (Grounded)
        {
            fallStatusTimeoutDelta = FallStatusTimeout;
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;
        }
        else
        {
            if (fallStatusTimeoutDelta >= 0f)
                fallStatusTimeoutDelta -= Time.deltaTime;
        }

        if (verticalVelocity < terminalVelocity)
            verticalVelocity += PlayerGravity * Time.deltaTime;
    }

    public bool GetSafe()
    {
        return safe;
    }

    public void SetSafe(bool isSafe)
    {
        safe = isSafe;
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded)
            Gizmos.color = transparentGreen;
        else
            Gizmos.color = transparentRed;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
    }
}
