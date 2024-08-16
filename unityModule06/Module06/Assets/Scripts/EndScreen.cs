using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Module06.Screen
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _winSprite;
        [SerializeField] private Sprite _loseSprite;
        
        private float _fadeDuration = 1.5f;
        private float _displayDuration = 3f;
        
        public Action OnFadeComplete;

        private void Awake()
        {
            _background.canvasRenderer.SetAlpha(0);
            _image.canvasRenderer.SetAlpha(0);
        }
        
        public void Display(bool win)
        {
            StartCoroutine(FadeCoroutine(win));
        }
        
        private IEnumerator FadeCoroutine(bool win)
        {
            _image.sprite = win ? _winSprite : _loseSprite;
            StartCoroutine(FadeBackground(true));
            
            yield return new WaitForSeconds(_displayDuration);
        
            if (!win)
                StartCoroutine(FadeBackground(false));
            else
                GameManager.Instance.QuitGame();
            
            OnFadeComplete?.Invoke();
        }
        
        private IEnumerator FadeBackground(bool fadeIn)
        {
            _background.CrossFadeAlpha(fadeIn ? 1 : 0, _fadeDuration, true);
            _image.CrossFadeAlpha(fadeIn ? 1 : 0, _fadeDuration, true);
            yield return new WaitForSeconds(_fadeDuration);
        
            if (!fadeIn)
            {
                _background.canvasRenderer.SetAlpha(0);
                _image.canvasRenderer.SetAlpha(0);
            }
        }
    }
}