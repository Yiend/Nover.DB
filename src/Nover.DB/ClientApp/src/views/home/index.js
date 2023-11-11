import React from 'react'
import { Message } from 'element-react'
import { login } from '@/api/login'

class Home extends React.Component {
  constructor(props) {
    super(props)
    this.testElementUi = this.testElementUi.bind(this)
    this.testRequest = this.testRequest.bind(this)
  }
  testElementUi() {
    Message.success('这是一条消息提示')
  }
  testRequest() {
    login()
  }

  render() {
    return (
      <div>
        <h1 onClick={this.testRequest}>Hello, world!</h1>
      </div>

    )
  }
}

export default Home
