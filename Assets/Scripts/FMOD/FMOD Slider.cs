using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class FMODSlider : MonoBehaviour
{
    public string parameterName = "Volume";
    public string eventPath = "event:/Music";
    public Slider slider;

    private EventInstance eventInstance;

    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(eventPath);
        eventInstance.start();
        eventInstance.setParameterByName(parameterName, slider.value);

        slider.onValueChanged.AddListener(SetParameter);
    }

    void SetParameter(float value)
    {
        eventInstance.setParameterByName(parameterName, value);
    }

    void OnDestroy()
    {
        eventInstance.release();
    }
}
