using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }
    public IEnumerator CountdownToStart()
    {
        //Time.timeScale = 0f;
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
            Debug.Log(countdownTime);
        }

        countdownDisplay.text = "GO!";

        //Time.timeScale = 1f;
        GameManager.instance.BeginGame();

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }
}
