using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    [Header("Tuning")]
    [field: SerializeField] public int LaneCount { get; private set; } = 3;
    [field: SerializeField] public float LaneWidth { get; private set; } = 3f;
    [field: SerializeField] public float TrackLength { get; private set; } = 100f;

    [Header("Encounters")]
    [SerializeField] private List<EncounterData> _encounters;

    [Header("References")]
    [SerializeField] private Transform _trackTransform;
    [SerializeField] private GameObject _spawnerPrefab;

    private List<SpawnHandler> _spawnPoints = new List<SpawnHandler>();

    private Coroutine _gameLoopCoroutine;

    private void Awake()
    {
        SetupTrack();
    }

    private void Start()
    {
        _gameLoopCoroutine = StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        // Initialize the first encounter
        yield return EncounterSequence(_encounters[0]);

        // Execute main loop
        while (true)
        {
            // Randomly choose next encounter
            int next = Random.Range(0, _encounters.Count);
            yield return EncounterSequence(_encounters[next]);
        }
    }

    private IEnumerator EncounterSequence(EncounterData data)
    {
        // Iterate through the list of obstacles in the thing
        for (int i = 0; i < data.Obstacles.Count; i++)
        {
            yield return new WaitForSeconds(data.TimeOffset[i]);
            int laneIndex = data.Lanes[i];
            ObstacleType obstacle = data.Obstacles[i];
            _spawnPoints[laneIndex].SpawnObjectOfType(obstacle);
        }
    }

    private void SetupTrack()
    {
        // Set the width of the track
        float totalWidth = LaneCount * LaneWidth;
        Vector3 initialScale = new Vector3(totalWidth / 10, 1f, TrackLength / 10);
        _trackTransform.localScale = initialScale;

        // Center the track
        _trackTransform.position = new Vector3(0f, 0f, TrackLength * 0.5f);

        // Establish spawn points
        for (int i = 0; i < LaneCount; i++)
        {
            // Instantiate the spawner object
            GameObject spawner = Instantiate(_spawnerPrefab);
            if (spawner.TryGetComponent(out SpawnHandler sh))
            {
                _spawnPoints.Add(sh);
            }
            else Debug.LogError("Incorrect prefab assigned");

            // Repositionthe spawner
            spawner.transform.position = new Vector3((-totalWidth / 2) + LaneWidth * i + (LaneWidth / 2f), 0f, TrackLength);
        }
    }
}