using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using Cinemachine;
using TMPro;
using System.Security.Cryptography;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    private DataManager dataManager;
    [SerializeField]
    private TextTimerViewer textTimerViewer;
    [SerializeField]
    private Text scoreNum;
    [SerializeField]
    private Text textScore;
    [SerializeField]
    private PizzaSpawner pizzaSpawner;
    [SerializeField]
    private GameObject panelGameEnd;
    [SerializeField]
    private GameObject panelGameEndInvisible;
    [SerializeField]
    private TextMeshPro textTopScore;
    [SerializeField]
    private TextMeshPro textTopspeed;

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

    [SerializeField]
    private Vector3 playerOriginPos;
    [SerializeField]
    private Vector3 playerOriginScale;

    private GameObject floor;
   [SerializeField]
    private List<GameObject> fragmentList;

    private Sequence sequence;

    private int maxTime = 60;
    public int time = 60;
    public int destroyedPlate = 0;

    public bool canMiss = true;
    public bool CanMerge = false;
    public bool isGameOver = false;

    public bool isTest = true;

    [SerializeField]
    public int topScore;
    public int topSpeed;

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

        dataManager = GetComponent<DataManager>();
        dataManager.path = Application.persistentDataPath + "/" + "Top.txt";
    }

    private void Start()
    {
        if(!File.Exists(dataManager.path))
        {
            dataManager.SaveData(new GameDataVO(0, 0));
        }

        GameDataVO vo = dataManager.LoadData();

        topScore = vo.highScore;

        if (isTest)
        {
            textTimerViewer.StartTimer();
            pizzaSpawner.SpawnPlate();

            startAudio.Stop();
            inGameAudio.Play();
        }
        else
        {
            textTimerViewer.TimerEnable(false);
            player.speedTMP.enabled = false;
            player.GetComponent<MeshRenderer>().enabled = false;
            player.GetComponent<Rigidbody>().isKinematic = true;
            cylinder.SetActive(false);
        }
    }

    public void DestroyPlate()
    {
        destroyedPlate++;
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

    public void ReStart()
    {
        StopAllCoroutines();

        sequence.Kill();
        DOTween.KillAll();

        if(floor != null)
        {
            Destroy(floor);
        }

        float fragCount = fragmentList.Count;

        for (int i = 0; i < fragCount; i++)
        {
            Destroy(fragmentList[i].gameObject);
        }

        fragmentList.Clear();

        isGameOver = false;
        CanMerge = false;

        player.vCams[3].gameObject.SetActive(false);
        player.vCams[0].gameObject.SetActive(true);

        player.transform.position = playerOriginPos;
        player.transform.localScale = playerOriginScale;
       

        time = maxTime;

        player.speedTMP.gameObject.SetActive(true);

        cylinder.SetActive(true);

        inGameAudio.Stop();
        inGameAudio.Play();

        textTimerViewer.TimerEnable(true);
        scoreNum.text = string.Empty;
        textScore.gameObject.SetActive(false);

        panelGameEnd.SetActive(false);
        panelGameEndInvisible.SetActive(false);

        textTimerViewer.StartTimer();
        pizzaSpawner.SpawnPlate();

        destroyedPlate = 0;
    }

    public void GameOver()
    {
        isGameOver = true;
        cylinder.SetActive(false);

        for (int i = 0; i < pizzaSpawner.plates.Count; i++)
        {
            if (pizzaSpawner.plates[i] != null)
            {
                Destroy(pizzaSpawner.plates[i].gameObject);
            }
        }

        for (int i = 0; i < pizzaSpawner.plates.Count; i++)
        {
            pizzaSpawner.plates.RemoveAt(i);
        }

        player.speedTMP.gameObject.SetActive(false);

        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y - 500, player.transform.position.z);
        Vector3 pos2 = new Vector3(player.transform.position.x + 12.5f, player.transform.position.y, player.transform.position.z);

        floor = Instantiate(gameOverFloor, pos, Quaternion.identity);

        StartCoroutine(FragmentsMove(pos2));

        if(topScore < destroyedPlate)
        {
            topScore = destroyedPlate;
        }

        GameDataVO vo = new GameDataVO(topScore);

        dataManager.SaveData(vo);
    }



    public IEnumerator FragmentsMove(Vector3 insPos)
    {
        sequence = DOTween.Sequence();
        yield return new WaitForSeconds(4f);
        for (int i = 0; i < destroyedPlate; i++)
        {
            GameObject temp = Instantiate(fragments, insPos, Quaternion.identity);
            fragmentList.Add(temp);
            yield return new WaitForSeconds(.05f);
        }
        yield return new WaitForSeconds(5f);
        CanMerge = true;

        float scaleSize = Mathf.Clamp(destroyedPlate * 3, 3, 150);
        sequence.Append(player.transform.DOScale(new Vector3(scaleSize, scaleSize, scaleSize),2f));
        sequence.Join(player.transform.DOMoveY(player.transform.position.y + destroyedPlate, 2f));
        sequence.Append(scoreNum.DOCounter(0, destroyedPlate, 1f));
        player.vCams[2].gameObject.SetActive(false);
        player.vCams[3].gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        GameDataVO vo = dataManager.LoadData();

        topScore = vo.highScore;
        topSpeed = vo.highSpeed;

        textTopScore.text = "Top Score\n\n<size=900>"+ topScore +"</size>\n<size=500>floor</size>";

        panelGameEnd.SetActive(true);
        panelGameEndInvisible.SetActive(true);
    }

    public IEnumerator CamaraMove()
    {
        player.vCams[0].gameObject.SetActive(false);
        player.vCams[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(7f);
        player.vCams[1].gameObject.SetActive(false);
        player.vCams[2].gameObject.SetActive(true);

        textTimerViewer.TimerEnable(false);
        textScore.gameObject.SetActive(true);
    }
}
