using UnityEngine;

public class CamaraFollower : MonoBehaviour
{
    //GameObject.FindGameObjectsWithTag("Ninja");
    public Transform target;
    public float smoothSpeed = .125f;
    public Vector3 offset;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ninja").transform;
    }
    private void LateUpdate()  //LateUpdate runs right after the update that the cameras do not compete between themselves 
    {
        Vector3 desireposition = target.position + offset; // target and offset are two vectores, we are adding two vectores.
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desireposition, smoothSpeed);
        transform.position = smoothedposition;
    }
}
