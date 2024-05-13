using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    SFXManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SFXManager>();
        //quitButton.onClick.AddListener(Quit);
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        audioManager.PlaySFX(audioManager.click);
        SceneManager.LoadSceneAsync("lvl1");
    }

    

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}     