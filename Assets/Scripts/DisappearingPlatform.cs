using System.Collections;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public GameObject platformDown;
    public GameObject platformUp;
    public float firstDelay = 1f;
    public float secondDelay = 2f;
    

    private void Initialize()
    {
        // if (platformObject == null)
        // {
        //     Debug.LogError("Platform object not assigned. Please assign the platform object in the Unity editor.");
        //     return;
        // }

        StartCoroutine(DisappearAndReappear());
    }

    IEnumerator DisappearAndReappear()
    {
        while (true)
        {
            yield return new WaitForSeconds(firstDelay);

            platformDown.SetActive(true);

            yield return new WaitForSeconds(firstDelay);
            
            platformUp.SetActive(true);

            yield return new WaitForSeconds(firstDelay);

            platformDown.SetActive(false);

            yield return new WaitForSeconds(firstDelay);

            platformDown.SetActive(true);

            

            yield return new WaitForSeconds(firstDelay);

            platformUp.SetActive(false);

            yield return new WaitForSeconds(firstDelay);
            
            platformUp.SetActive(true);

            
            yield return new WaitForSeconds(firstDelay);

            platformDown.SetActive(false);

            

            yield return null;
        }
    }


    private void Start()
    {
        platformDown.SetActive(false);
        platformUp.SetActive(false);
        Initialize();
    }
}