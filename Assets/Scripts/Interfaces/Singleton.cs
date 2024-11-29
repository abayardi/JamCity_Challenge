using UnityEngine;

/// <summary>
/// Abstract base class to create a Singleton in Unity.
/// Ensures that only one instance of the derived class exists in the scene.
/// </summary>
/// <typeparam name="T">The type of the derived class inheriting from Singleton.</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    /// <summary>
    /// The static reference to the single instance of the class.
    /// </summary>
    public static T Instance { get; private set; }

    /// <summary>
    /// Flag to indicate if the instance is being destroyed.
    /// Useful to avoid accessing the instance during application shutdown.
    /// </summary>
    public static bool IsShuttingDown { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// Ensures that the Singleton instance is initialized properly.
    /// </summary>
    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"[Singleton] Attempted to create a second instance of {typeof(T).Name}. Destroying new instance.");
            Destroy(gameObject);
            return;
        }

        Instance = (T)this;
        IsShuttingDown = false;
    }

    /// <summary>
    /// Called when the MonoBehaviour is destroyed.
    /// Resets the instance reference when the singleton is destroyed.
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            IsShuttingDown = true;
        }
    }

    /// <summary>
    /// Ensures the singleton instance persists across scenes if desired.
    /// Override in derived classes if persistence is not needed.
    /// </summary>
    protected virtual void SetPersistent()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Verifies if the Singleton instance exists.
    /// Useful to avoid null reference exceptions.
    /// </summary>
    public static bool Exists()
    {
        return Instance != null && !IsShuttingDown;
    }
}
