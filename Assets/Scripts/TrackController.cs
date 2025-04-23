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
    private EncounterData _currentEncounter;

    private void Awake()
    {
        SetupTrack();
        _currentEncounter = _encounters[0];
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

            //int next = Random.Range(0, _encounters.Count);

            EncounterData next = GetNextEncounter(_currentEncounter);
            
            Debug.Log("Starting new encounter at next = " + next);
            yield return EncounterSequence(next);
        }
    }

    private IEnumerator EncounterSequence(EncounterData data)
    {
        yield return new WaitForSeconds(data.BuildupTime);
        // Iterate through the list of obstacles in the thing
        for (int i = 0; i < data.Obstacles.Count; i++)
        {
            yield return new WaitForSeconds(data.TimeOffset[i]);
            int laneIndex = data.Lanes[i];
            ObstacleType obstacle = data.Obstacles[i];
            _spawnPoints[laneIndex].SpawnObjectOfType(obstacle);
        }
        yield return new WaitForSeconds(data.RecoveryTime);
    }

    private void SetupTrack()
    {
        // Set the width of the track
        float totalWidth = LaneCount * LaneWidth;
        Vector3 initialScale = new Vector3(totalWidth / 10, 1f, TrackLength / 10);
        _trackTransform.localScale = initialScale;

        // Center the track
        _trackTransform.position = new Vector3(0f, -1f, TrackLength * 0.5f - 10f);

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

            // Reposition the spawner
            spawner.transform.position = new Vector3((-totalWidth / 2) + LaneWidth * i + (LaneWidth / 2f), -1f, TrackLength);
        }
    }

    private EncounterData GetNextEncounter(EncounterData previousEncounter)
    {
        List<EncounterData> potentialNextEncounters = new List<EncounterData>();

        // Assemble a list of potential branches
        for (int i = 0; i < previousEncounter.ExitTypes.Count; i++)
        {   // For every exit type
            for (int j = 0; j < _encounters.Count; j++)
            {   // For every encounter
                for (int k = 0; k < _encounters[j].EntryTypes.Count; k++)
                {   // For every entry type
                    if (previousEncounter.ExitTypes[i] == _encounters[j].EntryTypes[k]) potentialNextEncounters.Add(_encounters[j]);
                }
            }
        }

        // Choose from the assembled list
        if (potentialNextEncounters.Count > 0)
        {
            int index = Random.Range(0, potentialNextEncounters.Count);
            Debug.Log("<color=green>Found viable encounter branch</color>");
            return potentialNextEncounters[index];
        }
        else if (_encounters.Count > 0)
        {
            int index = Random.Range(0, _encounters.Count);
            Debug.Log("<color=yellow>Defaulting to random branch</color>");
            return _encounters[index];
        }
        else
        {
            Debug.Log("<color=red>No encounters detected</color>");
            return null;
        }
    }
}