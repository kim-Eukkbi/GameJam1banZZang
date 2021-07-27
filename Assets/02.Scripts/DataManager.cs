using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public string path;

    public void SaveData(GameDataVO vo)
    {
        string json = JsonUtility.ToJson(vo);
        File.WriteAllText(path, json);
    }

    public GameDataVO LoadData()
    {
        string json = File.ReadAllText(path);
        GameDataVO vo = JsonUtility.FromJson<GameDataVO>(json);

        return vo;
    }
}
