using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasureCollider : MonoBehaviour
{

    private mazeGenerator generator;
    private cameraAdjust adjust;

    private GameObject activateUI;
    //private GameObject Walls;

    void Start()
    {
        generator = GameObject.Find("mazeGenerator").GetComponent<mazeGenerator>();
        adjust = GameObject.Find("Main Camera").GetComponent<cameraAdjust>();
        //this will make sure that the right camera script is used
        if (mazeGenerator.FPS)
        {
            adjust = GameObject.Find("FPSCamera").GetComponent<cameraAdjust>();
        }
        // Walls = GameObject.Find("wallHolder");
        //  activateUI = GameObject.Find("UIParent");
    }
    //this will check if it collided with the player
    void OnCollisionEnter(Collision other)
    {
        //this will check if the collider is the player
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            generator.winFunctionGen();
         //   activateUI.SetActive(true);
            
        }
    }
    void Update()
    {
       
       // print(activateUI);

    }
}
