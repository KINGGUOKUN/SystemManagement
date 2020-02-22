using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Dto;
using SystemManagement.Repository.Contract;
using SystemManagement.Service.Contract;

namespace SystemManagement.Service
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AccountService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Tuple<bool, SysUserDto>> ValidateCredentials(string account, string password)
        {
            var user = await _userRepository.FetchAsync(x => x.Account == account);
            if (user != null)
            {
                if (user.Password == password)
                {
                    return Tuple.Create(true, _mapper.Map<SysUserDto>(user));
                }
            }

            return Tuple.Create<bool, SysUserDto>(false, null);
        }
    }
}
