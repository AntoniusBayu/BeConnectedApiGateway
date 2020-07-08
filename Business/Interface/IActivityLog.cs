using DataAccess;

namespace Business
{
    public interface IActivityLog
    {
        void saveLog(ActivityLog data);
    }
}
