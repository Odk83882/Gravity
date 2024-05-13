using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Obstacle"))
        {
            Debug.Log("Hello");

            PlayerMovement.instance.Die();
        }

    }
}
