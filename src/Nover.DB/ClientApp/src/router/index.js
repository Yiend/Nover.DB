import { lazy } from 'react'
import Layout from '@/layout'

const constantRoutes = [
  {
    path: '/login',
    component: lazy(() => import('@/views/login')),
    meta: { hidden: true }
  },
  {
    path: '/',
    component: Layout,
    meta: { title: '首页', icon: 'home' },
    children: [
      {
        path: '/home',
        component: lazy(() => import('@/views/home')),
        meta: { title: '系统首页' }
      },
      {
        path: '/documentation',
        component: lazy(() => import('@/views/documentation')),
        meta: { title: '文档' }
      }
    ]
  },
  {
    path: '/404',
    component: lazy(() => import('@/views/not-found')),
    meta: { title: '404', hidden: true }
  }
]

const routes = [...constantRoutes]

export default routes
