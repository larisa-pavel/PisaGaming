using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 5f;
    public Transform[] spawnPoints;
    public float objectSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 2f, spawnInterval);
    }

    void SpawnObstacle()
    {
        // Instantiate the obstacle at a random spawn point
        if (obstaclePrefab != null && spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform selectedSpawnPoint = spawnPoints[randomIndex];
            GameObject createdObject = Instantiate(obstaclePrefab, selectedSpawnPoint.position, Quaternion.Euler(0f, 0f, 90f));
            createdObject.GetComponent<ObstacleMove>().speed = objectSpeed; // Set the speed of the obstacle
        }
        else
        {
            Debug.LogError("ObstaclePrefab or SpawnPoints are not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}