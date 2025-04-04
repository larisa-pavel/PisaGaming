using UnityEngine;
using UnityEngine.UI;
using Kino;

public class SlowMoUI : MonoBehaviour
{
    public PlayerController player;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = player.maxSlowMoEnergy; // Set max dynamically
    }

    void Update()
    {
        slider.value = player.currentSlowMoEnergy;

        if (slider.value <= 0)
        {
            slider.gameObject.SetActive(false); // Hide the slider when energy is depleted
        }
        else
        {
            slider.gameObject.SetActive(true); // Show the slider when energy is available
        }
        
        // The lower the slider value the more glitchy effects

    }
}