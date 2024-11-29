
using System;
using System.Diagnostics;

/// <summary>
/// Interface for the model in the MVC pattern.
/// </summary>
public interface IModel
{
    public event Action OnModelChanged;

    /// <summary>
    /// Notifies listeners about a change in the model's state.
    /// </summary>
    void NotifyChange(); 
}
