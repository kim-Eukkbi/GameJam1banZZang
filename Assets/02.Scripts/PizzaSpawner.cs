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
        for (int i = 0; i < plates.Count; i++)
        {
            if (plates[i] != null)
            {
                Destroy(plates[i].gameObject);
            }
        }

        for (int i = 0; i < 500; i++)
        {
            Plate temp = Instantiate(plate, transform).GetComponent<Plate>();

            temp.Setup();

            temp.transform.position = new Vector3(transform.position.x,transform.position.y - interval * i,transform.position.z);
            plates.Add(temp);
        }
    }
}
