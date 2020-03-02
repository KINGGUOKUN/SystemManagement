import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/role/list',
    method: 'get',
    params
  })
}


export function save(data) {
  return request({
    url: '/role',
    method: 'post',
    data
  })
}

export function remove(roleId) {
  return request({
    url: '/role',
    method: 'delete',
    params: {
      roleId: roleId
    }
  })
}

export  function roleTreeListByUserId(userId){
  return request({
    url: `/role/roleTreeListByUserId/${userId}`,
    method: 'get'
  })
}


export function savePermissons(params) {
  return request({
    url: '/role/savePermisson',
    method: 'post',
    params
  })
}
