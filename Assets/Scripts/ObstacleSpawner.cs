using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 3f;
    public Transform[] spawnPoints;
    public float objectSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnStartingObstacles(70);
        InvokeRepeating(nameof(SpawnObstaclesAtInterval), 0f, spawnInterval);
    }

    void SpawnObstaclesAtInterval()
    {
        // Randomly determine the number of objects to spawn (1, 2, or 3)
        int numberOfObjectsToSpawn = Random.Range(1, 4);
        SpawnObstacle(numberOfObjectsToSpawn);
    }

    void SpawnObstacle(int count)
    {
        // Instantiate the specified number of obstacles at random spawn points
        if (obstaclePrefab != null && spawnPoints.Length > 0)
        {
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform selectedSpawnPoint = spawnPoints[randomIndex];
                GameObject createdObject = Instantiate(obstaclePrefab, selectedSpawnPoint.position, Quaternion.Euler(0f, 0f, 90f));
                createdObject.GetComponent<ObstacleMove>().speed = objectSpeed; // Set the speed of the obstacle
            }
        }
        else
        {
            Debug.LogError("ObstaclePrefab or SpawnPoints are not assigned.");
        }
    }

    void SpawnObstacle(Vector3 position)
    {
        if (obstaclePrefab != null && spawnPoints.Length > 0)
        {
            GameObject createdObject = Instantiate(obstaclePrefab, position, Quaternion.Euler(0f, 0f, 90f));
            createdObject.GetComponent<ObstacleMove>().speed = objectSpeed; // Set the speed of the obstacle
        }
        else
        {
            Debug.LogError("ObstaclePrefab or SpawnPoints are not assigned.");
        }
    }

    void SpawnStartingObstacles(int interval)
    {
        // Spawn a few obstacles at the start
        for (int i = 0; i < interval / 5; i++)
        {
            Debug.Log("Spawning obstacle at interval: " + i);
            int randomPoint = Random.Range(0, spawnPoints.Length);
            SpawnObstacle(new Vector3(i * 5, 0, spawnPoints[randomPoint].position.z));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}