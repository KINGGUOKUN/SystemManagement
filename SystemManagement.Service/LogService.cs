using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using SystemManagement.Service.Contract;
using WeihanLi.Extensions;

namespace SystemManagement.Service
{
    public class LogService : ILogService
    {
        private readonly IMapper _mapper;
        private readonly IOperationLogRepository _operationLogRepository;
        private readonly IUserRepository _userRepository;

        public LogService(IMapper mapper, 
            IOperationLogRepository operationLogRepository,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _operationLogRepository = operationLogRepository;
            _userRepository = userRepository;
        }

        public async Task AppendOperationLog(SysOperationLogDto logDto)
        {
            var log = _mapper.Map<SysOperationLog>(logDto);

            await _operationLogRepository.InsertAsync(log);
        }

        public async Task ClearOperationLogs()
        {
            await _operationLogRepository.ClearLogs();
        }

        public async Task<PagedModel<SysOperationLogDto>> SearchOperationLogs(LogSearchModel searchModel)
        {
            Expression<Func<SysOperationLog, bool>> whereCondition = x => true;
            if (searchModel.BeginTime.HasValue)
            {
                whereCondition = whereCondition.And(x => x.CreateTime >= searchModel.BeginTime.Value);
            }

            if (searchModel.EndTime.HasValue)
            {
                whereCondition = whereCondition.And(x => x.CreateTime <= searchModel.BeginTime.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.LogName))
            {
                whereCondition = whereCondition.And(x => x.LogName.Contains(searchModel.LogName));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.LogType))
            {
                whereCondition = whereCondition.And(x => x.LogType == searchModel.LogType);
            }

            var pagedModel = await _operationLogRepository.PagedAsync(searchModel.PageIndex, searchModel.PageSize, whereCondition, x => x.ID, true);

            var result = _mapper.Map<PagedModel<SysOperationLogDto>>(pagedModel);
            if (result.Count > 0)
            {
                var userIds = result.Data.Select(x => x.UserId);
                var users = await _userRepository.SelectAsync(x => userIds.Contains(x.ID));
                foreach (var log in result.Data)
                {
                    log.UserName = users.FirstOrDefault(x => x.ID == log.UserId)?.Name;
                }
            }

            return result;
        }
    }
}
