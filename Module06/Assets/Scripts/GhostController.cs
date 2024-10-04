using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Module06.Enemy
{
    public class GhostController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private EnemyDetectionZone _detectionZone;
        [SerializeField] private AudioSource _sound;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Vector3 _initialPosition;
        private Vector3 _initialRotation;
        
        private bool _isChasingPlayer;
        private float _chasingDuration = 15f;
        public Action OnPlayerReached;
        
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _initialPosition = transform.position;
            _initialRotation = transform.eulerAngles;
            _detectionZone.OnPlayerDetected += PlaySound;
            _detectionZone.OnPlayerDetected += StartChasePlayer;
            _detectionZone.OnPlayerLost += StopSound;
        }

        public void StartChasePlayer()
        {
            if (!_isChasingPlayer)
                StartCoroutine(ChasePlayer());
        }

        private void Update()
        {
            _animator.SetFloat("move", _navMeshAgent.velocity.magnitude);
            if (_isChasingPlayer)
            {
                _navMeshAgent.SetDestination(_player.transform.position);
                
                // if near player, game over
                if (Vector3.Distance(transform.position, _player.transform.position) < 0.5f)
                    OnPlayerReached?.Invoke();
            }
            
            else
            {
                _navMeshAgent.SetDestination(_initialPosition);
                
                // if near initial position, reset
                if (_navMeshAgent.remainingDistance < 0.1f)
                    Reset();
            }
            
        }

        private IEnumerator ChasePlayer()
        {
            _isChasingPlayer = true;
            yield return new WaitForSeconds(_chasingDuration);
            _isChasingPlayer = false;
        }

        public void Reset()
        {
            transform.position = _initialPosition;
            transform.eulerAngles = _initialRotation;
            _isChasingPlayer = false;
            _navMeshAgent.SetDestination(_initialPosition);
            _animator.SetFloat("move", 0);
            enabled = true;
        }
        
        private void PlaySound()
        {
            _sound.Play();
        }
        
        private void StopSound()
        {
            _sound.Stop();
        }

        public void Disable()
        {
            StopSound();
            enabled = false;
            _animator.SetFloat("move", 0);
        }

        private void OnDestroy()
        {
            _detectionZone.OnPlayerDetected -= StartChasePlayer;
            _detectionZone.OnPlayerDetected -= PlaySound;
            _detectionZone.OnPlayerLost -= StopSound;
        }
    }
}