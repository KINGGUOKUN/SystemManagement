using AutoMapper;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using SystemManagement.Service.Contract;
using WeihanLi.Extensions;

namespace SystemManagement.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<PagedModel<SysUserDto>> SearchUsers(UserSearchModel searchModel)
        {
            Expression<Func<SysUser, bool>> whereCondition = x => x.Status;
            if (!string.IsNullOrWhiteSpace(searchModel.Account))
            {
                whereCondition = whereCondition.And(x => x.Account.Contains(searchModel.Account));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                whereCondition = whereCondition.And(x => x.Name.Contains(searchModel.Name));
            }

            var pagedModel = await _userRepository.PagedAsync(searchModel.PageIndex, searchModel.PageSize, whereCondition, x => x.ID);

            return _mapper.Map<PagedModel<SysUserDto>>(pagedModel);
        }
    }
}
