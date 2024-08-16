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
        
        private float _moveSpeed = 4f;
        private float _rotationSpeed = 100f;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z) && GameManager.Instance.CameraController.IsFpsActive)
            {
                Vector3 forwardDirection = GameManager.Instance.CameraController.Forward;
                forwardDirection.y = 0;
                forwardDirection.Normalize();
                _rigidbody.MovePosition(_rigidbody.position + forwardDirection * _moveSpeed * Time.deltaTime);
            }
        }

        void FixedUpdate()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            
            if (horizontalInput != 0)
            {
                _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, horizontalInput * _rotationSpeed * Time.deltaTime, 0));
            }
            
            Vector3 movement = transform.forward * verticalInput * _moveSpeed * Time.deltaTime;
            _rigidbody.MovePosition(_rigidbody.position + movement);
            
            // "walk" animation or "idle" aimation
            _animator.SetFloat("move", /*movement.magnitude*/Mathf.Abs(verticalInput));
            
            if (movement.magnitude > 0)
                _walkSound.Play();
            else
                _walkSound.Stop();
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
