using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDespawner : MonoBehaviour
{
    public float startPlatformX = 90f;
    private ObstacleMove ObstacleMove;
    // Start is called before the first frame update
    void Start()
    {
        ObstacleMove = GetComponent<ObstacleMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > startPlatformX)
        {
            ObstacleMove.enabled = false; // Disable the movement script
            transform.position = new Vector3(transform.position.x, transform.position.y - ObstacleMove.speed * Time.deltaTime, transform.position.z);
        }
        if (transform.position.x > startPlatformX + 10)
        {
            Destroy(gameObject);
        }
    }
}
