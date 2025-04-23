using TMPro;
using UnityEngine;

public class ProfileSlot : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI Rank { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Name { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Score { get; private set; }

    public void SetRank(string rank)
    {
        Rank.text = rank;
    }

    public void SetName(string name)
    {
        Rank.text = name;
    }

    public void SetScore(string score)
    {
        Rank.text = score;
    }
}