using System;
using UnityEngine;

namespace TextAdventure
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField] private Question startQuestion;

        public Action<Question> OnQuestionChangedChanged;
        public Action<string> OnTagApeared;

        public Question CurrentQuestion => _currentQuestion;

        private TagParser tagParser;
        private Question _currentQuestion;

        
        private void Awake()
        {
            _currentQuestion = startQuestion;
            tagParser = GetComponent<TagParser>();
        }

        public Question PeackAnswer(int index)
        {
            if(index >= _currentQuestion.Answers.Length)
                return null;

            Answer ans = _currentQuestion.Answers[index];
            if (ans.Tag.Length > 0)
            {
                ExecutePramsAnswer pramsAnswer = new ExecutePramsAnswer();
                pramsAnswer.state = StateTag.OnPicked;
                pramsAnswer.answer = ans;
                pramsAnswer.ui = null;
                tagParser.ParseTag(ans.Tag,pramsAnswer);   
            }

            _currentQuestion = ans.Question;
            
            if(_currentQuestion != null)
                OnQuestionChangedChanged?.Invoke(_currentQuestion);
            
            return _currentQuestion;
        }
    }
}