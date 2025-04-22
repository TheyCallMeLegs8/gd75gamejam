using UnityEditor;
using UnityEngine;

public class QuitHandler : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
