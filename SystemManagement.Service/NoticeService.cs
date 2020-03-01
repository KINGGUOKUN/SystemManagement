using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Dto;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using SystemManagement.Service.Contract;

namespace SystemManagement.Service
{
    public class NoticeService : INoticeService
    {
        private readonly IMapper _mapper;
        private readonly INoticeRepository _noticeRepository;

        public NoticeService(IMapper mapper, INoticeRepository noticeRepository)
        {
            _mapper = mapper;
            _noticeRepository = noticeRepository;
        }

        public async Task<List<SysNoticeDto>> GetList(string title)
        {
            List<SysNotice> notices = null;
            if (string.IsNullOrWhiteSpace(title))
            {
                notices = await _noticeRepository.SelectAsync(x => true);
            }
            else
            {
                notices = await _noticeRepository.SelectAsync(x => x.Title.Contains(title));
            }

            return _mapper.Map<List<SysNoticeDto>>(notices);
        }
    }
}
