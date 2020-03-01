import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/user/list',
    method: 'get',
    params
  })
}

export function saveUser(params) {
  return request({
    url: '/user',
    method: 'post',
    data: params
  })
}

export function remove(userId) {
  return request({
    url: `/user/${userId}`,
    method: 'delete'
  })
}

export function setRole(params) {
  return request({
    url: '/user/setRole',
    method: 'put',
    data: params
  })
}

export function changeStatus(userId) {
  return request({
    url: `/user/changeStatus/${userId}`,
    method: 'put'
  })
}
