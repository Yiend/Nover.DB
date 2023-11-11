import React from 'react'
import ReactDOM from 'react-dom/client'
import 'normalize.css/normalize.css'
import './index.scss'
import reportWebVitals from './reportWebVitals'
import 'element-theme-default'
import App from '@/App'
import '@/locale'
import '@/icons'
import { Suspense } from 'react'
import { BrowserRouter as Router } from 'react-router-dom'

const loadingRender = () => {
  return <div>loading</div>
}
const root = ReactDOM.createRoot(document.getElementById('root'))
root.render(
  <Suspense fallback={loadingRender()}>
    <Router>
      <App />
    </Router>
  </Suspense>
)

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals()
