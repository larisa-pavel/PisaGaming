using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 1f;
    public Transform[] spawnPoints;
    public float objectSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
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
        if (obstaclePrefabs.Length > 0 && spawnPoints.Length > 0)
        {
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform selectedSpawnPoint = spawnPoints[randomIndex];

                int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
                GameObject selectedPrefab = obstaclePrefabs[prefabIndex];

                GameObject createdObject = Instantiate(selectedPrefab, selectedSpawnPoint.position, Quaternion.Euler(0f, 0f, 0f));
                MoverScript mover;
                if (createdObject.transform.GetChild(0) != null)
                    mover = createdObject.transform.GetChild(0).GetComponent<MoverScript>();
                else 
                    mover = createdObject.GetComponent<MoverScript>();

                mover.start = selectedSpawnPoint.position;
                mover.end = new Vector3(selectedSpawnPoint.position.x, selectedSpawnPoint.position.y, 10f);
                mover.speed = objectSpeed;
            }
        }
        else
        {
            Debug.LogError("ObstaclePrefabs or SpawnPoints are not assigned.");
        }
    }
}