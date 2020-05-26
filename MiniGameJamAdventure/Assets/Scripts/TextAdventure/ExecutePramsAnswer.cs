namespace TextAdventure
{
    public struct ExecutePramsAnswer
    {
        public StateTag state;
        public Answer answer;
        public AnswerUI ui;
    }

    public enum StateTag
    {
        OnShow,
        OnPicked
    }
}