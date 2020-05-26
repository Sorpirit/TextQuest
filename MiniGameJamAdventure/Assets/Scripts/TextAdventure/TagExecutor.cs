using System;
using System.Collections;
using System.Collections.Generic;
using TextAdventure;
using UnityEngine;

public class TagExecutor : MonoBehaviour
{
    private List<string> triggers;

    private void Awake()
    {
        triggers = new List<string>();
    }

    public void Hide(AnswerUI ui)
    {
        Debug.Log("Hey");
        ui.gameObject.SetActive(false);
    }

    public void AddTrigger(string trigger)
    {
        triggers.Add(trigger);
    }

    public void RemTrigger(string trigger)
    {
        bool successes = triggers.Remove(trigger);
        if(!successes)
            Debug.LogWarning("Cant remove trigger : \"" + trigger + "\".");
    }

    public bool Contains(string trigger)
    {
        return triggers.Contains(trigger);
    }
    
    public void ActrionTriger(string actionName)
    {
        
    }
}
