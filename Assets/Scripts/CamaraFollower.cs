using System;
using System.Linq;
using System.Collections;
using UnityEngine;

public class CamaraFollower : MonoBehaviour
{
    [SerializeField] private Transform camara = null;
    private Player player;
    private float moveSpeed;
    private float camaraOffSet;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float targetRotation;
    [SerializeField] private float startRotation;

    public void SetUp(Player p, float speed)
    {
        player = p;
        moveSpeed = speed;
        camaraOffSet = camara.localPosition.y;
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.forward * moveSpeed * Time.fixedDeltaTime, Space.World);

        if(rotationSpeed != 0)
        {
            float rotationStart = GetYRotation();
            Debug.Log(rotationStart);
            //transform.Rotate(Vector3.up * (rotationSpeed * moveSpeed * Time.fixedDeltaTime), Space.World);

            transform.RotateAround(transform.position, Vector3.up, rotationSpeed * moveSpeed * Time.fixedDeltaTime);

            float rotation = GetYRotation();
            if (rotationSpeed > 0 && rotation + (rotationSpeed * moveSpeed * Time.fixedDeltaTime) > targetRotation)
            {
                rotationSpeed = 0;
                SetRotation();
            }
            else if (rotationSpeed < 0 && rotation + (rotationSpeed * moveSpeed * Time.fixedDeltaTime) < targetRotation)
            {
                rotationSpeed = 0;
                SetRotation();
            }
        }
    }

    public void UpdateCamara(float playerY)
    {
        camara.position = new Vector3(camara.position.x, playerY + camaraOffSet, camara.position.z);
    }

    public void Turn(float target, float speed)
    {
        rotationSpeed = speed;
        targetRotation = target;
        startRotation = GetYRotation();
        if (targetRotation < startRotation)
        {
            rotationSpeed = rotationSpeed * -1;
        }
    }

    public float GetYRotation()
    {
        Vector3 angle = transform.eulerAngles;
        float y = angle.y;
        if (angle.y > 180)
        {
            y = angle.y - 360f;
        }

        return Mathf.Round(y);
    }

    private void SetRotation()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = targetRotation;
        transform.eulerAngles = rotation;
    }
}