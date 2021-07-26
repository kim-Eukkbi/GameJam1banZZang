using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    [SerializeField]
    private PizzaType type;

    public PizzaType Type { get; set; }
}
