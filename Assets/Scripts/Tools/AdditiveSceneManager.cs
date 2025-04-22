using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
    [field: SerializeField] public List<SceneAsset> SceneList = new List<SceneAsset>();

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
        for (int i = 0; i < SceneList.Count; i++)
        {
            if (SceneManager.GetSceneByName(SceneList[i].name).isLoaded) continue;
            SceneManager.LoadScene(SceneList[i].name, LoadSceneMode.Additive);
        }
    }

    public void UnloadScenes()
    {
        for (int i = 0; i < SceneList.Count; i++)
        {
            if (!SceneManager.GetSceneByName(SceneList[i].name).isLoaded) continue;
            SceneManager.UnloadSceneAsync(SceneList[i].name);
        }
    }
}