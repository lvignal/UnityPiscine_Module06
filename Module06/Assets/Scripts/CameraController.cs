using Cinemachine;
using UnityEngine;

namespace Module06.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera _fpsCamera;
        [SerializeField] CinemachineVirtualCamera _tpsCamera;
        
        public bool IsFpsActive => _fpsCamera.gameObject.activeSelf;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; // hide cursor
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _fpsCamera.gameObject.SetActive(!_fpsCamera.gameObject.activeSelf);
                _tpsCamera.gameObject.SetActive(!_tpsCamera.gameObject.activeSelf);
            }
        }
    }
}