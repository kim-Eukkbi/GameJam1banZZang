using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentMove : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private bool isOnRid = false;
    private float distance = 0;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.instance.CanMerge)
        {
            if (!isOnRid)
            {
                Rigidbody.useGravity = false;
                distance = CalDistance();
                Destroy(this.gameObject, 3);
            }
                
            transform.position = Vector3.Lerp(transform.position, GameManager.instance.player.transform.position, Time.deltaTime * (distance / 100));
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * (distance / 100));
            gameObject.layer = 6;

            
        }
    }

    float CalDistance()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
        return distance;
    }
}

