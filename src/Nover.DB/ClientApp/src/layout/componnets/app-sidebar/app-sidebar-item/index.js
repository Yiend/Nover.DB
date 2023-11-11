import './index.scss'
import { Menu } from 'element-react'
import CuSvgIcon from '@/components/cu-svg-icon'
import { Link } from 'react-router-dom'

const AppSidebarItem = (props) => {
  const { item } = props

  if (!item || !item.meta || item.meta.hidden) {
    return
  }
  if (item.children && item.children.length > 0) {
    return (
      <Menu.SubMenu
        index={item.path}
        title={
          <div className="app-sidebar-item__title">
            <CuSvgIcon
              className="app-sidebar-item__icon"
              iconClass={item.meta.icon}
            />
            <span>{item.meta.title}</span>
          </div>
        }
      >

        {
          item.children.map((item, index) => {
            return (
              <AppSidebarItem
                item={item}
                key={index}
              />
            )
          })
        }
      </Menu.SubMenu>
    )
  }
  return (
    <>
      <Link to={item.path}>
        <Menu.Item className="app-sidebar-item__item"
          index={item.path}
        >{item.meta.title}</Menu.Item>
      </Link>
    </>
  )
}

export default AppSidebarItem
