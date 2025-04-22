using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    [field: SerializeField] public ObstaclePrefabData ObstaclePrefabData { get; private set; }

    private void Awake()
    {
        ObstaclePrefabData.Initialize();
    }

    public void SpawnObjectOfType(ObstacleType obstacleType)
    {
        Debug.Log(obstacleType.ToString());
        Debug.Log(ObstaclePrefabData.Obstacles[obstacleType]);
        GameObject obj = Instantiate(ObstaclePrefabData.Obstacles[obstacleType], transform);
        obj.transform.localPosition = Vector3.zero;
        Debug.Log($"Spawning object of type: {obstacleType}");
    }
}