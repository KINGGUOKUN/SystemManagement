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
            CreateMap<SysCfgDto, SysCfg>();

            CreateMap<SysDept, SysDeptDto>();
            CreateMap<SysDeptDto, SysDept>();
            CreateMap<SysDept, DeptNode>();

            CreateMap<SysDict, SysDictDto>();
            CreateMap<SysDictDto, SysDict>();

            CreateMap<SysFileInfo, SysFileInfoDto>();
            CreateMap<SysFileInfoDto, SysFileInfo>();

            CreateMap<SysLoginLog, SysLoginLogDto>();
            CreateMap<SysLoginLogDto, SysLoginLog>();

            CreateMap<SysMenu, SysMenuDto>();
            CreateMap<SysMenuDto, SysMenu>();
            CreateMap<SysMenu, RouterMenu>();

            CreateMap<SysNotice, SysNoticeDto>();
            CreateMap<SysNoticeDto, SysNoticeDto>();

            CreateMap<SysOperationLog, SysOperationLogDto>();
            CreateMap<SysOperationLogDto, SysOperationLog>();

            CreateMap<SysRelation, SysRelationDto>();
            CreateMap<SysRelationDto, SysRelation>();

            CreateMap<SysRole, SysRoleDto>();
            CreateMap<SysRoleDto, SysRole>();

            CreateMap<SysTask, SysTaskDto>();
            CreateMap<SysTaskDto, SysTask>();

            CreateMap<SysTaskLog, SysTaskLogDto>();
            CreateMap<SysTaskLogDto, SysTaskLog>();

            CreateMap<SysUser, SysUserDto>();
            CreateMap<SysUserDto, SysUser>();
            CreateMap<SysUser, UserProfile>();

            CreateMap<SysNotice, SysNoticeDto>();
            CreateMap<SysNoticeDto, SysNotice>();
        }
    }
}
