using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerINput : MonoBehaviour
{
    public int colorIndex;
    private SpriteRenderer renderer;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        colorIndex = 0;
        renderer.color = Color.red;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            colorIndex = (colorIndex + 1) % 2;
            ChangeColor();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.isPowerUp = true;
            rigidbody.gravityScale = 10f;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            rigidbody.gravityScale = 1f;
            GameManager.instance.isPowerUp = false;
        }
    }

    private void ChangeColor()
    {
        switch (colorIndex)
        {
            case 0:
                renderer.color = Color.red;
                break;
            case 1:
                renderer.color = Color.green;
                break;
        }
    }
}
