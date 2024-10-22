using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController cc;

    float jump = 10f;
    float vel = 0;
    float grav = -10f;

    float zVel = 1f;
    float zGrav = -10f;

    float moveSpeed = 15f;
    float turnSpeed = 90f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        transform.Rotate(0, turnSpeed * hAxis * Time.deltaTime, 0);
        
        Vector3 amountToMove = transform.forward * moveSpeed * vAxis;

        if (!cc.isGrounded){
            vel += grav * Time.deltaTime;
        }
        else{
            vel = -2.5f;
            
            if(Input.GetKey(KeyCode.Space)){
                vel = jump;
            }
        }
        
        amountToMove.y += vel;
        amountToMove *= Time.deltaTime;

        cc.Move(amountToMove);
    }
}
