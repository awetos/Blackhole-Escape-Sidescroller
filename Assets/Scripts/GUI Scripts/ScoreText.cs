using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    GameObject collectedScore;
    Text scoreText;
    float myScore;

    bool keepCounting;

    private void OnEnable()
    {
        CollisionEvents.OnPowerupEaten += spawnCollectedScore;

        Health.OnDie += Stop;
        Mover.OnBlackHoleDeath += Stop;
    }

    private void OnDisable()
    {
        CollisionEvents.OnPowerupEaten -= spawnCollectedScore;

        Health.OnDie -= Stop;
        Mover.OnBlackHoleDeath -= Stop;
    }
   

    // Start is called before the first frame update
    void Start()
    {
        keepCounting = true;
        collectedScore = Resources.Load("CollectedScore") as GameObject;
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(keepCounting == true)
        {
            myScore += Time.deltaTime * 10;
            scoreText.text = string.Format("{0:0}", myScore);
        }
        
    }

    void spawnCollectedScore()
    {
        GameObject cs;
        cs = Instantiate(collectedScore, new Vector3(transform.position.x + 20, transform.position.y, transform.position.z), Quaternion.identity);
        myScore += 40f;
        cs.transform.parent = gameObject.transform;
        Destroy(cs, 0.5f);
    }

    void Stop()
    {
        keepCounting = false;
    }
}
