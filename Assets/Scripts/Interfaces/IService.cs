using UnityEngine;

/// <summary>
/// Marker interface for services to be used with the Service Locator.
/// Any class that wants to be registered must implement this interface.
/// </summary>
public interface IService
{
    /// <summary>
    /// M�todo para inicializar el servicio.
    /// </summary>
    void Initialize();

    /// <summary>
    /// M�todo para apagar o liberar los recursos utilizados por el servicio.
    /// </summary>
    void Shutdown();

    /// <summary>
    /// Verifica si el servicio est� correctamente inicializado.
    /// </summary>
    /// <returns>True si el servicio est� inicializado; de lo contrario, false.</returns>
    bool IsInitialized { get; }
}