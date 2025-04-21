using System.Collections.Generic;
using UnityEngine;

public class OptionsSlider : MonoBehaviour
{
    [SerializeField] private OptionsValueType _optionVariable;
    [field: SerializeField] public OptionsData OptionsData { get; private set; }


    public void OnValueChanged(float value)
    {
        switch (_optionVariable)
        {
            case OptionsValueType.Volume:
                OptionsData.SetVolume(value);
                break;
            case OptionsValueType.Gamma:
                OptionsData.SetGamma(value);
                break;
        }
    }
}

public enum OptionsValueType
{
    Volume,
    Gamma
}
