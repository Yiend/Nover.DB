const result = [
  {
    url: '/login',
    type: 'post',
    response: () => {
      return {
        'code': 200,
        'msg': '登录成功',
        'body': '12345678'
      }
    }
  }
]

export default result
