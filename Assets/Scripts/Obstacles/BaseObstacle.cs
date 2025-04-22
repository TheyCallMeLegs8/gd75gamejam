using System.Collections;
using PrimeTween;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    [SerializeField] private TweenSettings _settings;

    private void Start()
    {
        StartCoroutine(TravelSequence());
        OnObstacleStart();
    }

    private IEnumerator TravelSequence()
    {
        yield return Tween.Position(transform, GetTargetPosition(), _settings).ToYieldInstruction();
        Destroy(gameObject);
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.z = 0;
        return targetPosition;
    }

    protected virtual void OnObstacleStart() { }
}