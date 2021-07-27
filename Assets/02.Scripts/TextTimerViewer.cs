using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using DG.Tweening;

public class TextTimerViewer : MonoBehaviour
{
    [SerializeField]
    private Text textAnimTimeText;
    [SerializeField]
    private Text textTimer;
    private Text textTime;

    void Start()
    {
        textTime = GetComponent<Text>();

        StartCoroutine(UpdateTimerRoutine());
    }

    public void UpdateTimer(int time)
    {
        if (time <= 10)
        {
            textTime.color = Color.red;
        }

        if (time <= 2)
        {
            StartCoroutine(GameManager.instance.CamaraMove());
        }

        if (time <= 0)
        {
            GameManager.instance.GameOver();
        }

        textAnimTimeText.text = time.ToString();
        textTime.text = time.ToString();
    }

    public void MissTimer()
    {
        if (GameManager.instance.time <= 10) textAnimTimeText.color = Color.red;

        textTime.enabled = false;
        textAnimTimeText.enabled = true;
        textAnimTimeText.gameObject.GetComponent<RectTransform>().DOScale(1.5f, 1f).OnComplete(() =>
        {
            textAnimTimeText.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            textAnimTimeText.enabled = false;
            textTime.enabled = true;
        });
    }

    public void TimerEnable(bool enable)
    {
        textTime.enabled = enable;
        textAnimTimeText.enabled = enable;
        textTimer.enabled = enable;
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
