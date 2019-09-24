using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private mazeGenerator generator;

    [SerializeField]
    private cameraAdjust adjust;

    public float currentTime = 0f;
    public float startingTime = 10f;
    private bool startTime = false;

    [SerializeField]
    Text countDownClock;

    void Start()
    {
        generator = GameObject.Find("mazeGenerator").GetComponent<mazeGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        //this is to see if the time has started
        if (startTime == true)
        {
            //this updates the timer an puts it in the UI
            currentTime -= 1 * Time.deltaTime;
            countDownClock.text = currentTime.ToString("0");

            //this will see if the timer has run out and so yes then it will start the timer again, create a new maze and adjusts the camera
            if (currentTime <= 0)
            {
                currentTime = startingTime;
                generator.createWalls();
                adjust.Adjust();


            }
        }


    }
    //this will be called when the reset button has been pressed
    public void timeStart()
    {
        currentTime = startingTime;
        startTime = true;
    }
    //here the time is defined and put into the startingTime
    public void timeSliderChanger(float value)
    {
        startingTime = Mathf.RoundToInt(value);
    }
}
