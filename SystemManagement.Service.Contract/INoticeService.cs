using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Dto;

namespace SystemManagement.Service.Contract
{
    public interface INoticeService : IService
    {
        Task<List<SysNoticeDto>> GetList(string title);
    }
}
