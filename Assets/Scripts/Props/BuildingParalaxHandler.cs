using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PrimeTween;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingParalaxHandler : MonoBehaviour
{
    [SerializeField] private float _vanishZValue;
    [SerializeField] private float _speed;
    [SerializeField] private int _sideMultiplier;

    [SerializeField] private Vector2 _buildingGapRange = new Vector2(1f, 2f);

    [SerializeField] private List<GameObject> _buildingPrefabs = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(BuildingSpawnLoop());
    }

    private IEnumerator BuildingSpawnLoop()
    {
        while (true)
        {
            // Pick the next object to spawn
            GameObject spawnable = GetRandomBuilding();

            // Spawn the building
            GameObject spawnedBuilding = Instantiate(spawnable, transform);
            Building building = spawnedBuilding.GetComponent<Building>();
            // Move the building
            Vector3 offset = new Vector3(_sideMultiplier * building.Dimensions.x / 2f, 0f, building.Dimensions.z);
            spawnedBuilding.transform.Translate(offset / 2f);
            MoveBuilding(spawnedBuilding);


            // Calculate the travel time of the object
            float interval = (building.Dimensions.z + Random.Range(_buildingGapRange.x, _buildingGapRange.y)) / _speed;

            

            // Wait before repeating
            yield return new WaitForSeconds(interval);
        }
    }

    private void MoveBuilding(GameObject building)
    {
        StartCoroutine(BuildingTransit(building.transform));
    }

    private IEnumerator BuildingTransit(Transform building)
    {
        Vector3 targetPoint = building.position;
        targetPoint.z = _vanishZValue;
        yield return Tween.PositionAtSpeed(building, targetPoint, _speed, Ease.Linear).ToYieldInstruction();
        Destroy(building.gameObject);
    }

    private GameObject GetRandomBuilding() { return _buildingPrefabs[Random.Range(0, _buildingPrefabs.Count)]; }
}