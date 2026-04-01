using System;
using System.Collections.Generic;

public class Quest
{
    public static List<Quest> quests = new List<Quest>();

    private string display;
    private Func<bool> completionCondition;

    public Quest(string display, Func<bool> completionCondition)
    {
        this.display = display;
        this.completionCondition = completionCondition;

        quests.Add(this);
    }

    public string Display { get { return display; } }
    public Func<bool> CompletionCondition { get { return completionCondition; } }
}