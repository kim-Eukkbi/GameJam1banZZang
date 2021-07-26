using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject plate;

    private const int interval = 40;

    private void Start()
    {
        PoolManager.CreatPool<Plate>(plate, gameObject.transform, 50);

        for (int i = 0; i < 50; i++)
        {
            Plate temp = PoolManager.GetItem<Plate>();

            temp.transform.position = new Vector3(transform.position.x, transform.position.y - interval * i, transform.position.z);
            temp.Setup();
        }
    }
}
