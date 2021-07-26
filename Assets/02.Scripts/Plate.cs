using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Plate : MonoBehaviour
{
    [SerializeField]
    private GameObject pizza;
    private List<Pizza> pizzas = new List<Pizza>();

    private const int each = 45;

    private void Start()
    {
        PoolManager.CreatPool<Pizza>(pizza, gameObject.transform, 8);

        for (int i = 0; i < 8; i++)
        {
            pizzas.Add(PoolManager.GetItem<Pizza>());
        }

        for (int i = 0; i < pizzas.Count; i++)
        {
            MeshRenderer temp = pizzas[i].GetComponent<MeshRenderer>();

            int randomNum = Random.Range(0, 3);

            temp.material = PizzaManager.instance.materials[randomNum];
            pizzas[i].Type = PizzaManager.instance.types[randomNum];
        }

        for (int i = 0; i < pizzas.Count; i++)
        {
            pizzas[i].transform.rotation = Quaternion.Euler(pizzas[i].transform.rotation.x
                                                          , pizzas[i].transform.rotation.y + each * i, 
                                                            pizzas[i].transform.rotation.z);
        }
    }
}
