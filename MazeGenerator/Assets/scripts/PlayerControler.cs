using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    [SerializeField]
    private cameraAdjust adjust;

    public float playerSpeed = 2f;
    Rigidbody rigidbody;



    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }
    //this makes the player move
    void FixedUpdate()
    {

        //this will check if the FPS camera is on
        if (mazeGenerator.FPS)
        {
            //this will only move the player forward the rest of the ifs is meant for rotation
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, 0, playerSpeed * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Rotate(0f, 90f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.Rotate(0f, 180f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(0f, 270f, 0f);
            }
        }
        //this is for the topdown view of the level
        else if (!mazeGenerator.cameraTurned)
        {
            playerSpeed = 3f;
            //forward
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, 0, playerSpeed * Time.deltaTime);
            }
            //rightside
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, 0, -playerSpeed * Time.deltaTime);
            }
            //backwards
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
            }
            //leftside
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
            }
        }
        //this will check if the camera is turned
        else if (mazeGenerator.cameraTurned)
        {
            //forward
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);

            }
            //rightside
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
            }
            //backwards
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(0, 0, playerSpeed * Time.deltaTime);
            }
            //leftside
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(0, 0, -playerSpeed * Time.deltaTime);
            }
        }


    }


}
