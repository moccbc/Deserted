using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float timeStart = 180;
    public Text textBox;
    public Slider timerSlider;
    

    // Use this for initialization
    void Start()
    {
        timerSlider.maxValue = timeStart;
        timerSlider.value = timeStart;
        float minutes = Mathf.Floor(timeStart / 60);
        float seconds = timeStart % 60;
        textBox.text = minutes + ":" + seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStart >= 0)
        {
            Timer();
        }
        else
        { 
            textBox.text = "Finish";
        }
    }

    void Timer() 
    {
        float minutes = Mathf.Floor(timeStart / 60);
        float seconds = timeStart % 60;

        timeStart -= Time.deltaTime;
        timerSlider.value = timeStart;

        if (minutes > 9)
        {
            if (Mathf.Floor(seconds) > 9)
            {
                textBox.text = minutes + ":" + seconds;
            }
            else
            {
                textBox.text = minutes + ":0" + seconds;
            }
        }
        else
        {
            if (Mathf.Floor(seconds) > 9)
            {
                textBox.text = "0" + minutes + ":" + seconds;
            }
            else
            {
                textBox.text = "0" + minutes + ":0" + seconds;
            }
        }
    }
}
