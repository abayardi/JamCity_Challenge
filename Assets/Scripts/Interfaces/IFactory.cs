public interface IFactory<T, in TParam>
{
    /// <summary>
    /// Creates an instance of type <typeparamref name="T"/> using the provided parameter.
    /// This method is responsible for creating objects of type <typeparamref name="T"/> 
    /// using the specified parameter of type <typeparamref name="TParam"/>.
    /// </summary>
    /// <param name="param">The parameter used to configure or initialize the object of type <typeparamref name="T"/>.</param>
    /// <returns>An instance of type <typeparamref name="T"/>.</returns>
    T Create(TParam param);
}