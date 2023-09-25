using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private PhotoCapture photoCapture;
    private float _verticalInput;
    private float _horizontalInput;
    private bool _playerJumped = false;
    private bool _canJump = true;

    private Rigidbody _rigidBody;
    private Transform _transform;

    private float _xRotation;
    private float _yRotation;
    private float _camera_sensitivity = 15f;

    private const float _inputScale = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();

        // Centre cursor and lock it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photoCapture.isGameOver)
        {
            _verticalInput = Input.GetAxis("Vertical");
            _horizontalInput = Input.GetAxis("Horizontal");
            _playerJumped = Input.GetButton("Jump");

            _xRotation -= Input.GetAxis("Mouse Y") * _camera_sensitivity;
            _yRotation += Input.GetAxis("Mouse X") * _camera_sensitivity;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); // to stop the player from looking above/below 90

            transform.localEulerAngles = new Vector3(_xRotation, _yRotation, 0);
        }


    }

    private void FixedUpdate()
    {
        if (!photoCapture.isGameOver)
        {
            _rigidBody.velocity += _transform.forward * _verticalInput * _inputScale;
            _rigidBody.velocity += _transform.right * _horizontalInput * _inputScale;

            if (_playerJumped && _canJump)
            {
                _rigidBody.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
                _canJump = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canJump = true;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus && !photoCapture.isGameOver)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
