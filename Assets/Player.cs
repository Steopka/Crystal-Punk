using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed, _jumpForce, _playerHeight;
    [SerializeField]
    private LayerMask _GroundLayer;
    private bool _isGrounded, _readyToJumpp = true;

    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    [Header("Input"), SerializeField]
    InputActionAsset InputActions;
    InputAction moveAction;
    InputAction jumpAction;


    private void Start()
    {
    }
    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _isGrounded = Physics2D.Raycast(_rb.position, Vector2.down, _playerHeight / 2 + 0.2f, _GroundLayer);
        _moveDirection = moveAction.ReadValue<Vector2>();
        if (jumpAction.IsPressed() && _isGrounded && _readyToJumpp )
        {
            Jump();
        }

    }
    void Jump()
    {
        _readyToJumpp = false;
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
        _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        Invoke(nameof(ResetJump), 0.3f);
    }
    private void ResetJump()
    {
        _readyToJumpp = true;
    }
    void Move()
    {
        _rb.AddForce(_moveDirection.normalized * _moveSpeed, ForceMode2D.Force);
        SpeedControl();
    }
    void SpeedControl()
    {
        Vector2 flatvel = new Vector3(_rb.linearVelocity.x, 0);
        if (flatvel.magnitude > _moveSpeed)
        {
            Vector3 limitVel = flatvel.normalized * _moveSpeed;
            _rb.linearVelocity = new Vector3(limitVel.x, _rb.linearVelocity.y);
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
}