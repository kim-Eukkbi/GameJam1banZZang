using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Plate : MonoBehaviour
{
    [SerializeField]
    private GameObject pizza;
    public List<Pizza> pizzas = new List<Pizza>();
    private RotateDir rotateDir;
    [SerializeField]
    private float rotateSpeed = 1f;

    private const int each = 45;

    public void Setup()
    {
        for (int i = 0; i < 8; i++)
        {
            Pizza temp = Instantiate(pizza, transform).GetComponent<Pizza>();
            pizzas.Add(temp);
        }

        int randomNum = Random.Range(0, 7);

        rotateDir = (RotateDir)PizzaManager.instance.platePattern.rotatePattern[randomNum];

        for (int i = 0; i < pizzas.Count; i++)
        {
            MeshRenderer temp = pizzas[i].GetComponent<MeshRenderer>();

            pizzas[i].Type = (PizzaType)PizzaManager.instance.platePattern.patterns[randomNum, i];

            temp.material = PizzaManager.instance.materials[PizzaManager.instance.platePattern.patterns[randomNum, i]];
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

    private void Update()
    {
        if(rotateDir == RotateDir.LEFT)
        {
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
        }
        else if (rotateDir == RotateDir.RIGHT)
        {
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
    }
}
