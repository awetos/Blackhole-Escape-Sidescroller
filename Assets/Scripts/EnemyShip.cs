using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    Vector3 playerLocation;
    Vector3 destination;
    [SerializeField]
    float enemySpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if(playerLocation != null)
        {

            destination = playerLocation - transform.position;
        }
        else
        {
            destination = Vector3.right;
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(destination * Time.deltaTime * enemySpeed);
    }

    public void IncreaseSpeed(float f)
    {
        enemySpeed += f;
    }
}
