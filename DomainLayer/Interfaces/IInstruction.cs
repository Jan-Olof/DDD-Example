namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The instruction properties interface.
    /// </summary>
    public interface IInstruction : IIdentifier, IInstructionModel
    {
        /// <summary>
        /// Gets or sets the description. A text field that describes the instruction.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the name. This is the name of the instruction.
        /// </summary>
        string Name { get; set; }
    }
}