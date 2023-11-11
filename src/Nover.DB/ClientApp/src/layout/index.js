import './index.scss'
import React from 'react'
import AppHeaderBar from '@/layout/componnets/app-header-bar'
import AppSidebar from '@/layout/componnets/app-sidebar'
import AppMain from '@/layout/componnets/app-main'
import AppNavigationBar from '@/layout/componnets/app-navigation-bar'

class Layout extends React.Component {
  render() {
    return (
      <div className="layout">
        <AppHeaderBar className="layout__header" />

        <div className="layout__ship">
          <AppSidebar className="layout__sidebar" />
          <div className="layout__cabin">
            <AppNavigationBar className="layout__navigation-bar" />
            <AppMain />
          </div>
        </div>
      </div>
    )
  }
}

export default Layout
