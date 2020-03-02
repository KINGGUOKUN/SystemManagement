import request from '@/utils/request'

export function tree() {
  return request({
    url: '/dept/tree',
    method: 'get',
  })
}

export function list() {
  return request({
    url: '/dept/list',
    method: 'get',
  })
}

export function save(data) {
  return request({
    url: '/dept',
    method: 'post',
    data
  })
}

export function del(id) {
  return request({
    url: `/dept/${id}`,
    method: 'delete'
  })
}
