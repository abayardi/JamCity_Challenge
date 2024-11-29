using UnityEngine;

/// <summary>
/// Marker interface for services to be used with the Service Locator.
/// Any class that wants to be registered must implement this interface.
/// </summary>
public interface IService
{
    /// <summary>
    /// Método para inicializar el servicio.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Método para apagar o liberar los recursos utilizados por el servicio.
    /// </summary>
    void Shutdown();

    /// <summary>
    /// Verifica si el servicio está correctamente inicializado.
    /// </summary>
    /// <returns>True si el servicio está inicializado; de lo contrario, false.</returns>
    bool IsInitialized { get; }
}