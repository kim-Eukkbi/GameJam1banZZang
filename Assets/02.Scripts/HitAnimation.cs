using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitAnimation : MonoBehaviour
{
    [SerializeField]
    private Image redImage;

    public void StartFade()
    {
        StartCoroutine(FadeRed());
    }

    private IEnumerator FadeRed()
    {
        Color temp = redImage.color;

        temp.a = 0.4f;
        redImage.color = temp;

        while(redImage.color.a > 0)
        {
            yield return new WaitForSeconds(0.01f);

            temp.a -= 0.05f;
            redImage.color = temp;
        }
    }
}
