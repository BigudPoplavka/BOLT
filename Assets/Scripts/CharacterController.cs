using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PhotonView _photonView;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _feet;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private List<Animator> _animators;

    [SerializeField] private float _speed;
    [SerializeField] private float _runSpeedMultiplayer;
    [SerializeField] private float _currSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckDistance;

    [SerializeField] private Vector3 _movementInput;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private Vector3 _groundCheckBoxSize;

    private void Start()
    {
        _rigidbody = _player.gameObject.GetComponent<Rigidbody>();
        _photonView = _player.gameObject.GetComponent<PhotonView>();

        if (!_photonView.IsMine)
        {
            Destroy(_rigidbody);
        }

        _currSpeed = _speed;
    }

    private void Update()
    {
        if(!_photonView.IsMine)
        {
            return;
        }

        GetInputs();
        SetupState();
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        if (!_photonView.IsMine)
        {
            return;
        }
    }

    private void GetInputs()
    {
        _velocity = _rigidbody.velocity;
        _movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void SetupState()
    {
        if (PlayerIsNotMoving())
        {
            _player.StateMachine.SetState(_player.Idle);
        }
    }

    private void Move()
    {
        if (IsGrounded())
        {

            Vector3 movement = transform.TransformDirection(_movementInput) * _currSpeed;
            _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);

            if (_rigidbody.velocity != Vector3.zero)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                   Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    _currSpeed = _speed;
                    _player.StateMachine.SetState(_player.Walk);
                }

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
                {
                    _currSpeed = _speed + _runSpeedMultiplayer;
                    _player.StateMachine.SetState(_player.Run);
                }
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position - transform.up * _groundCheckDistance, _groundCheckBoxSize);
    }

    private bool IsGrounded()
    {
        return Physics.BoxCast(transform.position,
                        _groundCheckBoxSize,
                        -transform.up,
                        transform.rotation,
                        _groundCheckDistance,
                        _groundLayerMask);
    }

    public bool PlayerIsNotMoving()
    {
        return _rigidbody.velocity.x == 0 && _rigidbody.velocity.z == 0;
    }
}
