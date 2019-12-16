using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
