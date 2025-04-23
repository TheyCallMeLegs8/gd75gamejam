using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(VerticalLayoutGroup))]
public class Leaderboard : MonoBehaviour
{
    [SerializeField] private PlayerHistoryData _history;

    [SerializeField] private GameObject _playerSlot;

    private void SetupBoard()
    {
        
    }
}