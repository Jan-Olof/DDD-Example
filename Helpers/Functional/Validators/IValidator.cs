namespace Helpers.Functional.Validators
{
    public interface IValidator<T>
    {
        bool IsValid(T cmd);
    }
}