using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public void SpawnObjectOfType(ObstacleType obstacleType)
    {
        Debug.Log($"Spawning object of type: {obstacleType}");
    }
}
