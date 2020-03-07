import request from '@/utils/request'

export function getList() {
  return request({
    url: '/menu/list',
    method: 'get'
  })
}

export function listForRouter(params) {
  return request({
    url: '/menu/listForRouter',
    method: 'get',
    params
  })
}

export function save(data) {
  return request({
    url: '/menu',
    method: 'post',
    data
  })
}

export function delMenu(id) {
  return request({
    url: `/menu/${id}`,
    method: 'delete'
  })
}
export function menuTreeListByRoleId(roleId) {
  return request({
    url: `/menu/menuTreeListByRoleId/${roleId}`,
    method: 'get'
  })
}
