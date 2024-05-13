using UnityEngine;
using UnityEngine.SceneManagement;

public class GravitySwitch: MonoBehaviour
{

    private Rigidbody2D rb2D;
    public float gravityMultiplier = 1.0f;
    public static GravitySwitch instance;
    private bool isToggled;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        rb2D.gravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerMovement.instance.isGrounded == true)
        {
            isToggled = !isToggled;
            GravityShift();  
        }

        rb2D.gravityScale = 1f * gravityMultiplier;
    }

    public void GravityShift()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        gravityMultiplier *= -1f;
        PlayerMovement.instance.jumpVelocity = -PlayerMovement.instance.jumpVelocity;
        if (isToggled)
        {
            PlayerMovement.instance.isUpsideDown = true;
            spriteRenderer.flipY = true;
        }
        else
        {
            PlayerMovement.instance.isUpsideDown = false;
            spriteRenderer.flipY = false;
        }
    }   
}