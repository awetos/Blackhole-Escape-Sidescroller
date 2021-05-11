using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    float gravity;
    float slowedGravity;

    bool gravityCoolDown;
    float gravityTimer;
    const float cooldownAmount = 5f;

    [SerializeField]
    bool isPlayer = false;
    bool isGravityOn = true;

    GravityUpdater findGravity;

    private void OnEnable()
    {
        CollisionEvents.OnPowerupEaten += turnGravityOff;
    }

    private void OnDisable()
    {
        CollisionEvents.OnPowerupEaten -= turnGravityOff;
    }

    private void Start()
    {
        gravityTimer = 0f;
        findGravity = GameObject.Find("GravityUpdater").GetComponent<GravityUpdater>();

        turnGravityOn();

        if (this.gameObject.tag == "Player")
        {
            isPlayer = true;
        }

        gravity = findGravity.getGravity();
    }

    // Update is called once per frame
    void Update()
    {
        gravityTimer += Time.deltaTime;
        if (gravityTimer > cooldownAmount)
        {
            isGravityOn = true;
        }
        if (isPlayer)
        {
            if(isGravityOn == true) //Energyballs can cancel gravity
            {
                transform.Translate(Vector3.left * Time.deltaTime * gravity);
                gravity = findGravity.getGravity();
            }
            else
            {
                
                transform.Translate(Vector3.left * Time.deltaTime * slowedGravity);

            }

            //turn gravity back on in 5 seconds
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * gravity);
            gravity = findGravity.getGravity();
        }
    }


    public void turnGravityOn()
    {
        isGravityOn = true;
    }

    public void turnGravityOff()
    {
        gravityTimer = 0f;
        slowedGravity = 0.25f * gravity; //this can be changed later to be more dynamic
        isGravityOn = false;
    }
}
