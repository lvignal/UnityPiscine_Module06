using System;
using Module06.Player;
using UnityEngine;

namespace Module06
{
    public class DoorTriggerZone : MonoBehaviour
    {
        [SerializeField] private GameObject _pivot;
        [SerializeField] private bool _needKeysToOpen = false;
        
        private bool _isOpened = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_isOpened)
            {
                if (!_needKeysToOpen)
                {
                    _pivot.transform.Rotate(0, -90, 0);
                    _isOpened = true;
                }
                else
                {
                    PlayerController player = other.GetComponent<PlayerController>();
                    if (player.NumberOfKeys == 3)
                    {
                        _pivot.transform.Rotate(0, -90, 0);
                        _isOpened = true;
                    }
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && _isOpened)
            {
                _pivot.transform.Rotate(0, 90, 0);
                _isOpened = false;
            }
        }
    }
}