using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    private PizzaType type;

    public PizzaType Type { get; set; }

    public void ExplosionPizza()
    {
        //this.gameObject.GetComponent<Rigidbody>().AddExplosionForce(500, new Vector3(0, 0, 0), 10,0,ForceMode.Impulse);

    }
}
