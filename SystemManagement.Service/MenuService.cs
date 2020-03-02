using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using SystemManagement.Service.Contract;
using WeihanLi.EntityFramework;

namespace SystemManagement.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private readonly SysUserDto _currentUser;
        private readonly IMenuRepository _menuRepository;
        private readonly IRelationRepository _relationRepository;

        public MenuService(IMapper mapper, 
            SysUserDto currentUser, 
            IMenuRepository menuRepository, 
            IRelationRepository relationRepository)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _menuRepository = menuRepository;
            _relationRepository = relationRepository;
        }

        public async Task DeleteMenu(long menuId)
        {
            var menu = await _menuRepository.FetchAsync(x => x.ID == menuId);
            await _menuRepository.DeleteAsync(x => x.PCodes.Contains($"[{menu.Code}]"));
            await _menuRepository.DeleteAsync(menu);
        }

        public async Task<List<MenuNode>> GetMenus()
        {
            var menus = await _menuRepository.GetAsync(q => q.WithPredict(x => true)
            .WithOrderBy(o => o.OrderBy(x => x.Levels).ThenBy(x => x.Num)));
            var menuNodes = _mapper.Map<List<MenuNode>>(menus);
            foreach (var node in menuNodes)
            {
                var parentNode = menuNodes.FirstOrDefault(x => x.Code == node.PCode);
                if (parentNode != null)
                {
                    node.ParentId = parentNode.ID;
                }
            }

            var dictNodes = menuNodes.ToDictionary(x => x.ID);
            foreach (var pair in dictNodes)
            {
                var currentNode = pair.Value;
                if (currentNode.ParentId.HasValue && dictNodes.ContainsKey(currentNode.ParentId.Value))
                {
                    dictNodes[currentNode.ParentId.Value].Children.Add(currentNode);
                }
                else
                {
                    menuNodes.Add(currentNode);
                }
            }

            return menuNodes;
        }

        public async Task<List<RouterMenu>> GetMenusForRouter()
        {
            List<RouterMenu> result = new List<RouterMenu>();

            var roleIds = _currentUser.RoleId.Split(',').Select(x => long.Parse(x));
            var menus = await _menuRepository.GetMenusByRoleIds(roleIds.ToArray(), false);
            if (menus.Any())
            {
                List<RouterMenu> routerMenus = new List<RouterMenu>();
                foreach (var menu in menus)
                {
                    if (string.IsNullOrWhiteSpace(menu.Component))
                    {
                        continue;
                    }

                    RouterMenu routerMenu = _mapper.Map<RouterMenu>(menu);
                    routerMenu.Path = menu.Url;
                    routerMenu.Meta = new MenuMeta
                    {
                        Icon = menu.Icon,
                        Title = menu.Code
                    };
                    routerMenus.Add(routerMenu);
                }

                foreach (var node in routerMenus)
                {
                    var parentNode = routerMenus.FirstOrDefault(x => x.Code == node.PCode);
                    if (parentNode != null)
                    {
                        node.ParentId = parentNode.ID;
                    }
                }

                var dictNodes = routerMenus.ToDictionary(x => x.ID);
                foreach (var pair in dictNodes.OrderBy(x => x.Value.Num))
                {
                    var currentNode = pair.Value;
                    if (currentNode.ParentId.HasValue && dictNodes.ContainsKey(currentNode.ParentId.Value))
                    {
                        dictNodes[currentNode.ParentId.Value].Children.Add(currentNode);
                    }
                    else
                    {
                        result.Add(currentNode);
                    }
                }
            }

            return result;
        }

        public async Task<dynamic> GetMenuTreeListByRoleId(long roleId)
        {
            var menuIds = await this.GetMenuIdsByRoleId(roleId);
            List<ZTreeNode<long, dynamic>> roleTreeList = null;
            List<SysMenu> menus = await _menuRepository.GetAsync(q => q.WithOrderBy(o => o.OrderBy(x => x.ID)));
            foreach (var menu in menus)
            {
                var parentMenu = menus.FirstOrDefault(x => x.Code == menu.PCode);
                ZTreeNode<long, dynamic> node = new ZTreeNode<long, dynamic>
                {
                    ID = menu.ID,
                    PID = parentMenu != null ? parentMenu.ID : 0,
                    Name = menu.Name,
                    Open = parentMenu != null,
                    Checked = menuIds.Contains(menu.ID)
                };
                roleTreeList.Add(node);
            }

            List<Node<long>> nodes = _mapper.Map<List<Node<long>>>(roleTreeList);
            foreach (var node in nodes)
            {
                foreach (var child in nodes)
                {
                    if (child.PID == node.ID)
                    {
                        node.Children.Add(child);
                    }
                }
            }

            var groups = roleTreeList.GroupBy(x => x.PID).Where(x => x.Key > 1);
            foreach(var group in groups)
            {
                roleTreeList.RemoveAll(x => x.ID == group.Key);
            }

            return new
            {
                treeData = nodes.Select(x => x.PID == 0),
                checkedIds = roleTreeList.Where(x => x.Checked && x.PID != 0).Select(x => x.ID)
            };
        }

        public async Task SaveMenu(SysMenuDto menuDto)
        {
            if (menuDto.ID < 1)
            {
                if (await _menuRepository.ExistAsync(x => x.Code == menuDto.Code))
                {
                    throw new BusinessException((int)ErrorCode.Forbidden, "同名菜单已存在");
                }
                menuDto.Status = true;
            }

            if (string.IsNullOrWhiteSpace(menuDto.PCode) || string.Equals(menuDto.PCode, "0"))
            {
                menuDto.PCode = "0";
                menuDto.PCodes = "[0],";
                menuDto.Levels = 1;
            }
            else
            {
                var parentMenu = await _menuRepository.FetchAsync(x => x.Code == menuDto.PCode);
                menuDto.PCode = parentMenu.Code;
                if (string.Equals(menuDto.Code, menuDto.PCode))
                {
                    throw new BusinessException((int)ErrorCode.Forbidden, "菜单编码冲突");
                }

                menuDto.Levels = parentMenu.Levels + 1;
                menuDto.PCodes = $"{parentMenu.PCodes}[{parentMenu.Code}]";
            }

            var menu = _mapper.Map<SysMenu>(menuDto);
            if (menu.ID == 0)
            {
                await _menuRepository.InsertAsync(menu);
            }
            else
            {
                await _menuRepository.UpdateAsync(menu);
            }
        }

        private async Task<List<long>> GetMenuIdsByRoleId(long roleId)
        {
            List<long> result = null;
            var relations = await _relationRepository.SelectAsync(x => x.RoleId == roleId);
            if (relations.Any())
            {
                result = relations.Select(x => x.MenuId).ToList();
            }

            return result;
        }
    }
}
