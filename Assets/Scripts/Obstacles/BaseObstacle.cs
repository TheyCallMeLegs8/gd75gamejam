using System.Collections;
using PrimeTween;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseObstacle : MonoBehaviour
{
    [SerializeField] private TweenSettings _settings;
    [SerializeField] private bool _isLethal = true;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.isKinematic = true;
        StartCoroutine(TravelSequence());
        OnObstacleStart();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out PlayerController pc) && _isLethal)
        {
            pc.Die();
        }
    }

    private IEnumerator TravelSequence()
    {
        yield return Tween.Position(transform, GetTargetPosition(), _settings).ToYieldInstruction();
        Destroy(gameObject);
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.z = -10f;
        return targetPosition;
    }

    protected virtual void OnObstacleStart()
    { FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Train Horn"); }
}