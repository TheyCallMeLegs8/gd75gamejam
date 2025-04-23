using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHistoryData", menuName = "Data/PlayerHistoryData")]
public class PlayerHistoryData : ScriptableObject
{
    public Dictionary<string, PlayerData> Players { get; private set; } = new Dictionary<string, PlayerData>();

    private string _currentPlayerName = null;

    public void AddOrLoginPlayer(string name)
    {
        if (Players.ContainsKey(name))
        {
            _currentPlayerName = name;
        }
        else
        {
            Players.Add(name, new PlayerData(0f));
        }
    }

    public void UpdatePlayerScore(float score)
    {
        if (Players.ContainsKey(_currentPlayerName) && score > Players[_currentPlayerName].score) Players[_currentPlayerName] = new PlayerData(score);
    }

    public void ClearHistory()
    {
        Players.Clear();
        _currentPlayerName = null;
    }
}

public struct PlayerData
{
    public PlayerData(float newScore)
    {
        score = newScore;
    }
    public float score;
}