using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaControl : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))  //check if the up arrow is being press 
        {
            this.transform.position = //this refers to the object the code has been attached
            this.transform.position + 
            this.transform.localRotation * Vector3.forward * moveSpeed;  //Position is stored in a class called Vector3
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position = //this refers to the object the code has been attached
            this.transform.position +
            this.transform.localRotation * Vector3.back * moveSpeed;  //Position is stored in a class called Vector3
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.localRotation =
                Quaternion.Euler(0, -turnSpeed, 0) *  //Rotation is stored in a class called Quaternion 
                this.transform.localRotation;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.localRotation =
                Quaternion.Euler(0, turnSpeed, 0) *   //Rotation is stored in a class called Quaternion 
                this.transform.localRotation;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "poop") 
        {
            ScoreBoard.Counter += 1;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "boom")
        {
            Destroy(gameObject);
        }
    }

}
