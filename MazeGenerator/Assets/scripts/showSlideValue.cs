using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showSlideValue : MonoBehaviour
{
    Text sliderValue;
    // Start is called before the first frame update
    void Start()
    {
        sliderValue = GetComponent<Text>();
    }

    //this will get the value from the sliders an puts them in text above the sliders
    public void textUpdateX(float value)
    {
        sliderValue.text = "X-Size: " + Mathf.Round(value);
    }
    public void textUpdateY(float value)
    {
        sliderValue.text = "Y-Size: " + Mathf.Round(value);
    }
    public void textUpdateTimer(float value)
    {
        sliderValue.text = "Seconds: " + Mathf.Round(value);
    }

}
