using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject plate;
    [SerializeField]
    private Plate lastPlate;
    private const int interval = 40;

    private void Start()
    {
        PoolManager.CreatPool<Plate>(plate, gameObject.transform, 5);

        lastPlate = PoolManager.GetItem<Plate>();

        lastPlate.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        lastPlate.Setup();

        for (int i = 0; i < 2; i++)
        {
            Plate temp = PoolManager.GetItem<Plate>();

            temp.Setup();

            temp.transform.position = new Vector3(lastPlate.transform.position.x,
                                              lastPlate.transform.position.y - interval,
                                              lastPlate.transform.position.z);
            lastPlate = temp;
        }
    }

    public void MakePizzaPlate()
    {
        Plate temp = PoolManager.GetItem<Plate>();

        temp.Setup();

        temp.transform.position = new Vector3(lastPlate.transform.position.x,
                                          lastPlate.transform.position.y - interval,
                                          lastPlate.transform.position.z);
        lastPlate = temp;
    }
}
