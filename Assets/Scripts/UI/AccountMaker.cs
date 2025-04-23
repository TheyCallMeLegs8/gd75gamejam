using TMPro;
using UnityEngine;

public class AccountMaker : MonoBehaviour
{
    [SerializeField] private PlayerHistoryData _playerHistoryData;
    [SerializeField] private TextMeshProUGUI _text;

    public void CreateUser(string name)
    {
        Debug.Log("Creating user profile");
        _playerHistoryData.AddOrLoginPlayer(name);
        //_text.text = "PLAYING AS: " + name;
    }
}