using UnityEngine;

namespace TextAdventure
{
    [CreateAssetMenu(fileName = "NewAnswer", menuName = "CreateAnswer", order = 0)]
    public class Answer : ScriptableObject
    {
        [TextArea]
        [SerializeField] private string text;
        [SerializeField] private Question question;
        [SerializeField] private string tag;
        public string Text => text;
        public Question Question => question;
        public string Tag => tag;
    }
}