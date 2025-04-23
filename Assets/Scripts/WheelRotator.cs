using UnityEngine;
using PrimeTween;
using System.Collections;

public class WheelRotator : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _rotationSpeed = -500;

    private Coroutine _wheelRoutine;

    private void Awake()
    {
        _wheelRoutine = StartCoroutine(RotationRoutine());
    }

    private IEnumerator RotationRoutine()
    {
        while (true)
        {
            float deltaRotation = _rotationSpeed * Time.deltaTime;

            transform.Rotate(0, 0, deltaRotation);

            yield return null;
        }
    }
}
