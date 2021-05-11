using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HealthText : MonoBehaviour
{

    Text healthText;
    Health currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("Health").GetComponent<Text>();
        currentHealth = GameObject.Find("Player").GetComponent<Health>();

        healthText.text = string.Format("Health: {0:0}", currentHealth.getMaxHealth());
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = string.Format("Health: {0:0}", currentHealth.getHealth());
    }
}