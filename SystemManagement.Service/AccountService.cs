using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
