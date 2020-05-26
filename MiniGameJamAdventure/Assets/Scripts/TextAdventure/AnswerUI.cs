using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TextAdventure
{
    public class AnswerUI : MonoBehaviour
    {
        [SerializeField] private GameObject OnHoverIcon;
        [SerializeField] private TMP_Text answerText;
        
        public int index;
        public Action<int> OnPeacked;
        public float apearDelay;
        
        private bool isActivated;
        private Button bt;

        
        public void Init(int index, string text)
        {
            this.index = index;
            answerText.text = text;

        }

        private void Awake()
        {
            bt = GetComponent<Button>();
        }

        public void PeackAnswer()
        {
            OnPeacked?.Invoke(index);
            bt.interactable = false;
            OnHoverIcon.SetActive(true);
            isActivated = true;
        }

        public void OnHover()
        {
            if(isActivated)
                return;
            OnHoverIcon.SetActive(true);
        }
        
        public void OnNotHover()
        {
            if(isActivated)
                return;
            OnHoverIcon.SetActive(false);
        }
    }
}