using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHistoryData", menuName = "Scriptable Objects/PlayerHistoryData")]
public class PlayerHistoryData : ScriptableObject
{
    private List<PlayerData> _data = new List<PlayerData>();

    public void AddPlayer(string name)
    {
        
    }

    public void UpdatePlayerScore(string name, float score)
    {
        for (int i = 0; i < _data.Count; i++)
        {
            if (_data[i].name == name)
            {
            }
        }
    }

    public void ClearHistory()
    {

    }
}

public struct PlayerData
{
    public string name;
    public float score;
}