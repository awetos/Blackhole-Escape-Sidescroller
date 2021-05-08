using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvents : MonoBehaviour
{
    public delegate void eatPowerup();
    public static event eatPowerup OnPowerupEaten;

    public delegate void takeDamage(float f);
    public static event takeDamage onDamageTaken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
