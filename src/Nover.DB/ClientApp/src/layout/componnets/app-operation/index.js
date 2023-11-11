import './index.scss'
import classNames from 'classnames'
import CuSvgIcon from '@/components/cu-svg-icon'
import { Dropdown } from 'element-react'
import UserStore from '@/store/users/modules/user'
import { useNavigate } from 'react-router-dom'

const AppOperation = ({ className }) => {
  const cns = classNames(
    'app-operation',
    className
  )

  const handleCommand = (command) => {
    switch (command) {
      case 'logout':
        logout()
        break
      default:
    }
  }

  const navigate = useNavigate()

  const logout = () => {
    UserStore.removeToken()
    navigate('/login')
  }

  return (
    <div className={cns}>
      <Dropdown
        menu={(
          <Dropdown.Menu >
            <Dropdown.Item command="logout">退出登录</Dropdown.Item>
          </Dropdown.Menu>
        )}
        onCommand={handleCommand}
      >
        <span className="el-dropdown-link">
          <CuSvgIcon className="app-operation__account"
            iconClass="person"
          />
        </span>
      </Dropdown>
    </div>
  )
}

export default AppOperation
