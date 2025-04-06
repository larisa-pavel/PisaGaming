using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    public GameObject GameWinScreen;
    AudioSource winSound;

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the Game Over scene
            UnityEngine.Debug.Log("You Win");
            GameWinScreen.SetActive(true);
            winSound.Play();
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
