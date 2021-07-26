using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PizzaType { DEFAULT, RED, GREEN }

public class PizzaManager : MonoBehaviour
{
    public static PizzaManager instance;

    public Material[] materials;
    public PizzaType[] types = new PizzaType[] { PizzaType.DEFAULT, PizzaType.GREEN, PizzaType.RED };

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
    }
}
