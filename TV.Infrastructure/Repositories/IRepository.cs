namespace TV.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T? Get(Guid id);
        List<T> GetAll();
        void SaveChanges();

    }
}
