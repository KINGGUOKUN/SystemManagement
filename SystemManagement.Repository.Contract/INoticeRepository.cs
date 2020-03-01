using SystemManagement.Entity;
using WeihanLi.EntityFramework;

namespace SystemManagement.Repository.Contract
{
    public interface INoticeRepository : IEFRepository<SystemManageDbContext, SysNotice>
    {
    }
}
