using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    private Text textScore;
    [SerializeField]
    private PizzaSpawner pizzaSpawner;

    [SerializeField]
    private GameObject gameOverFloor;
    [SerializeField]
    private GameObject fragments;

    [SerializeField]
    private GameObject cylinder;

    [SerializeField]
    private AudioSource startAudio;
    [SerializeField]
    private AudioSource inGameAudio;

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

    private void Start()
    {
        textTimerViewer.TimerEnable(false);
        player.speedTMP.enabled = false;
        player.GetComponent<MeshRenderer>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        cylinder.SetActive(false);
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

    public void GameStart()
    {
        textTimerViewer.StartTimer();
        textTimerViewer.TimerEnable(true);
        player.speedTMP.enabled = true;
        player.GetComponent<MeshRenderer>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        cylinder.SetActive(true);
        pizzaSpawner.SpawnPlate();

        startAudio.Stop();
        inGameAudio.Play();
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

        StartCoroutine(InstantiateFragments(pos2));
        StartCoroutine(CamaraMove());
    }

    public IEnumerator InstantiateFragments(Vector3 insPos)
    {
        yield return new WaitForSeconds(4f);
        for(int i = 0; i < destroyedPlate; i++)
        {
            Instantiate(fragments, insPos, Quaternion.identity);
            yield return new WaitForSeconds(.05f);
        }
           
    }

    public IEnumerator CamaraMove()
    {
        yield return new WaitForSeconds(5f);
        player.vCams[0].gameObject.SetActive(false);
        player.vCams[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        player.vCams[1].gameObject.SetActive(false);
        player.vCams[2].gameObject.SetActive(true);

        textScore.text = string.Concat("SCORE\n", destroyedPlate * 300);

        textTimerViewer.TimerEnable(false);
        textScore.gameObject.SetActive(true);
        //여기다가 V Cam 바꾸면 될듯;
    }
}
