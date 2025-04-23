using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(VerticalLayoutGroup))]
public class Leaderboard : MonoBehaviour
{
    [SerializeField] private PlayerHistoryData _history;

    [SerializeField] private GameObject _playerSlot;

    public void SetupBoard()
    {
        Debug.LogError(_history.Players.Count);

        if(_history.Players.Count != 0)
        {
            string[] names = _history.Players.Keys.ToArray();
            Debug.LogError(_history.Players[names[0]].score);
        }

        // Order the list
        List<string> players = _history.Players.Keys.ToList();
        players = GetOrderedList(players);

        for (int i = 0; i < players.Count; i++)
        {
            if (Instantiate(_playerSlot, transform).TryGetComponent(out ProfileSlot slot))
            {
                slot.SetRank((i + 1).ToString());
                slot.SetName(players[i].ToString());
                slot.SetScore(_history.Players[players[i]].score.ToString());
            }
        }
    }

    private List<string> GetOrderedList(List<string> players)
    {
        int check = 0;
        while (true)
        {
            check = 0;
            for (int i = 0; i < players.Count; i++)
            {
                if (i + 1 >= players.Count) continue;
                if (_history.Players[players[i]].score < _history.Players[players[i + 1]].score)
                {
                    string temp = players[i];
                    players[i] = players[i + 1];
                    players[i + 1] = temp;
                    check++;
                }
            }
            if (check == 0) break;
        }
        return players;
    }
}