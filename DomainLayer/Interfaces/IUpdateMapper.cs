namespace DomainLayer.Interfaces
{
    /// <summary>
    /// Handles what fields to update in a model.
    /// </summary>
    /// <typeparam name="T">The type of model to update</typeparam>
    public interface IUpdateMapper<T>
    {
        /// <summary>
        /// Updates the fields that are supposed to be updated when editing an instruction.
        /// </summary>
        T MapUpdate(T from, T to);
    }
}