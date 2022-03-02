using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _timeToReachJumpApex = 2f;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _velocity;
    private float _jumpForce;
    private float _jumpGravity;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool _jump;
    private bool _grounded;
//math.sqrt(2*g*jumpHeight)
    private static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
    private static readonly int Airborne = Animator.StringToHash("Airborne");
    private static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");
    [SerializeField] private bool _rightFacingSprite = true;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _jumpGravity = (2 * _jumpHeight) / Mathf.Pow (_timeToReachJumpApex, 2);
        _jumpForce = Mathf.Sqrt(2 * Mathf.Abs(_jumpGravity) * _jumpHeight);
    }

    private void Update()
    {
        CalculateState();
        CalculateVelocity();
        UpdateAnimator();
        Move();
        ResetState();
    }


    private void CalculateState()
    {
        var contactPoints = new List<ContactPoint2D>();
        int count = _rigidbody2D.GetContacts(contactPoints);

        foreach (var point in contactPoints)
        {
            if (Vector2.Dot(point.normal, Vector2.up) > 0.8)
            {
                _grounded = true;
            }
        }
    }

    private void ResetState()
    {
        _jump = false;
        _grounded = false;
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat(HorizontalSpeed, Mathf.Abs(_velocity.x));
        _animator.SetBool(Airborne, !_grounded);
        _animator.SetFloat(VerticalSpeed, _velocity.y);

        if (_velocity.x != 0)
            _spriteRenderer.flipX = _rightFacingSprite ? _velocity.x < 0 : _velocity.x > 0;
    }

    private void CalculateVelocity()
    {
        ApplyGravity();

        if (_jump && _grounded)
        {
            Jump();
            _jump = false;
        }
        else
        {
            if (_grounded && _velocity.y < 0)
                _velocity.y = Physics2D.gravity.y * Time.deltaTime;
        }
    }

    private void ApplyGravity()
    {
        _velocity += (Vector2.down * _jumpGravity) * Time.deltaTime;
    }

    private void Jump()
    {
        _velocity.y = _jumpForce;
    }

    private void Move()
    {
        _rigidbody2D.velocity = _velocity;
    }

    public void HandleDirectionalInput(InputAction.CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        _velocity.x = direction.x * _walkSpeed;
    }

    public void HandleFireInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            _jump = true;
    }
}
