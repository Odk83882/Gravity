using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleteController : MonoBehaviour
{
    public SFXManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SFXManager>();
        StartCoroutine(EndCreditsFlash());
    }

    private IEnumerator EndCreditsFlash()
    {
        audioManager.PlaySFX(audioManager.thanksForPlaying);
        Debug.Log("SceneLoaded");
        yield return new WaitForSeconds(1f); 
        Debug.Log("SceneChanged");
        SceneManager.LoadScene(0);
    }
}
