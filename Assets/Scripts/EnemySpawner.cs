using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Camera mainCamera;
    Vector3 topBound, bottomBound, worldTopBound, worldBottomBound;
    float y1, y2; // the line where obstacles spawn

    [SerializeField]
    GameObject AsteroidPrefab;

    [SerializeField]
    GameObject SpaceshipPrefab;

    [SerializeField]
    GameObject EnergyBall;

    //How many spawnpoints evenly spaced vertically there should be
    //The game designer should balance it based on the size of the sprites
    [SerializeField]
    int numberOfSpawnPoints = 9;

    [SerializeField]
    int[] secondsPerStage;

    const int stages = 5;
    int stageIndex = 0; //stage index determines how many enemies spawn and rate
    int timePerStage = 8;

    float timeElapsed = 0f;

    //Variables for setting up spawn points
    float range;
    List<float> spawnPointY;
    float spawnPointX;
    float increment;



    // Start is called before the first frame update
    void Start()
    {
        //Set up Camera in order to find where to spawn enemies
        SetUpCamera();

        //Set up evenly spaced areas to spawn asteroids
        SetUpSpawnPoints();

        secondsPerStage = new int[stages];
        int k = timePerStage;
        for(int i = 0; i < stages; i++)
        {
            secondsPerStage[i] = k;
            k += timePerStage;
        }

       
        //polish: delete the first and last spawnpoints so that mostly the middle is used.
        StartCoroutine(SpawnItem());
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (stageIndex < stages && timeElapsed > secondsPerStage[stageIndex]) //greater than 10 seconds
        {
            stageIndex++;
        }
    }

   
    void SetUpCamera()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        topBound = new Vector3(1f, 1f, 0f);
        bottomBound = new Vector3(1f, 0f, 0f);
        worldTopBound = mainCamera.ViewportToWorldPoint(topBound);
        worldTopBound = new Vector3(spawnPointX, worldTopBound.y, -1 * worldTopBound.z);
        worldBottomBound = mainCamera.ViewportToWorldPoint(bottomBound);
        worldBottomBound = new Vector3(worldBottomBound.x, worldBottomBound.y, -1 * worldBottomBound.z);
       
    }
    void SetUpSpawnPoints()
    {
        y1 = worldTopBound.y;
        y2 = worldBottomBound.y;

        // dynamically divide number of different spawn points.
        range = y1 - y2;
        numberOfSpawnPoints = 15;
        increment = range / numberOfSpawnPoints;

        // have a buffer so the item doesn't randomly appear at the edge of the screen
        spawnPointX = worldTopBound.x + 10f; 
        
        spawnPointY = new List<float>();

        for (int i = 0; i < numberOfSpawnPoints; i++)
        {
            spawnPointY.Add(y2 + i * increment);
        }

    }

    IEnumerator SpawnItem()
    {
        Vector3 location;
        Vector3 newLocation;
        while (true)
        {
            int r = (int)Mathf.Round(Random.Range(0f, 2f));

            location = new Vector3(spawnPointX, randomY(), 0f);
            SpawnAsteroid(location);

            if(r > 1)
            {

                newLocation = new Vector3(spawnPointX, randomY(), 0f);
                if(newLocation.y - location.y < increment * 2)
                {
                    int i = 1;
                    while(i < 4 && (newLocation.y - location.y < increment * 2))
                    {
                        newLocation.y = randomY();
                        i++;
                    }
                    
                }
                SpawnAsteroid(newLocation);
            }


            yield return new WaitForSeconds(2f / (stageIndex + 1));
        }
        
    }
    float randomY()
    {
        int i = (int)Mathf.Round(Random.Range(0f, numberOfSpawnPoints)); //grab a random index
        if (i < 0)
        {
            i = 0;
        }
        if (i >= spawnPointY.Count)
        {
            i = spawnPointY.Count - 1;
        }
        float y = spawnPointY[i];
        Debug.Log(y);
        return y;
    }
    void SpawnAsteroid(Vector3 location)
    {

        Instantiate(AsteroidPrefab, location, Quaternion.identity, this.transform);

    }

    void SpawnSpaceship(Vector3 location)
    {

    }
}
