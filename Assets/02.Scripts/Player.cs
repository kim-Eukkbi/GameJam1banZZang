using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Linq;
using UnityEngineInternal;
using Cinemachine;

public class Player : MonoBehaviour
{
    private MeshRenderer playerMesh;
    private TrailRenderer playerTrail;
    private Rigidbody playerRigidbody;
    private ConstantForce playerConstantForce;

   // public CinemachineBrain cinemachineBrain
    public TextMeshPro speedTMP;
    public List<CinemachineVirtualCamera> vCams;
    public List<Material> colorMat;
    private RaycastHit hit;
    private PizzaType playerType;
    private Vector3 tMPPos;
    private bool isShaking = false;

    void Start()
    {
        vCams[0].gameObject.SetActive(true);
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

        if (!isShaking)
        {
            isShaking = true;
            speedTMP.transform.DOShakePosition(1, 5).OnComplete(() => isShaking = false);
        }
        speedTMP.text = string.Format("{0:0}\n<size=50%>km/h</size>", Valocity);
        //speedTMP.
        speedTMP.fontSize = Valocity > 120 ? 500 : Valocity * 2f + 300;
        speedTMP.rectTransform.position += new Vector3(0, -(Valocity / 500), Valocity / 300);
    


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


        Debug.DrawRay(transform.position, Vector3.down, Color.blue, 10 + playerRigidbody.velocity.y / 100);
        if(Physics.Raycast(transform.position,Vector3.down,out hit, 10 + playerRigidbody.velocity.y / 100))
        {
            if(hit.transform.GetComponent<Pizza>() != null)
            {
                if (hit.transform.GetComponent<Pizza>().Type.Equals(playerType))
                {
                    //print("Ãæµ¹");
                    Plate currPlate = hit.transform.gameObject.GetComponentInParent<Plate>();
                    GameManager.DestroyPlate();
                    GameManager.instance.canMiss = true;
                    StartCoroutine(DestroyList(currPlate));
                }
                else
                {
                    GameManager.instance.MissPlate();
                    GameManager.instance.canMiss = false;
                }
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
