import React, { useEffect, useState } from 'react'
import './index.scss'
import classNames from 'classnames'
import AppSidebarItem from '@/layout/componnets/app-sidebar/app-sidebar-item'
import { Menu } from 'element-react'
import PermissionStore from '@/store/users/modules/permission'
import { useLocation } from 'react-router-dom'
const AppSidebar = ({ className }) => {
  const cns = classNames(
    'app-sidebar',
    className
  )

  const permissionRoutes = PermissionStore.getRoutes()
  const [routes, setRoutes] = useState(permissionRoutes)

  const [defaultActive, setDefaultActive] = useState('')
  useEffect(() => {
    setRoutes(permissionRoutes)
  }, [permissionRoutes])

  const location = useLocation()
  const { pathname } = location

  useEffect(() => {
    setDefaultActive(pathname)
  }, [pathname])

  return (
    <div className={cns}>
      <Menu
        className="app-sidebar__menu"
        defaultActive={defaultActive}
        theme="dark"
      >
        {routes.map((item, index) => {
          return (
            <AppSidebarItem
              item={item}
              key={index}
            />
          )
        })}
      </Menu>
    </div>
  )
}

export default AppSidebar
