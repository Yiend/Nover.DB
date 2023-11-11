import PermissionStore from '@/store/users/modules/permission'

export const getMatchedRoutes = (pathname) => {
  const routes = PermissionStore.getRoutes()
  return recursionMatchedRoutes(routes, pathname)
}

export const recursionMatchedRoutes = (routes, pathname) => {
  let result = []
  const activeMenu = pathname
  const findRoute = routes.find((item) => {
    if (item.path === activeMenu) {
      return true
    }
    if (item.children) {
      const matchedChildren = recursionMatchedRoutes(item.children, pathname)
      if (matchedChildren && matchedChildren.length > 0) {
        result = [...result, ...matchedChildren]
        return true
      }
    }
    return false
  })
  if (findRoute) {
    result.unshift(findRoute)
  }
  return result
}
