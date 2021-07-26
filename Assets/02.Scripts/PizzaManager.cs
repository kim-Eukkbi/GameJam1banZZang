using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PizzaType { RED, GREEN }
public enum RotateDir { LEFT, RIGHT }

public class PizzaManager : MonoBehaviour
{
    public static PizzaManager instance;

    public Material[] materials;
    public PizzaType[] types = new PizzaType[] { PizzaType.RED, PizzaType.GREEN };
    public PlatePattern platePattern = new PlatePattern();

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
