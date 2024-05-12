using System.Collections;
using UnityEngine;

public class DashingAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    void Start()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();

        // Set the initial state of the IsDashingAnimation parameter to false
        animator.SetBool("IsDashingAnimation", false);

        // Get the SpriteRenderer component attached to the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Disable the SpriteRenderer initially
        spriteRenderer.enabled = false;
    }

    void Update()
    {
        // Check if the character is currently dashing
        if (isDashing)
            return;

        // Check for dash input and cooldown
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            // Start dashing coroutine
            StartCoroutine(Dashing());
        }
    }

    IEnumerator WaitForDelay()
    {
        // Wait for the duration of the dashing animation
        yield return new WaitForSeconds(0.25f);

        // Disable the SpriteRenderer after the delay
        spriteRenderer.enabled = false;

        // End the dashing animation
        animator.SetBool("IsDashingAnimation", false);
    }

    private IEnumerator Dashing()
    {
        // Set dash flags
        canDash = false;
        isDashing = true;

        // Start dashing animation
        animator.SetBool("IsDashingAnimation", true);

        // Enable the SpriteRenderer
        spriteRenderer.enabled = true;

        // Wait for the duration of the dashing time
        yield return new WaitForSeconds(dashingTime);

        // Disable dashing animation and SpriteRenderer
        isDashing = false;
        animator.SetBool("IsDashingAnimation", false);
        spriteRenderer.enabled = false;

        // Start cooldown
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        // Wait for the dash cooldown duration
        yield return new WaitForSeconds(dashingCooldown);

        // Allow dashing again
        canDash = true;
    }
}
