namespace Dashboard.Domain
{
    public interface IUniqueKeyGenerator
    {
        int GetId<T>();
    }
}