using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    float fs = 12f;
    float xrs = 90f;
    float yrs = 90f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        float zAxis = Input.GetKey("z");

        transform.Rotate(vAxis * xrs * dt, hAxis * yrs * dt, 0, Space.Self);
        transform.position += transform.forward * fs * dt;


    }
}
