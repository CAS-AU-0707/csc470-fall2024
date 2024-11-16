using System.Collections;
using System.Collections.Generic;
using UnityEngine;

float hover;
bool peak = true;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range(0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        hover = Mathf.Sin((Time.deltaTime()* Mathf.PI) + rand);

        transform.position = new Vector3(transform.position.x, hover, transform.position.z);
    }
}
