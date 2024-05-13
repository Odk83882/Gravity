using UnityEngine;

public class Options : MonoBehaviour
{
    SFXManager audioManager;
    public GameObject optionsCanvas;
    public static bool isPaused;
    
    // Start is called before the first frame update
    void Start()
    {
        
        optionsCanvas.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isPaused)
            {
                TogglePause();
            }
            else
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused; // Toggle the value of isPaused
        optionsCanvas.SetActive(isPaused); // Set the active state of the options canvas
        Time.timeScale = isPaused ? 0f : 1f; // Pause or resume time based on isPaused
    }
}
