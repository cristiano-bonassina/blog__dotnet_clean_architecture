namespace blog__dotnet_clean_architecture.Domain.Entities;

public class Todo : Entity, IAggregateRoot
{
    private Description _description;
    private bool _isCompleted;

    public Description Description
    {
        get => _description;
        set => SetWithNotify(value, ref _description);
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => SetWithNotify(value, ref _isCompleted);
    }
}