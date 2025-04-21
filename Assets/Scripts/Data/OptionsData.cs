using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "OptionsData", menuName = "Data/OptionsData")]
public class OptionsData : ScriptableObject
{
    [field: SerializeField] public float Volume { get; private set; } = 1f;
    [field: SerializeField] public float Gamma { get; private set; } = 1f;

    public void SetVolume(float value)
    {
        Volume = value;
    }

    public void SetGamma(float value)
    {
        Gamma = value;
    }
}