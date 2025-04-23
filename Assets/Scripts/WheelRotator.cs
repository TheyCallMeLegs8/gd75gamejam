using UnityEngine;
using System.Collections;

public class WheelRotator : MonoBehaviour
{
    [Header("Wheels")]
    [SerializeField] GameObject _frontWheel;
    [SerializeField] GameObject _backWheel;

    [Header("Stats")]
    [SerializeField] private float _rotationSpeed = -500;

    private void Awake()
    {
        StartCoroutine(RotationRoutine());
    }

    private IEnumerator RotationRoutine()
    {
        while (true)
        {
            float deltaRotation = _rotationSpeed * Time.deltaTime;

            _frontWheel.transform.Rotate(0, 0, deltaRotation);
            _backWheel.transform.Rotate(0, 0, deltaRotation);

            yield return null;
        }
    }
}
