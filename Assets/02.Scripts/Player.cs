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
    [SerializeField]
    private HitAnimation hitAnimation;

    public TextMeshPro speedTMP;
    public List<CinemachineVirtualCamera> vCams;
    public List<Material> colorMat;
    private RaycastHit hit;
    private PizzaType playerType;
    public Vector3 tMPPos;
    private bool isShaking = false;
    public bool isDoubleChack = false;
    public bool isfadeEnd = false;

    void Start()
    {
        speedTMP.gameObject.transform.localScale = Vector3.zero;
        GameManager.instance.textCombo.gameObject.transform.localScale = Vector3.zero;
        StartCoroutine(Fading());

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
        GameManager.instance.textCombo.color = new Color(colorR, colorG, colorB);
        GameManager.instance.textCombo.text = 
            string.Format("{0:0}\n<size=50%>combo</size>", GameManager.instance.combo);
        GameManager.instance.textCombo.fontSize = (Valocity > 120 ? 300 : Valocity * 2f);
        GameManager.instance.textCombo.fontSize = Mathf.Clamp(GameManager.instance.textCombo.fontSize, 150, 300);

        //speedTMP.rectTransform.position += new Vector3(0, -(Valocity / 500), Valocity / 300);

        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.instance.canChangeColor)
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

                if (hit.transform.GetComponent<Pizza>().Type.Equals(playerType))
                {
                    Plate currPlate = hit.transform.gameObject.GetComponentInParent<Plate>();
                    if (!currPlate.isChecking)
                    {
                        isDoubleChack = true;
                        StartCoroutine(DestroyList(currPlate));
                        currPlate.isChecking = true;

                        GameManager.instance.PlaySoundEffect();
                        GameManager.instance.combo++;
                        GameManager.instance.textCombo.transform.DOShakePosition(1, 5f);
                    }
                }
                else
                {
                    Plate currPlate = hit.transform.gameObject.GetComponentInParent<Plate>();
                    if (!currPlate.isMissed)
                    {
                        GameManager.instance.MissPlate();
                        hitAnimation.StartFade();
                        currPlate.isMissed = true;

                        GameManager.instance.combo = 0;
                    }
                }

                GameManager.instance.textCombo.text = GameManager.instance.combo + "\n<size=50%>combo</size>";
            }
        }

        GameManager.instance.DestroyUpPlate();
    }


    private IEnumerator DestroyList(Plate plate)
    {
        GameManager.instance.DestroyPlate();
        plate.DestroyPizza();

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator Fading()
    {
        yield return new WaitForSeconds(1.5f);
        speedTMP.gameObject.transform.DOScale(new Vector3(1.4647f, 1.3907f, 1), .5f);
        GameManager.instance.textCombo.gameObject.transform.DOScale(new Vector3(1.4647f, 1.3907f, 1), .5f);
    }
}
