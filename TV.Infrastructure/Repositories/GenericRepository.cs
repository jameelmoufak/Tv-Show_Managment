namespace TV.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public readonly TVDbContext context;

        public GenericRepository(TVDbContext context)
        {
            this.context = context;
        }
        public T Add(T entity)
        {
            var newEntity = context.Add(entity);
            return newEntity.Entity;
        }

        public T? Get(Guid id)
        {
            return context.Find<T>(id);
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public T Update(T entity)
        {
            return context.Update(entity).Entity;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}
