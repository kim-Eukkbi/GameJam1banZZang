using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCol : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bar"))
        {
            GameManager.instance.ChackPlayerAndBar(GetComponent<PlayerINput>().colorIndex);
        }
    }
}
