using UnityEngine;
using UnityEngine.Events;

public class StartGameHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent _onGameStart;
    [SerializeField] private UnityEvent _onRunStart;

    [SerializeField] private ScreenFadeHandler _screenFadeHandler;

    private bool _isRunning = false;

    private void Start()
    {
        _onGameStart.Invoke();
        Time.timeScale = 0f;
        _screenFadeHandler.FadeIn();
    }

    private void Update()
    {
        if (!_isRunning && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            _onRunStart.Invoke();
        }
    }
}