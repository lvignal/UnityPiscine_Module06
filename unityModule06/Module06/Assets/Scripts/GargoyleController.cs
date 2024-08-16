using UnityEngine;

namespace Module06.Enemy
{
    public class GargoyleController : MonoBehaviour
    {
        [SerializeField] private EnemyDetectionZone _detectionZone;
        
        private void Start()
        {
            //_detectionZone.OnPlayerDetected += GameManager.Instance.TellGhostsToChasePlayer;
        }
    }
}