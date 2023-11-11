import './index.scss'
import classNames from 'classnames'
import { Breadcrumb } from 'element-react'
import { useEffect, useState } from 'react'
import { getMatchedRoutes } from '@/utils/route-utils'
import { useLocation } from 'react-router-dom'

const AppBreadcrumb = ({ className }) => {
  const cns = classNames(
    'app-breadcrumb',
    className
  )
  const location = useLocation()
  const { pathname } = location
  const [breadcrumbs, setBreadcrumbs] = useState(getMatchedRoutes(pathname))
  useEffect(() => {
    setBreadcrumbs(getMatchedRoutes(pathname))
  }, [pathname])
  return (
    <div className={cns}>
      <Breadcrumb>
        {
          breadcrumbs.map((item, index) => {
            return (
              <Breadcrumb.Item key={index}>{item.meta.title}</Breadcrumb.Item>
            )
          })
        }
      </Breadcrumb>
    </div>
  )
}

export default AppBreadcrumb
