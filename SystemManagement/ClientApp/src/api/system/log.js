import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/log/operationLog/list',
    method: 'get',
    params
  })
}

export function queryByUser() {
  return request({
    url:'/log/queryByUser',
    method: 'get'
  })
}

export function clear() {
  return request({
    url: '/log/operationLog',
    method: 'delete'
  })
}
