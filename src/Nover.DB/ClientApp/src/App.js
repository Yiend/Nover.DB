import './App.scss'
import { Route, Routes, useLocation, useNavigate } from 'react-router-dom'
import routeList from '@/router'
import { useEffect } from 'react'
import UserStore from '@/store/users/modules/user'
import PermissionStore from '@/store/users/modules/permission'

function App() {
  let renderContent = ''
  const location = useLocation()

  const navigate = useNavigate()

  const token = UserStore.getToken()

  useEffect(() => {
    const { pathname } = location
    if (pathname === '/login') {
      return
    }
    if (!token) {
      navigate('/login')
      return
    }
    PermissionStore.setRoutes(routeList)
    if (pathname === '/') {
      navigate('/home')
    }
  })

  const renderFn = (renderContent) => {
    return (
      <Routes>
        {renderContent}
      </Routes>
    )
  }

  const generateRoutes = (routes) => {
    if (!routes || routes.length === 0) {
      return
    }
    return routes.map(item => {
      return (
        <Route
          element={<item.component />}
          key={item.path}
          path={item.path}
        >
          {
            generateRoutes((item.children))
          }
        </Route>
      )
    })
  }

  const routeRender = () => {
    return generateRoutes(routeList)
  }

  renderContent = routeRender()
  return renderFn(renderContent)
}

export default App
