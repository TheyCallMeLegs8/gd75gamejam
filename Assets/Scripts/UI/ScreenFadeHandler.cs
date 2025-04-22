using System.Collections;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeHandler : MonoBehaviour
{
    [SerializeField] private bool _fadedOnStart;
    [SerializeField] private TweenSettings<float> _fadeInSettings;
    [SerializeField] private TweenSettings<float> _fadeOutSettings;

    private Image _image;
    private Tween _fadeTween;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        if (_fadedOnStart)
        {
            Color color = _image.color;
            color.a = 1f;
            Debug.Log(color.a);
            _image.color = color;
        }
    }

    public Coroutine FadeIn()
    {
        return StartCoroutine(FadeSequence(_fadeInSettings));
    }

    public Coroutine FadeOut()
    {
        return StartCoroutine(FadeSequence(_fadeOutSettings));
    }

    private IEnumerator FadeSequence(TweenSettings<float> settings)
    {
        _fadeTween.Stop();
        yield return Tween.Alpha(_image, settings).ToYieldInstruction();
    }
}