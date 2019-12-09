using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucible : MonoBehaviour
{
    public GameObject destroyedVersion;

    void OnCollisionEnter (Collision coll)
    {
        if (coll.gameObject.tag == "Player" && coll.gameObject.GetComponent<Player>().ANIM.IsPlaying("RunningAttack")) {
        Instantiate(destroyedVersion, transform.position, Quaternion.Euler(0,0,0));
        Destroy(gameObject);
        }
    }
}
