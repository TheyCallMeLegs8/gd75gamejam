using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterData", menuName = "Data/EncounterData")]
public class EncounterData : ScriptableObject
{
    [field: SerializeField] public EncounterTransitionType TransitionType { get; private set; }
    [field: SerializeField] public List<ObstacleType> Obstacles { get; private set; } = new List<ObstacleType>();
    [field: SerializeField] public List<int> Lanes { get; private set; } = new List<int>();
    [field: SerializeField] public List<float> TimeOffset { get; private set; } = new List<float>();
}

public enum EncounterTransitionType
{
    Any,
    A,
    B,
    C
}