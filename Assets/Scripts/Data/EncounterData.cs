using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterData", menuName = "Data/EncounterData")]
public class EncounterData : ScriptableObject
{
    [field: SerializeField] public EncounterTransitionType TransitionType { get; private set; }
}

public enum EncounterTransitionType
{
    Any,
    A,
    B,
    C
}