using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T data);
    }
}
