using System;
using UnityEngine;

namespace DefaultNamespace.UITweening
{
    public class ScenTransitions : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private float fadeDuration;

        private bool isFading;
        private float fadeId;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        

        public void FadeIn()
        {
            LeanTween.alphaCanvas(_group, 1, fadeDuration);
        }

        public void FadeOut()
        {
            LeanTween.alphaCanvas(_group, 0, fadeDuration);
        }
    }
}