using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallEventHandler : MonoBehaviour
{
    public delegate void PowerUpEaten();
    public static event PowerUpEaten OnPowerupEaten;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
