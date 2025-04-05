using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColision : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform plat;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the platform");
            // Add your logic here for when the player enters the platform
            other.gameObject.transform.SetParent(plat);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
            Debug.Log("Player has exited the platform");
            // Add your logic here for when the player exits the platform
        }
    }
}
