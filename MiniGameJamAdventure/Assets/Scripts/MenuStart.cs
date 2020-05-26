using System;
using System.Collections;
using DefaultNamespace.UITweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MenuStart : MonoBehaviour
    {
        private ScenTransitions _transitions;
        
        private void Start()
        {
            _transitions = FindObjectOfType<ScenTransitions>();
            _transitions.FadeOut();
        }

        public void SartScene(int index)
        {
            StartCoroutine(LoadLevL(index));
        }

        private IEnumerator LoadLevL(int index)
        {
            _transitions.FadeIn();

            yield return new WaitForSeconds(1f);
            
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(index);

            yield return loadScene;
        }
    }
}