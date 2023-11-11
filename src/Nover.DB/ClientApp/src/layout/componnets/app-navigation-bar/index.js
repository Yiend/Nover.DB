import './index.scss'
import classNames from 'classnames'
import AppBreadcrumb from '@/layout/componnets/app-breadcrumb'
import AppOperation from '@/layout/componnets/app-operation'

const AppNavigationBar = ({ className }) => {
  const cns = classNames(
    'app-navigation-bar',
    className
  )
  return (
    <div className={cns}>
      <AppBreadcrumb className="app-navigation-bar__breadcrumb" />
      <AppOperation className="app-navigation-bar__operation" />
    </div>
  )
}

export default AppNavigationBar
