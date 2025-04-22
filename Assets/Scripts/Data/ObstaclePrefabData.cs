using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstaclePrefabData", menuName = "Data/ObstaclePrefabData")]
public class ObstaclePrefabData : ScriptableObject
{
    [field: SerializeField] public GameObject Gate { get; private set; }
    [field: SerializeField] public GameObject Barrier { get; private set; }
    [field: SerializeField] public GameObject Wall { get; private set; }
    [field: SerializeField] public GameObject Hopping { get; private set; }
    [field: SerializeField] public GameObject Train { get; private set; }

    public Dictionary<ObstacleType, GameObject> Obstacles { get; private set; } = new Dictionary<ObstacleType, GameObject>();

    public void Initialize()
    {
        Obstacles.Clear();
        Obstacles.Add(ObstacleType.Gate, Gate);
        Obstacles.Add(ObstacleType.Barrier, Barrier);
        Obstacles.Add(ObstacleType.Wall, Wall);
        Obstacles.Add(ObstacleType.Hopping, Hopping);
        Obstacles.Add(ObstacleType.Train, Train);
    }
}
public enum ObstacleType
{
    Gate,
    Barrier,
    Wall,
    Hopping,
    Train
}