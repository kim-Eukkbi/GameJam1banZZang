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

    public TextMeshPro speedTMP;
    public List<CinemachineVirtualCamera> vCams;
    public List<Material> colorMat;
    private RaycastHit hit;
    private PizzaType playerType;
    public Vector3 tMPPos;
    private bool isShaking = false;
    public bool isDoubleChack = false;

    void Start()
    {
        //vCams[0].gameObject.SetActive(true);
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

        if (GameManager.instance.topSpeed < Valocity)
        {
            GameManager.instance.topSpeed = (int)Valocity;
        }

        if (!isShaking)
        {
            isShaking = true;
            speedTMP.transform.DOShakePosition(1, Valocity / 20).OnComplete(() => isShaking = false);
        }
        speedTMP.text = string.Format("{0:0}\n<size=50%>km/h</size>", Valocity);
        float colorR = Mathf.Clamp(Valocity / 100, 0.17f, 1);
        float colorG = Mathf.Clamp(Valocity / 100, 0.19f, 1);
        float colorB = Mathf.Clamp(Valocity / 100, 0.43f, 1);
        speedTMP.color = new Color(colorR, colorG, colorB);
        speedTMP.fontSize = Valocity > 120 ? 500 : Valocity * 2f + 300;
        //speedTMP.rectTransform.position += new Vector3(0, -(Valocity / 500), Valocity / 300);


        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.instance.isGameOver)
            {
                if (playerType.Equals(PizzaType.GREEN))
                    playerType = PizzaType.RED;
                else
                    playerType = PizzaType.GREEN;

                playerMesh.material = colorMat[(int)playerType];
            }
        }


        if (Physics.Raycast(transform.position, Vector3.down, out hit, 6 + Valocity / 70))
        {
            if (hit.transform.GetComponent<Pizza>() != null)
            {
                GameManager.instance.canCheck = true;

                if(GameManager.instance.combo % 4 == 0 && GameManager.instance.combo != 0)
                {
                    Plate currPlate = hit.transform.gameObject.GetComponentInParent<Plate>();
                    if(!currPlate.isChecking)
                    {
                        GameManager.instance.canMiss = true;
                        isDoubleChack = true;
                        StartCoroutine(DestroyList(currPlate));
                        currPlate.isChecking = true;

                        GameManager.instance.combo++;
                    }
                }
                else
                {
                    if (hit.transform.GetComponent<Pizza>().Type.Equals(playerType))
                    {
                        Plate currPlate = hit.transform.gameObject.GetComponentInParent<Plate>();
                        if(!currPlate.isChecking)
                        {
                            GameManager.instance.canMiss = true;
                            isDoubleChack = true;
                            StartCoroutine(DestroyList(currPlate));
                            currPlate.isChecking = true;

                            GameManager.instance.combo++;
                        }
                    }
                    else
                    {
                        GameManager.instance.MissPlate();
                        GameManager.instance.canMiss = false;

                        GameManager.instance.combo = 0;
                    }
                }
                GameManager.instance.textCombo.text = GameManager.instance.combo + "\n<size=200>combo</size>";
            }
        }
    }


    private IEnumerator DestroyList(Plate plate)
    {
        GameManager.instance.DestroyPlate();


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
