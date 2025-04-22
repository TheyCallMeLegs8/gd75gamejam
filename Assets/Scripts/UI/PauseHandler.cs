using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    public bool IsPaused { get; private set; } = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            if (IsPaused) PauseGame();
            else ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming");
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        Debug.Log("Pausing");
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
    }
}