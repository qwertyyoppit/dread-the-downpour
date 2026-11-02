using System;

public class GameTask
{
    public bool Completed { get; private set; }
    public string Description { get; private set; }

    public Action OnComplete;

    public GameTask(string description = "")
    {
        Description = description;
    }

    public void Complete()
    {
        Completed = true;
        OnComplete?.Invoke();
    }
}
