using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace blog__dotnet_clean_architecture.Domain.Entities;

public abstract class Entity : INotifyPropertyChanging, INotifyPropertyChanged
{
    private DateTimeOffset _createdAt = DateTimeOffset.UtcNow;
    private DateTimeOffset? _updatedAt;

    public event PropertyChangingEventHandler? PropertyChanging;
    public event PropertyChangedEventHandler? PropertyChanged;

    public DateTimeOffset CreatedAt
    {
        get => _createdAt;
        set => SetWithNotify(value, ref _createdAt);
    }

    public Id Id
    {
        get;
        set;
    }

    public DateTimeOffset? UpdatedAt
    {
        get => _updatedAt;
        set => SetWithNotify(value, ref _updatedAt);
    }

    protected void SetWithNotify<T>(T value, ref T field, [CallerMemberName] string propertyName = "")
    {
        if (Equals(field, value))
        {
            return;
        }

        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
