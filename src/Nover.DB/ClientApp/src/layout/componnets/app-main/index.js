import { Outlet } from 'react-router-dom'
import './index.scss'
import classNames from 'classnames'

const AppMain = ({ className }) => {
  const cns = classNames(
    'app-main',
    className
  )

  return (
    <div className={cns}>
      <Outlet />
    </div>
  )
}

export default AppMain
