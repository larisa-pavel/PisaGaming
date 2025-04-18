using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 1f;
    public Transform[] spawnPoints;
    public float objectSpeed = 3f;

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
            List<int> usedSpawnPoints = new List<int>();

            for (int i = 0; i < count; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, spawnPoints.Length);
                } while (usedSpawnPoints.Contains(randomIndex));

                usedSpawnPoints.Add(randomIndex);
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
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].position.z < 300)
                    spawnPoints[i].position += Vector3.forward * 5f;
            }
        }
        else
        {
            Debug.LogError("ObstaclePrefabs or SpawnPoints are not assigned.");
        }
    }
}