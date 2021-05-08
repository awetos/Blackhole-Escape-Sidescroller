using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    float gravity;

    [SerializeField]
    bool isPlayer = false;
    bool isGravityOn = true;

    GravityUpdater findGravity;


    private void Start()
    {
        findGravity = GameObject.Find("GravityUpdater").GetComponent<GravityUpdater>();

        turnGravityOn();

        if (this.gameObject.tag == "Player")
        {
            isPlayer = true;
            StartCoroutine(ResumeGravity());
        }

        gravity = findGravity.getGravity();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            if(isGravityOn == true) //Energyballs can cancel gravity
            {
                transform.Translate(Vector3.left * Time.deltaTime * gravity);
            }
            gravity = findGravity.getGravity();
            //turn gravity back on in 5 seconds
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * gravity);
            gravity = findGravity.getGravity();
        }
    }

    IEnumerator ResumeGravity()
    {
        while (true)
        {
            if (isGravityOn == false)
            {
                turnGravityOn();
            }
            yield return new WaitForSeconds(5f);
        }
        //Every 5 seconds, if gravity is not turned on, turn it back on
    }

    public void turnGravityOn()
    {
        isGravityOn = true;
    }

    public void turnGravityOff()
    {
        isGravityOn = false;
    }
}
