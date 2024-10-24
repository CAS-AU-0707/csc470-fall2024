using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public CharacterController cc;

    float jump = 10f;
    float jump1 = 7.5f;
    float vel = 0;
    float grav = -10f;
    bool dou = false;

    float zVel = 1f;
    float zGrav = -15f;

    float moveSpeed = 15f;
    float turnSpeed = 130f;

    int col = 3;
    float timer = 0;
    bool gameover = false;
    bool ig = false;

    public TMP_Text colletables;
    public TMP_Text time;
    public TMP_Text center;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P)){
            ig = true;
        }

        if (!gameover || ig){
            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            transform.Rotate(0, turnSpeed * hAxis * Time.deltaTime, 0);
        
            Vector3 amountToMove = transform.forward * moveSpeed * vAxis;

            if (!cc.isGrounded){
                vel += grav * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space) && !dou){
                    dou = true;
                    vel = jump1;
                }
            }
            else{
                dou = false;
                vel = -2.5f;
            
                if(Input.GetKeyDown(KeyCode.Space)){
                    vel = jump;
                }
            }
        
            amountToMove.y += vel;
            amountToMove *= Time.deltaTime;

            cc.Move(amountToMove);

            timer += Time.deltaTime;

            if(col == 0){
                    center.text = "You Win!";
                    time.text = "";
                colletables.text = "";
            }
            else if(timer >= 20){
                gameover = true;
                time.text = "";
                colletables.text = "";
            }
            else{
                time.text = ((int)(20f - timer)).ToString();
                colletables.text = col.ToString();
                center.text = "";
            }
        }
        else{
            center.text = "Game Over";
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("collectable")){
            
            // Removes/destroys the Collectable
            Destroy(other.gameObject);

            // Adds to score and scoreText
            col--;
            timer = 0;

        }     
    }
}
