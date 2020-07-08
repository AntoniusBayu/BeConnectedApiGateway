using DataAccess;
using System;

namespace Business
{
    public class ActivityLogBusiness : IActivityLog
    {
        private IUnitofWork _uow { get; set; }
        public ActivityLogBusiness(IUnitofWork uow)
        {
            _uow = uow;
        }
        public void saveLog(ActivityLog data)
        {
            try
            {
                _uow.OpenConnection(_uow.GetAppSettings("MongoDBConnString"), _uow.GetAppSettings("DbName"));

                var repo = new ActivityLogRepository(_uow);

                _uow.BeginTransaction();

                repo.AddAsync(data).Wait();

                _uow.CommitTransaction();
            }
            catch (Exception ex)
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
    }
}
