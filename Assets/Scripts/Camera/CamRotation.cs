using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 180f;
    [SerializeField] private Transform _playerTransform;

    private float _rotationX = 0f;
    private Vector3 _rotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        FollowTheCursor();
    }

    private void FollowTheCursor()
    {
        float mouseInputX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseInputY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _rotationX -= mouseInputY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        _playerTransform.Rotate(Vector3.up * mouseInputX);
    }
}
