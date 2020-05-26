using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TextAdventure
{
    public class QuestUIController : MonoBehaviour
    {
        [SerializeField] private GameObject answerPrefab;
        [SerializeField] private QuestController questController;

        [SerializeField] private TMP_Text questionLable;
        [SerializeField] private float textAppearSpeed;
        [SerializeField] private AudioSource sors;
        [SerializeField] private AudioSource myVoice;

        [SerializeField] private TagParser tagParser;
        
        private Question _question;
        private bool _isAnimatingQuestion;
        private bool _isReadyToShow;
        private GameObject slected;

        private void Start()
        {
            _question = questController.CurrentQuestion;
            if (_question != null)
                StartCoroutine(AnimateQuestionText());
        }

        private void Update()
        {
            if (_question == null)
                return;

            bool skipPress = Input.GetMouseButtonDown(0) ||
                             Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.Escape);
            if (_isAnimatingQuestion && skipPress)
            {
                StopAnimationText();
            }
        }

        private void StopAnimationText()
        {
            if (!_isAnimatingQuestion)
                return;
            _isAnimatingQuestion = false;
            StopCoroutine(AnimateQuestionText());
            GnerateAnsers();
        }

        public void OnAnswePeacked(int index)
        {
            myVoice.Play();
            _question = questController.PeackAnswer(index);
            if (_question == null)
            {
                ClearPrevAnswers();
                questionLable.text = "";
            }
            else
            {
                DesablaAllBesidesIndex(index);
                StartCoroutine(AnimateQuestionText());
            }
        }

        private IEnumerator AnimateQuestionText()
        {
            float timer = 0;
            int charakterIndex = -1;
            string textToWrite = _question.Text;
            _isAnimatingQuestion = true;
            _isReadyToShow = false;

            bool isTag = false;
            bool muteSound = false;
            
            while (_isAnimatingQuestion)
            {
                yield return null;
                timer -= Time.deltaTime;
                
                while (timer <= 0f)
                {
                    charakterIndex++;  
                    if (charakterIndex >= textToWrite.Length)
                    {
                        _isAnimatingQuestion = false;
                        break;
                    }
                    
                    if (textToWrite[charakterIndex] == '<')
                    {
                        isTag = true;
                    }
                    if (isTag && textToWrite[charakterIndex] == '>')
                    {
                        isTag = false;
                        continue;
                    }
                    
                    if (isTag)
                    {
                        continue;
                    }
                    string text = textToWrite.Substring(0, charakterIndex);
                    text += "<color=#00000000>" + textToWrite.Substring(charakterIndex) + "</color>";

                    questionLable.text = text;
                    if (!muteSound)
                    {
                        sors.pitch = Random.Range(1.5f, 3f);
                        sors.Play();
                    }
                    

                    timer += textAppearSpeed;
                                     
                }

                if (!_isAnimatingQuestion && charakterIndex < textToWrite.Length)
                    questionLable.text = textToWrite;
            }

            ClearPrevAnswers();
            GnerateAnsers();
            _isReadyToShow = true;
        }

        private void GnerateAnsers()
        {
            ClearPrevAnswers();
            int index = 0;
            foreach (var answer in _question.Answers)
            {
                if(answer == null)
                    continue;
                
                GameObject uiObj = Instantiate(answerPrefab, transform);
                AnswerUI answerUi = uiObj.GetComponent<AnswerUI>();
                answerUi.Init(index, answer.Text);
                answerUi.apearDelay = index * .3f;
                answerUi.OnPeacked += OnAnswePeacked;
                index++;
                
                if (answer.Tag.Length > 0)
                {
                    ExecutePramsAnswer pramsAnswer = new ExecutePramsAnswer();
                    pramsAnswer.state = StateTag.OnShow;
                    pramsAnswer.answer = answer;
                    pramsAnswer.ui = answerUi;
                    tagParser.ParseTag(answer.Tag,pramsAnswer);   
                }
            }
        }

        private void ClearPrevAnswers()
        {
            if(slected != null)
                Destroy(slected);
            
            if (transform.childCount == 0)
                return;

            foreach (Transform answer in transform)
            {
                Destroy(answer.gameObject);
            }
        }

        private void DesablaAllBesidesIndex(int index)
        {
            slected = transform.GetChild(index).gameObject;
            transform.GetChild(index).SetParent(transform.parent);
            
            for (int ch = 0; ch < transform.childCount; ch++)
            {
                LayoutElement el = transform.GetChild(ch).gameObject.GetComponent<LayoutElement>();
                el.ignoreLayout = true;
                RollFadeIn rollFadeIn = transform.GetChild(ch).gameObject.GetComponent<RollFadeIn>();
                rollFadeIn.FadeOutDestroy();
            }
        }
    }
}