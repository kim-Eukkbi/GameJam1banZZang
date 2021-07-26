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

        if(!(time - 3 <= 0))
        {
            time -= 3;
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

        Instantiate(gameOverFloor, pos, Quaternion.identity);
    }
}
