namespace DataAccess
{
    public class ActivityLogRepository : MongoDBRepository<ActivityLog>
    {
        public ActivityLogRepository(IUnitofWork uow) : base(uow)
        { }
    }
}
