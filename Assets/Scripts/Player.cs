using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed; //The amount to multiply the player movement input by.
    private Transform TF;//The Transform for the attached object, set at start.

    // Start is called before the first frame update
    void Start()
    {
        TF = gameObject.GetComponent<Transform>(); //Get attached objects Transform.
    }

    //Movement should be done in FixedUpdate to look proper.
    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            TF.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, 0);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            TF.position += new Vector3(0, 0, Input.GetAxis("Vertical") * moveSpeed);
        }
    }
}
