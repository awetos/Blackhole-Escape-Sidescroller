using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAsteroid : MonoBehaviour
{
    float randomSize;
    float randomRotation;
    // Start is called before the first frame update
    void Start()
    {
        randomSize = Random.Range(0.5f, 2.5f);

        transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        //randomRotation = Random.Range(0f, 360);
        //transform.localRotation = Quaternion.Euler(0, 0, randomRotation);
        //Random rotation does not work with gravity as it begins to move towards its local rotation
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
