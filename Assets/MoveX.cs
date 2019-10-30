using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Xvalue;
using UnityEngine.UI;

public class MoveX : MonoBehaviour
{
    public RectTransform rawImage;

    // Update is called once per frame
    void Update()
    {
        if (StaticData.xValue != 0)
        {
            transform.localPosition = new Vector3(
                ((StaticData.xValue-(rawImage.rect.width/2)) / ((rawImage.rect.width/2)/10)),
                transform.localPosition.y,
                transform.localPosition.z);
        }
    }
}
