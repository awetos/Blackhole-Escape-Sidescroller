using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityUpdater : MonoBehaviour
{
    [SerializeField]
    float UpdateEveryHowManySeconds = 5f;
    float gravity = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(updateGravity());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator updateGravity()
    {
        while (true)
        {
            if(gravity > 10)
            {
                gravity += 5f;
            }
            else
            {
                gravity += 1f;
            }
            yield return new WaitForSeconds(UpdateEveryHowManySeconds);
        }
        
    }
    public float getGravity()
    {
        return gravity;
    }
}
