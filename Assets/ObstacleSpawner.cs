using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab of the obstacle to spawn
    public float spawnInterval = 5f; // Time interval between spawns
    public Transform spawnPoint; // Point where the obstacle will spawn


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 2f, spawnInterval);
    }

    void SpawnObstacle()
    {
        // Instantiate the obstacle at the spawn point
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.Euler(0f, 0f, 90f));
        obstacle.GetComponent<ObstacleSpawner>().spawnPoint = spawnPoint; // manually assign it

        // Optionally, you can set the obstacle's parent to the spawner
        //obstacle.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
