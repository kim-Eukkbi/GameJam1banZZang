using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject plate;
    [SerializeField]
    private Plate lastPlate;
    private const int interval = 60;

    private void Start()
    {
        PoolManager.CreatPool<Plate>(plate, gameObject.transform, 500);

/*        lastPlate = PoolManager.GetItem<Plate>();

        lastPlate.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        lastPlate.Setup();
*/
        for (int i = 0; i < 500; i++)
        {
            Plate temp = PoolManager.GetItem<Plate>();

            temp.Setup();

            temp.transform.position = new Vector3(transform.position.x,transform.position.y - interval * i,transform.position.z);
            lastPlate = temp;
        }
    }
}
