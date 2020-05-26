using TextAdventure;
using UnityEngine;

public class TagParser : MonoBehaviour
{
    private TagExecutor executor;

    private void Awake()
    {
        executor = GetComponent<TagExecutor>();
    }

    public void ParseTag(string tag, ExecutePramsAnswer pramsAnswer)
    {
        string[] stringComands = tag.Split(' ');
        ExcuteState state = ExcuteState.Idel;
        ExcuteState lastState = ExcuteState.Idel;
        bool canExecute = true;

        bool equasion = false;
        bool signState = false;
        bool sign = true;

        foreach (string comand in stringComands)
        {
            bool executingAction = state != ExcuteState.Idel && state != ExcuteState.ExcuteStart &&
                                   state != ExcuteState.ExecutePick;
            if (executingAction)
            {
                switch (state)
                {
                    case ExcuteState.AddingTag:
                        executor.AddTrigger(comand);
                        continue;
                    case ExcuteState.RemovingTag:
                        executor.RemTrigger(comand);
                        continue;
                    case ExcuteState.TriggeringAction:
                        executor.ActrionTriger(comand);
                        continue;
                    case ExcuteState.Hiding:
                        if (signState)
                        {
                            switch (comand)
                            {
                                case "&":
                                    sign = false;
                                    continue;
                                case "|":
                                    sign = true;
                                    continue;
                                default:
                                    if (!equasion)
                                        executor.Hide(pramsAnswer.ui);
                                    state = lastState;
                                    break;
                            }

                            signState = false;
                        }
                        else
                        {
                            bool inverse = comand[0] == '!';
                            bool contains = false;
                            if (inverse)
                            {
                                Debug.Log("Hey2!");
                                string trigger = comand.Substring(1);
                                contains = !executor.Contains(trigger);
                            }
                            else
                            {
                                Debug.Log("Hey3!");
                                contains = executor.Contains(comand);
                            }

                            equasion &= (sign || contains);
                            signState = true;
                            
                            if (comand.Equals(stringComands[stringComands.Length - 1]))
                            {
                                if (!equasion)
                                    executor.Hide(pramsAnswer.ui);
                            }
                        }

                        break;
                }
            }

            canExecute = state == ExcuteState.Idel ||
                         (state == ExcuteState.ExcuteStart && pramsAnswer.state == StateTag.OnShow) ||
                         (state == ExcuteState.ExecutePick && pramsAnswer.state == StateTag.OnPicked);

            switch (comand)
            {
                case "s":
                    state = ExcuteState.ExcuteStart;
                    continue;
                case "p":
                    state = ExcuteState.ExecutePick;
                    continue;
            }

            if (canExecute)
            {
                lastState = state;

                switch (comand)
                {
                    case "hide":
                        Debug.Log("Hey!");
                        state = ExcuteState.Hiding;
                        sign = false;
                        signState = false;
                        equasion = true;
                        break;
                    case "add":
                        state = ExcuteState.AddingTag;
                        break;
                    case "rem":
                        state = ExcuteState.RemovingTag;
                        break;
                    case "trigger":
                        state = ExcuteState.TriggeringAction;
                        break;
                }
            }
        }
    }

    private enum ExcuteState
    {
        Idel,
        ExcuteStart,
        ExecutePick,
        AddingTag,
        RemovingTag,
        Hiding,
        TriggeringAction
    }
}