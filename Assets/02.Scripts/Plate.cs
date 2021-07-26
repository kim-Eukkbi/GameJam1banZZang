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

    public void Setup()
    {
        for (int i = 0; i < 8; i++)
        {
            Pizza temp = Instantiate(pizza, transform).GetComponent<Pizza>();
            pizzas.Add(temp);
        }

        int randomNum = Random.Range(0, 7);

        for (int i = 0; i < pizzas.Count; i++)
        {
            MeshRenderer temp = pizzas[i].GetComponent<MeshRenderer>();

            temp.material = PizzaManager.instance.materials[PizzaManager.instance.platePattern.patterns[randomNum, i]];
            pizzas[i].Type = (PizzaType)PizzaManager.instance.platePattern.patterns[randomNum, i];
        }

        for (int i = 0; i < pizzas.Count; i++)
        {
            pizzas[i].transform.position = transform.position;
        }

        for (int i = 0; i < pizzas.Count; i++)
        {
            pizzas[i].transform.rotation = Quaternion.Euler(pizzas[i].transform.rotation.x
                                                          , pizzas[i].transform.rotation.y + each * i,
                                                            pizzas[i].transform.rotation.z);
        }
    }
}
