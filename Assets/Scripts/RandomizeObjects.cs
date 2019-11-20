using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] randomizedObjects = new GameObject[0];
    [SerializeField] private int minNumObjects = 0;

    private void Start()
    {
        int maxNumObjects = randomizedObjects.Length;
        if (minNumObjects > maxNumObjects)
        {
            Debug.Log("minNumObjects is more then the num of objects given. Seting to maxNumObjects.");
            minNumObjects = maxNumObjects;
        }
        int numObjects = Random.Range(minNumObjects, maxNumObjects);
        int numRemove = maxNumObjects - numObjects;
        Debug.Log(numObjects + " | " + numRemove);
        if(numObjects > 0)
        {
            for(int i = 0; i < numRemove;)
            {
                int removeIndex = Random.Range(0, randomizedObjects.Length);

                if (randomizedObjects[removeIndex].activeSelf)
                {
                    randomizedObjects[removeIndex].SetActive(false);
                    i++;
                }
            }
        }
    }
}
