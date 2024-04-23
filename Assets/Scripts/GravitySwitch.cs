using UnityEngine;
using UnityEngine.SceneManagement;

public class GravitySwitch: MonoBehaviour
{
    // Reference to the Rigidbody2D component of the object
    private Rigidbody2D rb2D;

    // Gravity multiplier to adjust the strength of gravity
    public float gravityMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to the object
        rb2D = GetComponent<Rigidbody2D>();

        // Set the default gravity scale
        rb2D.gravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the 'G' key is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Toggle the gravity multiplier
            gravityMultiplier *= -1f;
            PlayerMovement.instance.fallLongMult = -0.85f;
            PlayerMovement.instance.fallShortMult = -1.55f;
        }

        // Adjust the gravity scale based on the multiplier
        rb2D.gravityScale = 1f * gravityMultiplier;
    }
}
