using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TextTimerViewer : MonoBehaviour
{
    private Text tiemrText;

    void Start()
    {
        tiemrText = GetComponent<Text>();

        StartCoroutine(UpdateTimerRoutine());
    }

    public void UpdateTimer(int time)
    {
        tiemrText.text = time.ToString();
    }

    private IEnumerator UpdateTimerRoutine()
    {
        while (GameManager.instance.time > 0)
        {
            yield return new WaitForSeconds(1f);

            GameManager.instance.time--;
            tiemrText.text = GameManager.instance.time.ToString();
        }
    }
}
