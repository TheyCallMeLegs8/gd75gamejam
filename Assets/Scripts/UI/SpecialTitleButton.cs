using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialTitleButton : MonoBehaviour
{
    [SerializeField] private GameObject _titlePanel;
    [SerializeField] private List<GameObject> _otherPanels = new List<GameObject>();
    public void OnClick()
    {
        if (_titlePanel.activeSelf)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            _titlePanel.SetActive(true);
            for (int i = 0; i < _otherPanels.Count; i++)
            {
                _otherPanels[i].gameObject.SetActive(false);
            }
        }
    }
}
