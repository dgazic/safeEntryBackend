namespace SafeEntry.Persistance.Interfaces
{
    public interface IAsyncPersistance<T>
    {
        Task<T> Insert(T model);
        Task<IEnumerable<T>> GetAll(int id);
    }
}
