using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb2D;
    public float life = 3;
    private bool isDestroyed = false; // Flag to track if the bullet is already marked for destruction

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Awake()
    {

        StartCoroutine(BulletLifeEnd());

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerMovement.instance.Die();
        }
        else if (other.gameObject.CompareTag("BulletDestroyer"))
        {
            StartCoroutine(BulletDestroy());
        }
    }

    private IEnumerator BulletDestroy()
    {
        // Play animation or perform any other action
        animator.SetBool("isTouchingWall", true);


        // Stop the bullet's movement
        rb2D.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.30f); 
        Destroy(gameObject);
    }



private IEnumerator BulletLifeEnd()
{
    // Check if the bullet is already marked for destruction
    if (isDestroyed)
    {
        yield break; // Exit the coroutine early if already marked for destruction
    }

    isDestroyed = true; // Mark the bullet for destruction

    // Reference to the Rigidbody2D component
    Rigidbody2D rb = GetComponent<Rigidbody2D>();

    // Wait for the bullet's life duration
    yield return new WaitForSeconds(life);

    // Play the animation for a short duration
    animator.SetBool("isTouchingWall", true);

    // Gradually slow down the bullet's velocity until it reaches zero
    float slowdownDuration = 0.5f; // Duration over which the bullet slows down (adjust as needed)
    float elapsedTime = 0f; // Elapsed time during slowdown

    while (elapsedTime < slowdownDuration)
    {
        // Calculate the slowdown factor based on the elapsed time
        float slowdownFactor = 1f - (elapsedTime / slowdownDuration);

        // Apply the slowdown factor to the bullet's velocity
        rb.velocity *= slowdownFactor;

        // Increment the elapsed time
        elapsedTime += Time.deltaTime;

        // Wait for the next frame
        yield return null;
    }

    // Set the bullet's velocity to zero
    rb.velocity = Vector2.zero;

    yield return new WaitForSeconds(0.2f);

    // Turn off the animation
    animator.SetBool("isTouchingWall", false);

    // Destroy the bullet GameObject
    Destroy(gameObject);
}



}
