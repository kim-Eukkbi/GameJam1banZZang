using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private MeshRenderer playerMesh;
    private TrailRenderer playerTrail;
    private Rigidbody playerRigidbody;

    public List<Material> colorMat;
    private int colorindex;

    void Start()
    {
        colorindex = 0;
        playerMesh = GetComponent<MeshRenderer>();
        playerTrail = GetComponent<TrailRenderer>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            colorindex = (colorindex + 1) % 2;
            playerMesh.material = colorMat[colorindex];
           /* playerTrail.startColor = colorMat[colorindex].color;
            playerTrail.endColor = colorMat[colorindex].color;*/
        }

        if (Input.GetMouseButtonDown(1))
        {
            playerRigidbody.AddForce(Vector3.down * 10, ForceMode.VelocityChange);
        }
    }
}
