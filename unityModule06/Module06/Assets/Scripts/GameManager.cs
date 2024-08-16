using System.Collections.Generic;
using Module06.Camera;
using Module06.Enemy;
using Module06.Player;
using Module06.Screen;
using UnityEngine;

namespace Module06
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] public CameraController CameraController;
        [SerializeField] private List<GhostController> _ghostsList;
        [SerializeField] private List<GameObject> _keysList;
        [SerializeField] private EndScreen _endScreen;
        [SerializeField] private AudioSource _winSound;
        [SerializeField] private AudioSource _loseSound;
        
        private static GameManager _instance;
        public static GameManager Instance => _instance;
        
        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);

            _player.OnWin += LaunchWin;
            foreach (var ghost in _ghostsList)
            {
                ghost.OnPlayerReached += LaunchGameOver;
            }
        }
        
        public void TellGhostsToChasePlayer()
        {
            foreach (var ghost in _ghostsList)
            {
                ghost.StartChasePlayer();
            }
        }

        private void DisableCharacters()
        {
            foreach (var ghost in _ghostsList)
            {
                ghost.Disable();
            }
            _player.Disable();
        }
        
        private void LaunchGameOver()
        {
            DisableCharacters();
            _loseSound.Play();
            _endScreen.Display(false);
            _endScreen.OnFadeComplete += ResetGame;
        }
        
        private void ResetGame()
        {
            _endScreen.OnFadeComplete -= ResetGame;
            _player.Reset();
            foreach (var ghost in _ghostsList)
            {
                ghost.Reset();
            }
            
            foreach (var key in _keysList)
            {
                key.SetActive(true);
            }
        }
        
        private void LaunchWin()
        {
            DisableCharacters();
            _winSound.Play();
            _endScreen.Display(true);
        }
        
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnDestroy()
        {
            _player.OnWin -= LaunchWin;
            _endScreen.OnFadeComplete -= ResetGame;
            foreach (var ghost in _ghostsList)
            {
                ghost.OnPlayerReached -= LaunchGameOver;
            }
        }
    }
}