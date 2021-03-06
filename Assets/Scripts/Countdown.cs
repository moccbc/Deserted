using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float timeStart = 5;
    public Text textBox;
    public Slider timerSlider;
    public bool gamePlaying;
    

    // Use this for initialization
    void Start()
    {
        gamePlaying = false;
        timerSlider.maxValue = timeStart;
        timerSlider.value = timeStart;
        float minutes = Mathf.Floor(timeStart / 60);
        float seconds = timeStart % 60;
        if (minutes > 9)
        {
            if (Mathf.Floor(seconds) > 9)
            {
                textBox.text = minutes + ":" + seconds.ToString("F1");
            }
            else
            {
                textBox.text = minutes + ":0" + seconds.ToString("F1");
            }
        }
        else
        {
            if (Mathf.Floor(seconds) > 9)
            {
                textBox.text = "0" + minutes + ":" + seconds.ToString("F1");
            }
            else
            {
                textBox.text = "0" + minutes + ":0" + seconds.ToString("F1");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlaying)
        {
            if (timeStart >= 0)
            {
                Timer();
            }
            else
            {
                textBox.text = "Finish";
                FindObjectOfType<GameManager>().EndGame();
                enabled = false;
            }
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
                textBox.text = minutes + ":" + seconds.ToString("F1");
            }
            else
            {
                textBox.text = minutes + ":0" + seconds.ToString("F1");
            }
        }
        else
        {
            if (Mathf.Floor(seconds) > 9)
            {
                textBox.text = "0" + minutes + ":" + seconds.ToString("F1");
            }
            else
            {
                textBox.text = "0" + minutes + ":0" + seconds.ToString("F1");
            }
        }
    }
}
