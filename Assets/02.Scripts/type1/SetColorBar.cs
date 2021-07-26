using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorBar : MonoBehaviour
{
    public int barColor;
    private SpriteRenderer sp;

    void Start()
    {
        barColor = Random.Range(0, 2);

        sp = GetComponent<SpriteRenderer>();

        switch(barColor)
        {
            case 0:
                sp.color = Color.red;
                break;
            case 1:
                sp.color = Color.green;
                break;
        }
    }
}
