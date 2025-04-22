using System.Collections;
using PrimeTween;
using UnityEngine;

public class ButtonTriggerAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _normalScale = new Vector3();
    [SerializeField] private Vector3 _largeScale = new Vector3();
    [SerializeField] private TweenSettings _scaleSettings;

    private void Awake()
    {
        _normalScale = transform.localScale;
    }

    public void Expand()
    {
        StartCoroutine(ScaleSequence(_largeScale));
    }

    public void Retract()
    {
        StartCoroutine(ScaleSequence(_normalScale));
    }

    private IEnumerator ScaleSequence(Vector3 newScale)
    {
        Tween.ScaleX(transform, newScale.x, _scaleSettings);
        Tween.ScaleY(transform, newScale.y, _scaleSettings);
        yield return Tween.ScaleZ(transform, newScale.z, _scaleSettings).ToYieldInstruction();
    }
}