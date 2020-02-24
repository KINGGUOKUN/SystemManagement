using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Entity;
using WeihanLi.Common.Models;

namespace SystemManagement.Service
{
    public class SystemManagementProfile : Profile
    {
        public SystemManagementProfile()
        {
            CreateMap(typeof(IPagedListModel<>), typeof(PagedModel<>));
            CreateMap<SysCfg, SysCfgDto>();
            CreateMap<SysDept, SysDeptDto>();
            CreateMap<SysDict, SysDictDto>();
            CreateMap<SysFileInfo, SysFileInfoDto>();
            CreateMap<SysLoginLog, SysLoginLogDto>();
            CreateMap<SysMenu, SysMenuDto>();
            CreateMap<SysNotice, SysNoticeDto>();
            CreateMap<SysOperationLog, SysOperationLogDto>();
            CreateMap<SysRelation, SysRelationDto>();
            CreateMap<SysRole, SysRoleDto>();
            CreateMap<SysTask, SysTaskDto>();
            CreateMap<SysTaskLog, SysTaskLogDto>();
            CreateMap<SysUser, SysUserDto>();
        }
    }
}
