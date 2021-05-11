using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{
    private void OnEnable()
    {
        Health.OnDie += Lose;
        Mover.OnBlackHoleDeath += Lose;
    }

    private void OnDisable()
    {
        Health.OnDie -= Lose;
        Mover.OnBlackHoleDeath -= Lose;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void Lose()
    {

    }
}
