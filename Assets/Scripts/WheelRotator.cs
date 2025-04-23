using UnityEngine;
using PrimeTween;
using System.Collections;

public class WheelRotator : MonoBehaviour
{
    [Header("Wheels")]
    [SerializeField] private GameObject _frontWheel;
    [SerializeField] private GameObject _backWheel;

    [Header("Stats")]
    [SerializeField] private float _rotationSpeed;

    private Coroutine _wheelRoutine;

    private void Awake()
    {
        //_wheelRoutine = StartCoroutine(RotationRoutine());
    }

    private IEnumerator RotationRoutine()
    {
        float time = 0f;
        while (true)
        {
            time += Time.deltaTime;

            _frontWheel.transform.Rotate(0, _rotationSpeed * time, 0);
            _backWheel.transform.Rotate(0, _rotationSpeed * time, 0);
        }
    }
}
