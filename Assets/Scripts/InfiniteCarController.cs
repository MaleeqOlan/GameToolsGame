using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class InfiniteCarController : MonoBehaviour

{
    public float moveSpeed = 10f; // Adjust this value to set the car's constant speed
    public float turnSpeed = 2f; // Adjust this value to set the car's turning sensitivity

    void Update()
    {
        // Move the car forward along its forward axis
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Allow the car to turn slightly based on horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * turnSpeed);
    }
}

