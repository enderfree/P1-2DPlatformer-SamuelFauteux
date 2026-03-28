using System;

public class Quest
{
    private string display;
    private Func<bool> completionCondition;

    public Quest(string display, Func<bool> completionCondition)
    {
        this.display = display;
        this.completionCondition = completionCondition;
    }

    public string Display { get { return display; } }
    public Func<bool> DisplaycompletionCondition { get { return completionCondition; } }
}