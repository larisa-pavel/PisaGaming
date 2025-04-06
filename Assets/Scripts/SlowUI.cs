using UnityEngine;
using UnityEngine.UI;
using Kino;
using System.Diagnostics;

public class SlowMoUI : MonoBehaviour
{
    public PlayerController player;
    private Slider slider;
    public AnalogGlitch AnalogGlitchEffect;
    public DigitalGlitch DigitalGlitchEffect;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = player.maxSlowMoEnergy; // Set max dynamically
    }

    void Update()
    {

        slider.value = player.currentSlowMoEnergy;

        if (slider.value < 2f)
        {
            float decrease = (5 - slider.value)/ 15 ;
            AnalogGlitchEffect.scanLineJitter = decrease; // The lower the slider value the more glitchy effects

            AnalogGlitchEffect.horizontalShake = decrease; // The lower the slider value the more glitchy effects
            AnalogGlitchEffect.colorDrift = decrease; // The lower the slider value the more glitchy effects
            UnityEngine.Debug.Log("AnalogGlitch scanLineJitter: " + AnalogGlitchEffect.scanLineJitter);

            if (slider.value <= 1.5f)
            {
                AnalogGlitchEffect.verticalJump = 0.1f; // The lower the slider value the more glitchy effects
            }
            else
            {
                AnalogGlitchEffect.verticalJump = 0f;
            }

            if (slider.value <= 0.5f)
            {
                DigitalGlitchEffect.intensity = 0.2f; // The lower the slider value the more glitchy effects
            }
            else
            {
                DigitalGlitchEffect.intensity = 0f;
            }
        }
        else
        {
            AnalogGlitchEffect.scanLineJitter = 0f;
            AnalogGlitchEffect.horizontalShake = 0f;
            AnalogGlitchEffect.colorDrift = 0f;
        }

        


        // The lower the slider value the more glitchy effects

    }
}
