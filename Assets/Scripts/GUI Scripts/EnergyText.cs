using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyText : MonoBehaviour
{
    Text energyText;
    int energyBalls;
    

    private void OnEnable()
    {
        CollisionEvents.OnPowerupEaten += addBall;
    }

    private void OnDisable()
    {
        CollisionEvents.OnPowerupEaten -= addBall;
    }

    // Start is called before the first frame update
    void Start()
    {
        energyBalls = 0;
        energyText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        energyText.text = string.Format("Energy Balls: {0}", energyBalls);
    }

    void addBall()
    {
        energyBalls++;
    }
}
