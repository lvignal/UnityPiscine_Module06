using System;
using UnityEngine;

namespace Module06.Enemy
{
    public class EnemyDetectionZone : MonoBehaviour
    {
        public Action OnPlayerDetected;
        public Action OnPlayerLost;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerDetected?.Invoke();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerLost?.Invoke();
            }
        }
    }
}