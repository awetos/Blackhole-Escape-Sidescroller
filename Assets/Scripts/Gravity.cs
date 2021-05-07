using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    float gravity;

    GravityUpdater findGravity;


    private void Start()
    {
        findGravity = GameObject.Find("GravityUpdater").GetComponent<GravityUpdater>();
        gravity = findGravity.getGravity();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * gravity);
        gravity = findGravity.getGravity();

    }
}
