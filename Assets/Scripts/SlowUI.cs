using UnityEngine;
using UnityEngine.UI;

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
    }
}