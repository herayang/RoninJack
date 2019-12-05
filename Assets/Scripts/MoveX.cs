using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Xvalue;
using UnityEngine.UI;

public class MoveX : MonoBehaviour
{
    [SerializeField] private RectTransform rawImage = null;
    private float facePosX;
    private Player player;

    private void Start()
    {
        facePosX = getFacePosX();
        player = gameObject.GetComponent<Player>();
        if(player == null)
        {
            Debug.Log("Not player Object that has MoveX script.");
        }
    }

    void FixedUpdate()
    {
        if (StaticData.xValue != 0)
        {
            moveHorizontally();
        }
    }

    private void moveHorizontally()
    {
        facePosX = getFacePosX();
        //Debug.Log("T Pos: " + transform.localPosition.x + " | F Pos: " + facePosX);

        if ((transform.localPosition.x < facePosX + 0.5f) && (transform.localPosition.x > facePosX - 0.5f))
        {
            return;
        }
        else if (transform.localPosition.x > facePosX)
        {
            transform.localPosition += Vector3.left * player.moveSpeed * Time.fixedDeltaTime;
        }
        else if (transform.localPosition.x < facePosX)
        {
            transform.localPosition += Vector3.right * player.moveSpeed * Time.fixedDeltaTime;
        }
    }

    private float getFacePosX()
    {
        return (StaticData.xValue - (rawImage.rect.width / 2)) / ((rawImage.rect.width / 2) / 10);
    }
}
