using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    float gravity = 1.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * gravity);
    }
}
