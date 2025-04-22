using UnityEngine;
using UnityEngine.InputSystem;
using PrimeTween;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent _onRunStart;
    [SerializeField] private UnityEvent _onDeath;

    [Header("Components")]
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private PlayerInput _playerInput;

    [Header("Groundcheck")]
    [SerializeField] Vector3 _raycastDirection = new Vector3(0, -1, 0);
    [SerializeField] float _raycastLength = 3f;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] private Vector3 _frontCheckOffset;
    [SerializeField] private Vector3 _backCheckOffset;
    private bool _isGrounded = false;
    private bool _wasGrounded;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _maxJumpCount = 1;
    private int _jumpCounter = 0;
    private bool _hasLanded = true;
    private bool _canJump = true;
    [SerializeField] private float _jumpForce = 10f;
    private Tween _rightMovementTween;
    private Tween _leftMovementTween;
    [SerializeField] private List<float> _movePoints;
    [SerializeField] private int _startingIndex = 1;
    [SerializeField] private int _currentIndex = 0;

    [Header("Slide")]
    [SerializeField] private float _aerialSlideSpeed = 10f;
    [SerializeField] private float _slideDuration = 1f;
    [SerializeField] private int _slideLayerIndex = 7;

    [Header("Super Mode")]
    [SerializeField] private float _superDuration = 6f;
    [SerializeField] private float _superHeight = 10f;
    [SerializeField] private float _superRiseSpeed = 5f;
    private Tween _superMovementTween;
    private bool _isSuper = false;

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
        TryJump();
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

    public void OnSlide()
    {
        if (_isSuper) return;

        DoSlide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ActivateSuperMode();
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private bool TryJump()
    {
        if (!_canJump) return false;

        if (_isGrounded)
        {
            _jumpCounter = 0;
        }

        // if the player is grounded and has enough jumps then they may jump
        if (_isGrounded || _jumpCounter < _maxJumpCount)
        {
            _jumpCounter++;
            DoJump();
            return true;
        }

        return false;
    }

    private void DoJump()
    {
        _rigidBody.linearVelocity = new Vector3(_rigidBody.linearVelocity.x, _jumpForce, _rigidBody.linearVelocity.z);
    }

    protected bool GroundCheck()
    {
        _wasGrounded = _isGrounded;

        // shoots a raycast from front back and center
        if (Physics.Raycast(transform.position + _frontCheckOffset, _raycastDirection, out RaycastHit frontHitInfo, _raycastLength, _groundLayer) || Physics.Raycast(transform.position + _backCheckOffset, _raycastDirection, out RaycastHit backHitInfo, _raycastLength, _groundLayer) || Physics.Raycast(transform.position, _raycastDirection, out RaycastHit centerHitInfo, _raycastLength, _groundLayer))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
            _hasLanded = false;
        }

        if(!_wasGrounded && _isGrounded)
        {
            Landed();
        }

        return _isGrounded;
    }

    private void DoSlide()
    {
        if (!_isGrounded)
        {
            _rigidBody.linearVelocity = new Vector3(_rigidBody.linearVelocity.x, -_aerialSlideSpeed, _rigidBody.linearVelocity.z);
        }
        if (_isGrounded)
        {
            StartCoroutine(SlideRoutine());
        }
    }

    private IEnumerator SlideRoutine()
    {
        float time = 0f;

        gameObject.layer = _slideLayerIndex;
        foreach (Transform childTransform in gameObject.transform)
        {
            childTransform.gameObject.layer = _slideLayerIndex;
        }

        while (time < _slideDuration)
        {
            time += Time.deltaTime;

            yield return null;
        }

        gameObject.layer = LayerMask.NameToLayer("Player");
        foreach (Transform childTransform in gameObject.transform)
        {
            childTransform.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void Landed()
    {
        _hasLanded = true;
    }

    public void ActivateSuperMode()
    {
        StartCoroutine(SuperMode());

        _rigidBody.linearVelocity = Vector3.zero;

        _isSuper = true;
    }

    private IEnumerator SuperMode()
    {
        gameObject.layer = LayerMask.NameToLayer("Power");
        foreach (Transform childTransform in gameObject.transform)
        {
            childTransform.gameObject.layer = LayerMask.NameToLayer("Power");
        }

        _rigidBody.useGravity = false;

        float currentY = gameObject.transform.localPosition.y;
        float targetY = _superHeight;
        float distance = Mathf.Abs(targetY - currentY);

        float duration = distance / _superRiseSpeed;

        _superMovementTween = Tween.LocalPositionY(gameObject.transform, _superHeight, duration);

        float time = 0f;

        while(time < _superDuration)
        {
            time += Time.deltaTime;

            yield return null;
        }

        _rigidBody.useGravity = true;

        _isSuper = false;

        gameObject.layer = LayerMask.NameToLayer("Player");
        foreach (Transform childTransform in gameObject.transform)
        {
            childTransform.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void Teleport(Vector3 teleportPoint)
    {
        transform.position = teleportPoint;
        _rigidBody.position = teleportPoint;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position + _frontCheckOffset, _raycastDirection, Color.yellow);
        Debug.DrawRay(transform.position + _backCheckOffset, _raycastDirection, Color.yellow);
        Debug.DrawRay(transform.position, _raycastDirection, Color.yellow);
    }

    public void Die()
    {
        Debug.Log("<color=red>Player killed</color>");
        _onDeath.Invoke();
    }
}
