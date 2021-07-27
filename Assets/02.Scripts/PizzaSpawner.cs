using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject plate;
    public List<Plate> plates = new List<Plate>();
    private const int interval = 40;

    public void SpawnPlate()
    {
        PoolManager.CreatPool<Plate>(plate, gameObject.transform, 500);

        for (int i = 0; i < 500; i++)
        {
            Plate temp = PoolManager.GetItem<Plate>();

            temp.Setup();

            temp.transform.position = new Vector3(transform.position.x,transform.position.y - interval * i,transform.position.z);
            plates.Add(temp);
        }
    }
}
