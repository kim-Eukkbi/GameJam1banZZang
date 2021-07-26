using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Player player;
    [SerializeField]
    private TextTimerViewer textTimerViewer;
    [SerializeField]
    private PizzaSpawner pizzaSpawner;
    [SerializeField]
    private Text textAnimTimerText;

    [SerializeField]
    private GameObject gameOverFloor;
    [SerializeField]
    private GameObject fragments;

    [SerializeField]
    private int minusTime = 2;

    public int time = 60;
    public int destroyedPlate = 0;

    public bool canMiss = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void DestroyPlate()
    {
        instance.destroyedPlate++;
    }

    public void MissPlate()
    {
        if (!canMiss) return;

        if(!(time - minusTime <= 0))
        {
            time -= minusTime;
        }

        textTimerViewer.UpdateTimer(time);

        if (time <= 10) textAnimTimerText.color = Color.red;
        textAnimTimerText.text = time.ToString();

        textAnimTimerText.gameObject.SetActive(true);
        textAnimTimerText.gameObject.GetComponent<RectTransform>().DOScale(1.5f, 1f).OnComplete(() =>
        {
            textAnimTimerText.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            textAnimTimerText.gameObject.SetActive(false);
        });
    }

    public void GameOver()
    {
        for (int i = 0; i < pizzaSpawner.plates.Count; i++)
        {
            Destroy(pizzaSpawner.plates[i].gameObject);
        }

        for (int i = 0; i < pizzaSpawner.plates.Count; i++)
        {
            pizzaSpawner.plates.RemoveAt(i);
        }

        player.speedTMP.gameObject.SetActive(false);

        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y - 500, player.transform.position.z);
        Vector3 pos2 = new Vector3(player.transform.position.x + 12.5f, player.transform.position.y - 450, player.transform.position.z);

        Instantiate(gameOverFloor, pos, Quaternion.identity);
        Instantiate(fragments, pos2, Quaternion.identity);
    }
}
