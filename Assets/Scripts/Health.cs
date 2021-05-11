using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void die();
    public static event die OnDie;

    [SerializeField]
    float health;

    GameObject explosion;
    // Start is called before the first frame update
    const float healthMax = 100f;

    private void OnEnable()
    {
        CollisionEvents.onDamageTaken += takeDamage;
    }

    private void OnDisable()
    {
        CollisionEvents.onDamageTaken -= takeDamage;
    }

    void Start()
    {

        health = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void takeDamage(float f)
    {
        health = health - f;
        if(health <= 0)
        {
            health = 0;
            OnDie();
            Transform effect = this.gameObject.transform.GetChild(0);
            effect.GetComponent<ParticleSystem>().Stop();
            Destroy(gameObject);
           // GetComponent<SpriteRenderer>().enabled = false;
            //We do not destroy this, but simple disable it, because enemy ships continue to search for player.
        }
    }

    public float getHealth()
    {
        return health;
    }

    //Start the text with health Max instead of current health, sometimes health isn't initialized yet.
    public float getMaxHealth()
    {
        return healthMax;
    }
}
