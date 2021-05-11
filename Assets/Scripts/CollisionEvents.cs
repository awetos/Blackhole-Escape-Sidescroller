using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvents : MonoBehaviour
{
    public delegate void eatPowerup();
    public static event eatPowerup OnPowerupEaten;

    public delegate void takeDamage(float f);
    public static event takeDamage onDamageTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //It is possible the player hits more than one object at a time
        //So they are their own if statements

        if(collision.tag == "Asteroid")
        {
            Destroy(collision); //We must destroy it immediately so that we will not hit the collider again or use lateupdate
            onDamageTaken(10f);
        }
        if(collision.tag == "Spaceship")
        {
            Destroy(collision);
            onDamageTaken(40f);
        }
        if(collision.tag == "EnergyBall")
        {
            OnPowerupEaten();
        }

    }

}
