using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenOut : MonoBehaviour
{
    Vector3 leftTop, leftTopBoundary;
    float terminatorX;
    float buffer = 2f; //This buffer should cover the size of sprites so they appear to disappear off screen
    Camera mainCamera;



    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        leftTop = new Vector3(0f, 1f, 0f);
        leftTopBoundary = mainCamera.ViewportToWorldPoint(leftTop);
        terminatorX = leftTopBoundary.x - buffer;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < terminatorX)
        {
            Destroy(this.gameObject);
        }
    }

}
