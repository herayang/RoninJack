using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucible : MonoBehaviour
{
    public GameObject destroyedVersion;

    void OnMouseDown ()
    {
        Instantiate(destroyedVersion, transform.position, Quaternion.Euler(0,0,0));
        Destroy(gameObject);
    }
}
