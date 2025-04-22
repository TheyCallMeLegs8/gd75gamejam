using UnityEngine;
using UnityEngine.Events;

public class EndgameHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEndgameTriggered;

    [SerializeField] private GameObject _endgamePanel;

    public void TriggerEndgame()
    {
        _endgamePanel.SetActive(true);
        _onEndgameTriggered.Invoke();
    }
}