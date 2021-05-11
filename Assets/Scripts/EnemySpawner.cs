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

    /*
     * secondsPerStage[0] = 8
     * secondsPerStage[1] = 16
     * secondsPerStage[2] = 24
     * secondsPerStage[3] = 32
     * secondsPerStage[4] = 40
     * When stageIndex is more than 40 seconds, stageIndex = 5
     */
    const int stages = 5;
    int stageIndex = 0; //stage index determines how many enemies spawn and rate
    int timePerStage = 8;
    float timeElapsed = 0f;

    //Variables for setting up spawn points
    float range;
    List<float> spawnPointY;
    float spawnPointX;
    float increment;

    //This array holds the chance that something will spawn
    /*
     * 75% chance it is an asteroid (store value of 0)
     * 20% chance it is a spaceship (store a value of 1)
     * 5% chance it is another energy ball (store a value of 2)
     */
    int[] spawnChances;
    int asteroidChance;
    int spaceshipChance;
    int energyBallChance;


    // Start is called before the first frame update
    void Start()
    {
        //Set up Camera in order to find where to spawn enemies
        SetUpCamera();

        //Set up evenly spaced areas to spawn asteroids
        SetUpSpawnPoints();

        //Stages increase how many items spawn (not implemented)
        secondsPerStage = new int[stages];
        int k = timePerStage;
        for(int i = 0; i < stages; i++)
        {
            secondsPerStage[i] = k;
            k += timePerStage;
        }

        asteroidChance = 75;
        spaceshipChance = 20;
        energyBallChance = 5;
        spawnChances = new int[100];

        SetUpSpawnChances();

        //polish: delete the first and last spawnpoints so that mostly the middle is used.
        StartCoroutine(SpawnItem());
        StartCoroutine(SpawnEnergyBallsRegularly());
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (stageIndex < stages && timeElapsed > secondsPerStage[stageIndex]) //Stages: 0, 1, 2, 3, 4
        {
            stageIndex++; // ends at stage 5
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
    void SetUpSpawnChances()
    {

        for (int i = 0; i < 100; i++)
        {
            if (i < asteroidChance)
            {
                spawnChances[i] = 0;
            }
            else
            {
                if (i < asteroidChance + spaceshipChance)
                {
                    spawnChances[i] = 1;
                }
                else
                {
                    spawnChances[i] = 2;
                }
            }
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
            SpawnByChance(location);

            if (r > 1)
            {

                newLocation = new Vector3(spawnPointX, randomY(), 0f);
                if(newLocation.y - location.y < increment * 2)
                {
                    int i = 1;
                    //try up to 4 times not to spawn near the same place
                    while (i < 4 && (newLocation.y - location.y < increment * 2)) 
                    {
                        newLocation.y = randomY();
                        i++;
                    }
                    
                }
                SpawnByChance(newLocation);
            }


            yield return new WaitForSeconds(4f / (stageIndex + 1));
        }
        
    }

    IEnumerator SpawnEnergyBallsRegularly()
    {
        while (true)
        {
            Vector3 location = new Vector3(spawnPointX, randomY(), 0f);
            spawnEnergyBall(location);
            yield return new WaitForSeconds(3f);
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
        return y;
    }

    void SpawnByChance(Vector3 location)
    {
        int i = (int)Mathf.Round(Random.Range(0f, spawnChances.Length));
        if(i >= spawnChances.Length)
        {
            i = spawnChances[spawnChances.Length - 1];
        }

        switch (spawnChances[i])
        {
            case 0:
                SpawnAsteroid(location);
                break;
            case 1:
                SpawnSpaceship(location);
                break;
            case 2:
                spawnEnergyBall(location);
                break;
            default:
                SpawnAsteroid(location);
                break;
        }

    }
    void SpawnAsteroid(Vector3 location)
    {
        Instantiate(AsteroidPrefab, location, Quaternion.identity, this.transform);
    }

    void SpawnSpaceship(Vector3 location)
    {
        Instantiate(SpaceshipPrefab, location, Quaternion.identity, this.transform);
        //In future stages, spaceships may be changed to have higher speed
        //EnemyShip newShip = Instantiate(SpaceshipPrefab, location, Quaternion.identity, this.transform).GetComponent<EnemyShip>();
        // float newShipSpeed = 0.1f * stageIndex;
        // newShip.IncreaseSpeed(newShipSpeed);
    }

    void spawnEnergyBall(Vector3 location)
    {
        Instantiate(EnergyBall, location, Quaternion.identity, this.transform);
    }
}
