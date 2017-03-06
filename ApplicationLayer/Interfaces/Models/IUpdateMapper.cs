namespace ApplicationLayer.Interfaces.Models
{
    public interface IUpdateMapper<T>
    {
        T MapUpdate(T from, T to);
    }
}