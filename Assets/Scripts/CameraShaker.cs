using UnityEngine;
using PrimeTween;
using System.Collections;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Vector3 _shakeForce = new Vector3(5f, 5f, 5f);
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _frequency = 5f;

    private Coroutine _cameraRoutine;

    private void Start()
    {
        StartCoroutine(ShakeCameraRoutine());
    }

    private IEnumerator ShakeCameraRoutine()
    {
        while (true)
        {
            yield return Tween.ShakeLocalPosition(transform, _shakeForce, _duration, _frequency).ToYieldInstruction();
        }
    }
}
