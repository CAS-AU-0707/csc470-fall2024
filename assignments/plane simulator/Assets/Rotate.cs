using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatte : MonoBehaviour
{
    float rotSpeed = 35f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(0,0,0);
        rotation.y += rotSpeed* Time.deltaTime;
        transform.Rotate(rotation);
    }
}
