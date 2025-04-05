using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
   public GameObject GameOverScreen;

   void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the Game Over scene
            Debug.Log("Game Over");
            GameOverScreen.SetActive(true);
        }
    }
}
