using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TextTimerViewer : MonoBehaviour
{
    private Text timer;

    void Start()
    {
        timer = GetComponent<Text>();

        StartCoroutine(UpdateTimerRoutine());
    }

    public void UpdateTimer(int time)
    {
        if (GameManager.instance.time <= 0)
        {
            GameManager.instance.GameOver();
        }
        else if (GameManager.instance.time <= 10)
        {
            timer.color = Color.red;
        }

        timer.text = string.Concat("<size=60>Time</size>\n", time.ToString());
    }

    private IEnumerator UpdateTimerRoutine()
    {
        while (GameManager.instance.time > 0)
        {
            yield return new WaitForSeconds(1f);

            GameManager.instance.time--;
            UpdateTimer(GameManager.instance.time);
        }
    }
}
