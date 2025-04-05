using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    public GameObject groundTile;

    void SpawnTile()
    {
        Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
    }

    Vector3 nextSpawnPoint;
}
