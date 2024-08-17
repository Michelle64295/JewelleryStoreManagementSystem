#nullable enable
namespace JewelleryStoreManagementSystem.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void SaveChanges();
        void Remove(T entity);
    }
}
