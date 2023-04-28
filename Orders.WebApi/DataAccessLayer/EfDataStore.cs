using Orders.WebApi.Abstractions;

namespace Orders.WebApi.DataAccessLayer
{
    public class EfDataStore : IDataStore
    {
        internal readonly ApplicationDbContext _dbContext;

        public EfDataStore(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        void IDataStore.Create<T>(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        void IDataStore.Delete<T>(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        T IDataStore.Get<T>(Guid id)
        {
            return _dbContext.Set<T>().First(x => x.Id.Equals(id));
        }

        IQueryable<T> IDataStore.GetAll<T>()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        void IDataStore.Update<T>(T entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}