using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : MonoBehaviour
    {
        private Player _player;
        [SerializeField] private CharacterController _controller;

        [Header("Attributes")]
        [SerializeField] private float speed;
        [SerializeField] private float gravity, jumpHeight, groundDistance;
        [SerializeField] private float mouseSensitivity, maxSlopeAngle;
        [SerializeField] private LayerMask groundMask;

        [HideInInspector] public bool Jump;

        private bool _isGrounded;
        private Vector3 velocity;

        private void Awake()
        {
            _player = GetComponent<Player>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private Vector3 _moveDir;
        private float _mouseX, _mouseY, _xRotation;

        private void Update()
        {
            _mouseX *= mouseSensitivity * Time.deltaTime;
            _mouseY *= mouseSensitivity * Time.deltaTime;
            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 45f);

            transform.GetChild(1).localRotation = Quaternion.Euler(_xRotation,0f,0f);
            transform.Rotate(Vector3.up * _mouseX);

            _isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

            if (_isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // if (IsOnSlope())
            // {
            //     _moveDir = GetSlopeMoveDirection() * speed * Time.deltaTime;
            // }
            _controller.Move(_moveDir * (speed * Time.deltaTime));

            if ((Input.GetButtonDown("Jump")) && _isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            _controller.Move(velocity * Time.deltaTime);
        }

        public void SetDir(Vector3 dir)
        {
            _moveDir = dir;
        }

        public void SetMouseX(float value)
        {
            _mouseX = value;
        }
        
        public void SetMouseY(float value)
        {
            _mouseY = value;
        }

        // private RaycastHit _slopeHit;
        // private bool IsOnSlope()
        // {
        //     if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, 1.3f))
        //     {
        //         float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
        //         return angle < maxSlopeAngle && angle != 0;
        //     }
        //
        //     return false;
        // }
        //
        // private Vector3 GetSlopeMoveDirection()
        // {
        //     return Vector3.ProjectOnPlane(_moveDir, _slopeHit.normal).normalized;
        // }
    }
}