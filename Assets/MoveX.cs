using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Xvalue;

public class MoveX : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (StaticData.xValue != 0)
        {
            transform.localPosition = new Vector3(((StaticData.xValue-650) / 65), 0, 10);
        }
    }
}
