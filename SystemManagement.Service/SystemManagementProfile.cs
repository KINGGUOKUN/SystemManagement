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
            CreateMap(typeof(ZTreeNode<,>), typeof(Node<>)).IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<SysCfg, SysCfgDto>().ReverseMap();
            CreateMap<SysDept, SysDeptDto>().ReverseMap();
            CreateMap<SysDept, DeptNode>();
            CreateMap<SysDict, SysDictDto>().ReverseMap();
            CreateMap<SysFileInfo, SysFileInfoDto>().ReverseMap();
            CreateMap<SysLoginLog, SysLoginLogDto>().ReverseMap();
            CreateMap<SysMenu, SysMenuDto>().ReverseMap();
            CreateMap<SysMenu, RouterMenu>();
            CreateMap<SysNotice, SysNoticeDto>().ReverseMap();
            CreateMap<SysOperationLog, SysOperationLogDto>().ReverseMap();
            CreateMap<SysRelation, SysRelationDto>().ReverseMap();
            CreateMap<SysRole, SysRoleDto>().ReverseMap();
            CreateMap<SysTask, SysTaskDto>().ReverseMap();
            CreateMap<SysTaskLog, SysTaskLogDto>().ReverseMap();
            CreateMap<SysUser, SysUserDto>().ReverseMap();
            CreateMap<SysUser, UserProfile>();
            CreateMap<SysNotice, SysNoticeDto>().ReverseMap();
        }
    }
}
