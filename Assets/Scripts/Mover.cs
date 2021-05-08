using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    public float shipSpeed = 2.0f;

    [SerializeField]
    Camera mainCamera;

    Vector3 middleTop, middleBottom, middleTopBoundary, middleBottomBoundary;
    Vector3 leftTop, leftBottom, leftTopBoundary, leftBottomBoundary;
    Vector3 startPosition;
    // Start is called before the first frame update
    private void Start()
    {
        middleTop = new Vector3(0.5f,1f,0f);
        middleBottom = new Vector3(0.5f, 0f, 0f);
        leftTop = new Vector3(0f,1f,0f);
        leftBottom = new Vector3(0f, 0f, 0f) ;
        middleTopBoundary = mainCamera.ViewportToWorldPoint(middleTop);
        middleBottomBoundary = mainCamera.ViewportToWorldPoint(middleBottom);
        leftTopBoundary = mainCamera.ViewportToWorldPoint(leftTop);
        leftBottomBoundary = mainCamera.ViewportToWorldPoint(leftBottom);
        float x = (middleTopBoundary.x + leftTopBoundary.x) / 2;
        float y = (middleTopBoundary.y + middleBottomBoundary.y) / 2;
        startPosition = new Vector3(x, y, 0f);
        transform.position = startPosition;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("Horizontal") < 0) //go left
        {
            

            if (Input.GetAxis("Vertical") < 0) //go down
            {
                transform.Translate(Vector3.down * Time.deltaTime * shipSpeed * 0.5f);
                transform.Translate(Vector3.left * Time.deltaTime * shipSpeed * 0.5f);
            }
            else if (Input.GetAxis("Vertical") > 0) //go up
            {
                transform.Translate(Vector3.up * Time.deltaTime * shipSpeed * 0.5f);
                transform.Translate(Vector3.left * Time.deltaTime * shipSpeed * 0.5f);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * shipSpeed);
            }


            ClampToScreen();
        }
        if (Input.GetAxis("Horizontal") > 0) //go right
        {
            

            if (Input.GetAxis("Vertical") < 0) //go down
            {
                transform.Translate(Vector3.down * Time.deltaTime * shipSpeed * 0.5f);
                transform.Translate(Vector3.right * Time.deltaTime * shipSpeed * 0.5f);
            }
            else if (Input.GetAxis("Vertical") > 0) //go up
            {
                transform.Translate(Vector3.up * Time.deltaTime * shipSpeed * 0.5f);
                transform.Translate(Vector3.right * Time.deltaTime * shipSpeed * 0.5f);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * shipSpeed);
            }

            ClampToScreen();
        }



        if (Input.GetAxis("Vertical") < 0) //go down
        {
            

            if (Input.GetAxis("Horizontal") < 0) //go left
            {
                transform.Translate(Vector3.left * Time.deltaTime * shipSpeed * 0.5f);
                transform.Translate(Vector3.down * Time.deltaTime * shipSpeed * 0.5f);
            }
            else if (Input.GetAxis("Horizontal") > 0) //go right
            {
                transform.Translate(Vector3.right * Time.deltaTime * shipSpeed * 0.5f);
                transform.Translate(Vector3.down * Time.deltaTime * shipSpeed * 0.5f);
            }
            else
            {
                transform.Translate(Vector3.down * Time.deltaTime * shipSpeed);
            }

            ClampToScreen();
        }
        if (Input.GetAxis("Vertical") > 0) //go up
        {

            if (Input.GetAxis("Horizontal") < 0) //go left
            {
                transform.Translate(Vector3.left * Time.deltaTime * shipSpeed * 0.5f);
                transform.Translate(Vector3.up * Time.deltaTime * shipSpeed * 0.5f);
            }
            else if (Input.GetAxis("Horizontal") > 0) //go right
            {
                transform.Translate(Vector3.right * Time.deltaTime * shipSpeed * 0.5f);
                transform.Translate(Vector3.up * Time.deltaTime * shipSpeed * 0.5f);
            }
            else
            {
                transform.Translate(Vector3.up * Time.deltaTime * shipSpeed);
            }
            ClampToScreen();
        }
        
    }

    void ClampToScreen()
    {
        //Do not go above or below the screen
        //Do not go beyond the middle of the screen

        if (transform.position.y > middleTopBoundary.y)
        {
            Vector3 relocateVector = new Vector3(transform.position.x, middleTopBoundary.y, 0);
            transform.position = relocateVector;
        }
        if (transform.position.y < middleBottomBoundary.y)
        {
            Vector3 relocateVector = new Vector3(transform.position.x, middleBottomBoundary.y, 0);
            transform.position = relocateVector;
        }


        if (transform.position.x > middleTopBoundary.x)
        {
            Vector3 relocateVector = new Vector3(middleTopBoundary.x, transform.position.y, 0);
            transform.position = relocateVector;
        }
    }
}