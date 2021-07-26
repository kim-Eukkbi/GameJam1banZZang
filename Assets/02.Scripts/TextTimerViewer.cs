using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TextTimerViewer : MonoBehaviour
{
    private Text tiemrText;
    private int time = 60;
    void Start()
    {
        tiemrText = GetComponent<Text>();

        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1f);

            time--;
            tiemrText.text = time.ToString();
        }
    }
}
