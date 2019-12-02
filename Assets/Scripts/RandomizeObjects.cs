using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] randomizedObjects = new GameObject[0];
    [SerializeField] private int minNumObjects = 0;
    [SerializeField] private int maxNumObjects = 0;

    private void Start()
    {
        if (maxNumObjects < 0)
        {
            Debug.Log("maxNumObjects is less then 0. Seting to 0.");
            maxNumObjects = 0;
        }
        if (maxNumObjects > randomizedObjects.Length)
        {
            Debug.Log("maxNumObjects is more then the num of objects given. Seting to num of objects given.");
            maxNumObjects = randomizedObjects.Length;
        }
        if (minNumObjects > maxNumObjects)
        {
            Debug.Log("minNumObjects is more then the num of objects given. Seting to maxNumObjects.");
            minNumObjects = maxNumObjects;
        }

        Randomize();
    }

    private void Randomize()
    {
        if(randomizedObjects.Length == 0) return;

        int numObjects = Random.Range(minNumObjects, maxNumObjects + 1);
        int numRemove = randomizedObjects.Length - numObjects;
        Debug.Log("numObjects: " + numObjects + " | numRemove: " + numRemove);

        for (int i = 0; i < numRemove;)
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
