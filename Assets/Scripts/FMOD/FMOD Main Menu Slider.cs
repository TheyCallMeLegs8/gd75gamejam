using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;

public class FmodSliderController : MonoBehaviour
{
    public Slider slider;
    public EventInstance eventInstance;

    public void OnSliderValueChange(float value)
    {
        if (eventInstance != null && slider != null)
        {
            // Replace "your_parameter_name" with the actual parameter name in your FMOD event
            eventInstance.setParameterByName("Main Menu Music", value);
        }
    }
}