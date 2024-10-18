using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlaneScript : MonoBehaviour
{
    float forwardSpeed = 70f; // Starting forward speed
    float maxForwardSpeed = 70f; // The maximum speed (resets to this on collision)
    float minForwardSpeed = 0f; // Minimum speed (plane stops at this speed)
    float slowDownRate = 5f; // Rate at which the plane slows down over time
    float xRotationSpeed = 60f; // Pitch (up and down)
    float yRotationSpeed = 70f; // Yaw (left and right)
    float zRotationSpeed = 60f; // Roll (tilt left and right)
    float zResetSpeed = 2f; // Speed at which the Z-axis rotation is reset
    bool resetZRotation = false;
    int score = 0;

    public TMP_Text scoreText;
    public TMP_Text end;

    void Start()
    {
        // Optional: Initialize speed or other variables if needed
    }

    void Update()
    {
        // Movement inputs
        float pitch = Input.GetAxis("Vertical"); // W/S or UP/DOWN for pitch
        float roll = Input.GetAxis("Horizontal"); // A/D or LEFT/RIGHT for roll
        float yaw = 0f;

        // Yaw input
        if (Input.GetKey(KeyCode.Q))
        {
            yaw = -1f; // Yaw left
        }
        else if (Input.GetKey(KeyCode.E))
        {
            yaw = 1f; // Yaw right
        }

        // Calculate rotation for each axis
        Vector3 rotation = new Vector3(
            pitch * xRotationSpeed * Time.deltaTime,    // Pitch (X-axis)
            yaw * yRotationSpeed * Time.deltaTime,      // Yaw (Y-axis)
            -roll * zRotationSpeed * Time.deltaTime     // Roll (Z-axis)
        );

        // Apply the rotation to the plane
        transform.Rotate(rotation);

        // Move the plane forward
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;

        // Gradually reduce the forward speed over time
        forwardSpeed -= slowDownRate * Time.deltaTime;
        forwardSpeed = Mathf.Clamp(forwardSpeed, minForwardSpeed, maxForwardSpeed); // Prevent speed from going negative
        if(forwardSpeed == 0){
            end.text = "GAME OVER,\nYOU COULDN'T MAKE IT IN TIME";
        }

        // Check if 'C' is pressed to start resetting Z-axis rotation
        if (Input.GetKeyDown(KeyCode.C))
        {
            resetZRotation = true;
        }

        // Gradually reset the Z-axis rotation when 'C' is pressed
        if (resetZRotation)
        {
            Quaternion currentRotation = transform.rotation;
            Vector3 eulerAngles = currentRotation.eulerAngles;
            eulerAngles.z = Mathf.LerpAngle(eulerAngles.z, 0, zResetSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(eulerAngles);

            // Stop resetting once it's close enough to 0
            if (Mathf.Abs(eulerAngles.z) < 0.1f)
            {
                resetZRotation = false;
            }
        }
    }

    // This function will be called when the plane collides with a CollidableObject
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("collectables")){
            
            // Removes/destroys the Collectable
            Destroy(other.gameObject);

            // Reset the forward speed to the maximum speed
            forwardSpeed = maxForwardSpeed;

            // Adds to score and scoreText
            score++;
            if (score == 21){
                scoreText.text = "";
                end.text = "YOU WIN!";
            }
            scoreText.text = "Score: " + score + "\nRemaining:" + (21 - score);

        }     
    }

    
}