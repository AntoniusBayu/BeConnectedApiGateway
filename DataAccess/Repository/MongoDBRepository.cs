using MongoDB.Driver;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class MongoDBRepository<T> : IRepository<T> where T : class
    {
        protected MongoDBUnitofWork _uow;
        protected IMongoCollection<T> _collection;

        public MongoDBRepository(IUnitofWork uow)
        {
            _uow = (MongoDBUnitofWork)uow;
            _collection = _uow._MongoDBConn._database.GetCollection<T>(typeof(T).Name.ToLower());
        }

        public async Task AddAsync(T data)
        {
            await _collection.InsertOneAsync(data);
        }
    }
}
