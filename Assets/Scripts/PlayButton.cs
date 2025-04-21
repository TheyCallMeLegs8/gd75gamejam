using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Debug.Log($"<color=green>Loading Scene: {sceneName}</color>");
        SceneManager.LoadScene(sceneName);
    }
}