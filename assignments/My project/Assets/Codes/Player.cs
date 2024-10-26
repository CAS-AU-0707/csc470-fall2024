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
    float grav = -20f;
    bool dou = false;

    float zVel = 30f;
    float zGrav = -15f;

    float moveSpeed = 15f;
    float turnSpeed = 130f;

    int col = 6;
    float timer = 0;
    bool gameover = false;
    bool ig = false;
    public int lives = 3;
    bool key = false;
    int boost = 5;
    bool bos = false;

    public TMP_Text colletables;
    public TMP_Text time;
    public TMP_Text center;
    public TMP_Text life;
    public TMP_Text boostCount;




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

            if (Input.GetKeyDown(KeyCode.LeftShift) && (boost > 0 || ig))
            {
                bos = true;
                moveSpeed = 40f;
                boost--;
            }
            else if (bos)
            {
                moveSpeed -= 20f * Time.deltaTime;
                if (moveSpeed <= 15f)
                {
                    moveSpeed = 15f;
                    bos = false;
                }
            }

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

            if(col == 0 || Input.GetKeyDown(KeyCode.G)){
                center.text = "You Win!";
                time.text = "";
                colletables.text = "";
                life.text = "";
                gameover = !gameover;
            }
            else if(timer >= 40 || lives == 0){
                gameover = true;
                time.text = "";
                colletables.text = "";
                life.text = "";
            }
            else{
                time.text = ((int)(40f - timer)).ToString();
                colletables.text = col.ToString();
                center.text = "";
                life.text = "Lives: " + lives.ToString();
                boostCount.text = "Boost: " + boost.ToString();
            }
        }
        else{
            center.text = "Game Over";
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("collectable")){
            
            Destroy(other.gameObject);

            col--;
            timer = 0;

        }

        if (other.CompareTag("key"))
        {

            Destroy(other.gameObject);
            key = true;
        }

        if (other.CompareTag("door") && key)
        {
            Destroy(other.gameObject);

        }

        if (other.CompareTag("LAVA"))
        {

            lives = 0;

        }
        else
        {
            Debug.Log("HIT!");
        }

    }
}
