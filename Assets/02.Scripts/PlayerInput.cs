using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private MeshRenderer playerMesh;
    private TrailRenderer playerTrail;
    private Rigidbody playerRigidbody;
    private ConstantForce playerConstantForce;

    public List<Material> colorMat;
    private int colorindex;
    private RaycastHit hit;
    private PizzaType playerType;

    void Start()
    {
        playerType = PizzaType.RED;
        playerMesh = GetComponent<MeshRenderer>();
        playerTrail = GetComponent<TrailRenderer>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerConstantForce = GetComponent<ConstantForce>();
        playerMesh.material = colorMat[(int)playerType];
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (playerType.Equals(PizzaType.GREEN))
                playerType = PizzaType.RED;
            else
                playerType = PizzaType.GREEN;

            playerMesh.material = colorMat[(int)playerType];
           /* playerTrail.startColor = colorMat[colorindex].color;
            playerTrail.endColor = colorMat[colorindex].color;*/
        }

        if (Input.GetMouseButtonDown(1))
        {
            playerRigidbody.AddForce(Vector3.down * 10, ForceMode.VelocityChange);
        }

        //이거 핵임 쓰면 편함
      /*  if(Input.GetKeyDown(KeyCode.E))
        {
            playerConstantForce.force = new Vector3(0, -10000, 0);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            playerConstantForce.force = new Vector3(0, 0, 0);
        }*/

        Debug.DrawRay(transform.position, Vector3.down, Color.blue, 5);
        if(Physics.Raycast(transform.position,Vector3.down,out hit,5))
        {
            if(hit.transform.GetComponent<Pizza>().Type.Equals(playerType))
            {
                print("충돌");
                Plate currPlate = hit.transform.gameObject.GetComponentInParent<Plate>();
                for (int i =0;i < currPlate.pizzas.Count;i++)
                {
                    Destroy(currPlate.pizzas[i].gameObject);
                }

                for (int i = 0; i < currPlate.pizzas.Count; i++)
                {
                    currPlate.pizzas.RemoveAt(i);
                }

            }
            else
            {

            }
        }
    }
}
