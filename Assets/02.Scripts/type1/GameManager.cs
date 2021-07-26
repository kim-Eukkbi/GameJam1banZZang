using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PhysicsMaterial2D physicsMaterial;
    public List<SetColorBar> bars;
    public bool isPowerUp = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        physicsMaterial.bounciness = 0;
    }

    public void ChackPlayerAndBar(int playerColorIndex)
    {
        if (playerColorIndex.Equals(bars[0].barColor))
        {
            physicsMaterial.bounciness = 0;
            Destroy(bars[0].gameObject);
            bars.RemoveAt(0);
        }
        else
        {
            if(isPowerUp)
                physicsMaterial.bounciness = 10f;
            else
                physicsMaterial.bounciness = .2f;
        }
    }
}
