import './index.scss'
import classNames from 'classnames'

const AppHeaderBar = ({ className }) => {
  const cns = classNames(
    'app-header-bar',
    className
  )
  return <div className={cns}>React Element Admin</div>
}

export default AppHeaderBar
