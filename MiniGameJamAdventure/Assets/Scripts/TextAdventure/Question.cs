using UnityEngine;

namespace TextAdventure
{
    [CreateAssetMenu(fileName = "NewNode", menuName = "CreateQuestion", order = 0)]
    public class Question : ScriptableObject
    {
        [TextArea]
        [SerializeField] private string text;
        [SerializeField] private Answer[] answers;
        [SerializeField] private string tag;

        public string Text => text;
        public Answer[] Answers => answers;
        public string Tag => tag;
    }
}