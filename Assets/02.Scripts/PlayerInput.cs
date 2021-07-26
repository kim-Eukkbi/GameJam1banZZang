using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

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
                //print("충돌");
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
        List<Collider> col = Physics.OverlapSphere(new Vector3(0, 0, 0), 10).ToList();
        print(col.Count);
        /*foreach (Collider hit in col)
        {
            Rigidbody hitRig = hit.gameObject.GetComponent<Rigidbody>();
            if(hitRig != null)
            {
                print("폭*8");
                hitRig.AddExplosionForce(500, new Vector3(0, 0, 0), 10, 0, ForceMode.Impulse);
            }
        }*/

        for (int i = 0; i < plate.pizzas.Count; i++)
        {
            Rigidbody rigidbody = plate.pizzas[i].transform.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
           // plate.pizzas[i].GetComponent<Pizza>().ExplosionPizza();
            Destroy(plate.pizzas[i].gameObject, 1f);
        }

        col.Clear();

        yield return new WaitForSeconds(1f);


        for (int i = 0; i < plate.pizzas.Count; i++)
        {
             plate.pizzas.RemoveAt(i);
        }
    }
}
