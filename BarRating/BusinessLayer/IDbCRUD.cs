namespace BusinessLayer
{
    public interface IDbCRUD<T, K> where T : class
    {
        Task CreateAsync(T item);
        Task<T> ReadAsync(K key, bool useNavigationalProperties = false, bool isReadOnly = true);
        Task<List<T>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true);
        Task UpdateAsync(T item);
        Task DeleteAsync(K key);
    }
}
