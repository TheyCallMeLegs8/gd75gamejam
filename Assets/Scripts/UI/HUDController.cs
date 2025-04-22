using System.Collections;
using PrimeTween;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private float _majorTick;
    [SerializeField] private float _minorTick;

    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private ShakeSettings _majorTickScaleSettings;
    [SerializeField] private ShakeSettings _minorTickScaleSettings;

    public float Time => _currentTime - _startTime;

    private float _startTime;
    private float _currentTime;

    private float _currentMajorCount = 0f;
    private float _currentMinorCount = 0f;

    private bool _isPlaying = false;

    private void Update()
    {
        if (_isPlaying)
        {
            CheckForMilestones();
            _scoreText.text = GetScoreString();
            _currentTime += UnityEngine.Time.deltaTime;
            _currentMajorCount += UnityEngine.Time.deltaTime;
            _currentMinorCount += UnityEngine.Time.deltaTime;
        }
    }

    private void CheckForMilestones()
    {
        if (_currentMajorCount > _majorTick)
        {
            _currentMajorCount -= _majorTick;
            Tween.ShakeScale(_scoreText.transform, _majorTickScaleSettings);
        }
        if (_currentMinorCount > _minorTick)
        {
            _currentMinorCount -= _minorTick;
            Tween.ShakeScale(_scoreText.transform, _minorTickScaleSettings);
        }
    }

    public void StartScoring()
    {
        _isPlaying = true;
        _startTime = 0f;
        _currentTime = 0f;
    }

    public void StopScoring()
    {
        _isPlaying = false;
    }

    public float GetScore(bool stopScoring = false)
    {
        return Time;
    }

    private string GetScoreString()
    {
        string score = Time.ToString();
        string[] parts = score.Split(".");
        return parts[0];
    }
}