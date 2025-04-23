using UnityEngine;
using FMOD;
using FMOD.Studio;

public class VolumeControl : MonoBehaviour
{
    //public Slider volumeSlider; // UI slider in Unity
    public FMOD.Studio.Bus musicBus;  // FMOD Bus (could be a VCA or a group)

    void Start()
    {
        // Get the FMOD Studio project's root sound bank
        //var project = FMODManager.GetRootGroup();

        // Find the FMOD Bus (VCA or group)
        //musicBus = project.getBusByName("YourMusicBusName");

        // Initialize the slider's value (optional)
        //volumeSlider.value = musicBus.getVolume();
    }

    void Update()
    {
        // Update the VCA's volume based on the slider's value
        //musicBus.setVolume(volumeSlider.value);
    }
}