using UnityEngine;

public class ObstacleSize : MonoBehaviour
{
    public float length; // adjust per prefab
    void Update()
    {
        length = transform.parent.GetComponent<Collider>().bounds.size.z;
    }
}