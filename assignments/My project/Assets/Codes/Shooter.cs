using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bullet;
    float timer = 0;
    Vector3 pos;
    Vector3 rota;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        pos.y += 2.9f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if ((timer) > 1){
            GameObject shot = Instantiate(bullet, pos, transform.rotation);
            timer = 0;
        }
    }
}
