using UnityEngine;
using UnityEngine.InputSystem;
using PrimeTween;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private PlayerInput _playerInput;

    [Header("Groundcheck")]
    [SerializeField] Vector3 _raycastDirection = new Vector3(0, -1, 0);
    [SerializeField] float _raycastLength = 3f;
    [SerializeField] LayerMask _groundLayer;
    private bool _isGrounded = false;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    private Tween _rightMovementTween;
    private Tween _leftMovementTween;
    [SerializeField] private List<float> _movePoints;
    [SerializeField] private int _startingIndex = 1;
    [SerializeField] private int _currentIndex = 0;

    private void OnValidate()
    {
        if(_rigidBody == null) _rigidBody = GetComponent<Rigidbody>();
        if(_playerInput == null) _playerInput = GetComponent<PlayerInput>();
    }

    private void Awake()
    {
        _currentIndex = _startingIndex;
        Teleport(new Vector3(_movePoints[_startingIndex], 0, 0));
    }

    public void OnJump()
    {
        _rigidBody.AddForce(new Vector3(0, _jumpForce, 0));
    }

    public void OnLeft()
    {
        if (_currentIndex - 1 < 0) return;

        _currentIndex--;

        float currentX = gameObject.transform.localPosition.x;
        float targetX = _movePoints[_currentIndex];
        float distance = Mathf.Abs(targetX - currentX);

        // Calculate dynamic duration
        float duration = distance / _moveSpeed;

        _rightMovementTween.Stop();
        _leftMovementTween.Stop();
        _leftMovementTween = Tween.LocalPositionX(gameObject.transform, _movePoints[_currentIndex], duration);
    }

    public void OnRight()
    {
        if (_currentIndex + 1 == _movePoints.Count) return;

        _currentIndex++;

        float currentX = gameObject.transform.localPosition.x;
        float targetX = _movePoints[_currentIndex];
        float distance = Mathf.Abs(targetX - currentX);

        float duration = distance / _moveSpeed;

        _leftMovementTween.Stop();
        _rightMovementTween.Stop();
        _rightMovementTween = Tween.LocalPositionX(gameObject.transform, _movePoints[_currentIndex], duration);
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawRay(transform.position, _raycastDirection, Color.yellow);
    }

    protected bool GroundCheck()
    {
        if (Physics.Raycast(transform.position, _raycastDirection, out RaycastHit hitInfo, _raycastLength, _groundLayer))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        return _isGrounded;
    }

    private void Teleport(Vector3 teleportPoint)
    {
        transform.position = teleportPoint;
        _rigidBody.position = teleportPoint;
    }
}
