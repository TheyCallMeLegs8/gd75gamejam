using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHistoryData", menuName = "Data/PlayerHistoryData")]
public class PlayerHistoryData : ScriptableObject
{
    private Dictionary<string, PlayerData> _players = new Dictionary<string, PlayerData>();

    private string _currentPlayerName = null;

    public void AddOrLoginPlayer(string name)
    {
        if (_players.ContainsKey(name))
        {
            _currentPlayerName = name;
        }
        else
        {
            _players.Add(name, new PlayerData(0f));
        }
    }

    public void UpdatePlayerScore(float score)
    {
        if (_players.ContainsKey(_currentPlayerName) && score > _players[_currentPlayerName].score) _players[_currentPlayerName] = new PlayerData(score);
    }

    public void ClearHistory()
    {
        _players.Clear();
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