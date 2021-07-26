using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Linq;

public class PlayerInput : MonoBehaviour
{
    private MeshRenderer playerMesh;
    private TrailRenderer playerTrail;
    private Rigidbody playerRigidbody;
    private ConstantForce playerConstantForce;

    public TextMeshPro speedTMP;
    public List<Material> colorMat;
    private int colorindex;
    private RaycastHit hit;
    private PizzaType playerType;
    private Vector3 tMPPos;
    private bool isShaking = false;

    void Start()
    {
        playerType = PizzaType.RED;
        playerMesh = GetComponent<MeshRenderer>();
        playerTrail = GetComponent<TrailRenderer>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerConstantForce = GetComponent<ConstantForce>();
        playerMesh.material = colorMat[(int)playerType];
        tMPPos = speedTMP.rectTransform.position;
    }

    void Update()
    {
        float Valocity = Mathf.Abs(playerRigidbody.velocity.y);

        if (Valocity > 200)
        {
            if (!isShaking)
            {
                isShaking = true;
                speedTMP.transform.DOShakePosition(1, 5).OnComplete(() => isShaking = false);
            }
            speedTMP.text = string.Format("{0:0}", Valocity);
            speedTMP.fontSize = Valocity > 800 ? 800 : Valocity * 2f;
            speedTMP.rectTransform.position += new Vector3(0,  -(Valocity / 500), Valocity / 300);
        }
        else
        {
            speedTMP.text = " ";
            speedTMP.fontSize = 400;
            speedTMP.rectTransform.position = new Vector3(tMPPos.x, tMPPos.y + Camera.main.transform.position.y - 150, tMPPos.z);
        }



        

        if (Input.GetMouseButtonDown(0))
        {
            if (playerType.Equals(PizzaType.GREEN))
                playerType = PizzaType.RED;
            else
                playerType = PizzaType.GREEN;

            playerMesh.material = colorMat[(int)playerType];
           /* playerTrail.startColor = colorMat[colorindex].color;
            playerTrail.endColor = colorMat[colorindex].color;*/
        }


        Debug.DrawRay(transform.position, Vector3.down, Color.blue, 15 + playerRigidbody.velocity.y / 100);
        if(Physics.Raycast(transform.position,Vector3.down,out hit, 15 + playerRigidbody.velocity.y / 100))
        {
            if(hit.transform.GetComponent<Pizza>().Type.Equals(playerType))
            {
                //print("Ãæµ¹");
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
            rigidbody.useGravity = true;
            plate.pizzas[i].gameObject.layer = 7;
            rigidbody.AddExplosionForce(100, transform.position, 10, 10, ForceMode.Impulse);
            Destroy(plate.pizzas[i].gameObject, 1f);
        }


        yield return new WaitForSeconds(1f);


        for (int i = 0; i < plate.pizzas.Count; i++)
        {
             plate.pizzas.RemoveAt(i);
        }
    }
}
