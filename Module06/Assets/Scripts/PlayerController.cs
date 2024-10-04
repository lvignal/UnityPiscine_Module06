using System;
using TMPro;
using UnityEngine;

namespace Module06.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _numberOfKeysText;
        [SerializeField] private AudioSource _walkSound;
        [SerializeField] private AudioSource _collectKeySound;
        
        private Animator _animator;
        private Rigidbody _rigidbody;
        
        private float _moveSpeed = 2f;
        private float _rotationSpeed = 120f;
        private Vector3 _initialPosition;
        private Vector3 _initialRotation;
        
        public int NumberOfKeys { get; private set; }

        public Action OnWin;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _walkSound = GetComponent<AudioSource>();
            _initialPosition = transform.position;
        }

        void FixedUpdate()
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            
            if (GameManager.Instance.CameraController.IsFpsActive)
                ManageMovementWithFps();
            else
                ManageMovementWithTps();
            
            // "walk" animation or "idle" aimation
            _animator.SetFloat("move", Mathf.Abs(verticalInput));
            
            if (verticalInput > 0.1f && !_walkSound.isPlaying)
                _walkSound.Play();
            else if (verticalInput < 0.1f && _walkSound.isPlaying)
                _walkSound.Stop();
        }

        private void ManageMovementWithFps()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            
            Vector3 forward = GameManager.Instance.CameraController.transform.forward;
            Vector3 right = GameManager.Instance.CameraController.transform.right;
            
            forward.y = 0f;
            right.y = 0f;
            
            forward.Normalize();
            right.Normalize();
            
            Vector3 moveDirection = (forward * verticalInput + right * horizontalInput).normalized;
            _rigidbody.MovePosition(_rigidbody.position + moveDirection * _moveSpeed * Time.deltaTime);
                
            if (moveDirection != Vector3.zero)
                transform.forward = moveDirection;
        }
        
        private void ManageMovementWithTps()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            
            if (horizontalInput != 0)
                _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, horizontalInput * _rotationSpeed * Time.deltaTime, 0));
            
            if (verticalInput != 0)
            {
                Vector3 movement = transform.forward * verticalInput * _moveSpeed * Time.deltaTime;
                _rigidbody.MovePosition(_rigidbody.position + movement);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Key"))
            {
                _collectKeySound.Play();
                NumberOfKeys++;
                _numberOfKeysText.text = NumberOfKeys + " / 3";
                other.gameObject.SetActive(false);
            }

            if (other.CompareTag("WinWardrobe"))
                OnWin?.Invoke();
        }

        public void Reset()
        {
            transform.position = _initialPosition;
            transform.eulerAngles = _initialRotation;
            NumberOfKeys = 0;
            _numberOfKeysText.text = NumberOfKeys + " / 3";
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
            _animator.SetFloat("move", 0);
            _walkSound.Stop();
        }
    }
}
