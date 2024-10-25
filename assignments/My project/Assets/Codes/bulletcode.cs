using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcode : MonoBehaviour
{
    float speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall")){
            Destroy(gameObject);
        }
        if(other.CompareTag("Player")){
            Player player = other.GetComponent<Player>();
            player.lives--;
            Destroy(gameObject);
        }
    }
}
