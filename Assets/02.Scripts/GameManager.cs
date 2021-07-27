using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    [SerializeField]
    private TextTimerViewer textTimerViewer;
    [SerializeField]
    private Text textScore;
    [SerializeField]
    private PizzaSpawner pizzaSpawner;

    [SerializeField]
    private GameObject gameOverFloor;
    [SerializeField]
    private GameObject fragments;

    [SerializeField]
    private int minusTime = 2;

    public int time = 60;
    public int destroyedPlate = 0;

    public bool canMiss = true;
    public bool CanMerge = false;

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
        textTimerViewer.MissTimer();
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
        Vector3 pos2 = new Vector3(player.transform.position.x + 12.5f, player.transform.position.y, player.transform.position.z);

        Instantiate(gameOverFloor, pos, Quaternion.identity);

        StartCoroutine(FragmentsMove(pos2));
    }



    public IEnumerator FragmentsMove(Vector3 insPos)
    {
        Sequence sequence = DOTween.Sequence();
        yield return new WaitForSeconds(4f);
        for(int i = 0; i < destroyedPlate; i++)
        {
            Instantiate(fragments, insPos, Quaternion.identity);
            yield return new WaitForSeconds(.05f);
        }

        yield return new WaitForSeconds(5f);
        CanMerge = true;
        print(10 + destroyedPlate / 5);
        sequence.Append(player.transform.DOScale(new Vector3((10 + destroyedPlate) * 3, (10 + destroyedPlate) * 3, (10 + destroyedPlate) * 3),destroyedPlate / 50));
        sequence.Join(player.transform.DOMoveY(player.transform.position.y + ((10 + destroyedPlate) * 3), destroyedPlate / 50));
        yield return new WaitForSeconds(destroyedPlate / 50);
        //player.vCams[2].
    }

    public IEnumerator CamaraMove()
    {
        player.vCams[0].gameObject.SetActive(false);
        player.vCams[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(7f);
        player.vCams[1].gameObject.SetActive(false);
        player.vCams[2].gameObject.SetActive(true);

        textScore.text = string.Concat("SCORE\n", destroyedPlate * 500);

        textTimerViewer.TimerEnable(false);
        textScore.gameObject.SetActive(true);
        //여기다가 V Cam 바꾸면 될듯;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(player.transform.position, 300);
        Gizmos.color = Color.blue;
    }
}
