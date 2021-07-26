using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
                StartCoroutine(DestroyList(currPlate));
            }
            else
            {

            }
        }
    }

    private IEnumerator DestroyList(Plate plate)
    {
        for (int i = 0; i < plate.pizzas.Count; i++)
        {
            Rigidbody rigidbody = plate.pizzas[i].transform.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            //rigidbody.AddForce(Vector3.back * 10, ForceMode.Impulse);
            rigidbody.AddExplosionForce(500, new Vector3(0, 0, 0), 10,10);
            //Destroy(plate.pizzas[i].gameObject, 1f);
        }

        yield return new WaitForSeconds(2f);
        plate.SetOff();
        /* for (int i = 0; i < plate.pizzas.Count; i++)
         {
             //plate.pizzas.RemoveAt(i);
         }
 */

    }
}
