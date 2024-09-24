using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // This method will be called when a collision happens
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with has the "PlaneScript" component (i.e., it's the plane)
        if (other.gameObject.GetComponent<PlaneScript>() != null)
        {
            // Print a message to the console
            Debug.Log("Collided");

            // Destroy this object
            Destroy(gameObject);
        }
    }
}
