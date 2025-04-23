using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
    [field: SerializeField] public List<string> SceneList = new List<string>();

    [SerializeField] private bool _loadAllOnEnable = true;
    [SerializeField] private bool _unloadAllOnDisable = true;

    private void OnEnable()
    {
        if (_loadAllOnEnable) LoadScenes();
    }

    private void OnDisable()
    {
        if (_unloadAllOnDisable) UnloadScenes();
    }

    public void LoadScenes()
    {
        if (SceneList.Count <= 0) return;
        for (int i = 0; i < SceneList.Count; i++)
        {
            if (SceneManager.GetSceneByName(SceneList[i]).isLoaded) continue;
            SceneManager.LoadScene(SceneList[i], LoadSceneMode.Additive);
        }
    }

    public void UnloadScenes()
    {
        if (SceneList.Count <= 0) return;
        for (int i = 0; i < SceneList.Count; i++)
        {
            if (!SceneManager.GetSceneByName(SceneList[i]).isLoaded) continue;
            SceneManager.UnloadSceneAsync(SceneList[i]);
        }
    }
}