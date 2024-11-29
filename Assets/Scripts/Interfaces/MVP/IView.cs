using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Interface for the view in the MVC pattern.
/// </summary>
public interface IView
{
    Transform Transform { get; }
    GameObject GameObject { get; }

}
