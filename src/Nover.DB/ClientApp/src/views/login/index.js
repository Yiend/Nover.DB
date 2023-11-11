import './index.scss'
import { Form, Input, Button } from 'element-react'
import { useTranslation } from 'react-i18next'
import { useRef, useState } from 'react'
import CuSvgIcon from '@/components/cu-svg-icon'
import UserStore from '@/store/users/modules/user'
import { useNavigate } from 'react-router-dom'

const Login = () => {
  const { t } = useTranslation()

  const navigate = useNavigate()

  const formDataState = {
    username: '',
    password: ''
  }

  const formRules = {
    username: [
      { required: true, message: '请输入用户名', trigger: 'blur' }
    ],

    password: [
      { required: true, message: '请输入密码', trigger: 'blur' }
    ]
  }

  const [formData, setFormData] = useState(formDataState)

  const [passwordType, setPasswordType] = useState('password')

  const changePasswordType = () => {
    if (passwordType === 'password') {
      setPasswordType('')
    } else {
      setPasswordType('password')
    }
  }

  const formRef = useRef()

  const changeUsername = (val) => {
    setFormData({
      ...formData,
      username: val
    })
  }

  const changePassword = (val) => {
    setFormData({
      ...formData,
      password: val
    })
  }

  const submit = () => {
    formRef.current.validate((valid) => {
      if (!valid) {
        return
      }
      // todo 通过api setToken
      UserStore.setToken('token')
      navigate('/')
    })
  }

  return (
    <div className="login">
      <div className="login__main">
        <div className="login__title">{t('login.title')}</div>

        <Form
          className="login__form"
          model={formData}
          ref={formRef}
          rules={formRules}
        >
          <Form.Item
            className="login__form-item"
            prop="username"
          >
            <span className="login__svg-container">
              <CuSvgIcon iconClass="user" />
            </span>
            <Input
              className="login__input"
              onChange={changeUsername}
              placeholder={t('login.username')}
              value={formData.username}
            />
          </Form.Item>

          <Form.Item
            className="login__form-item"
            prop="password"
          >
            <span className="login__svg-container">
              <CuSvgIcon iconClass="password" />
            </span>
            <Input
              className="login__input"
              onChange={changePassword}
              placeholder={t('login.password')}
              type={passwordType}
              value={formData.password}
            />
            <span
              className="login__show-pwd"
              onClick={changePasswordType}
            >
              <CuSvgIcon iconClass={passwordType === 'password' ? 'eye' : 'eye-open'} />
            </span>
          </Form.Item>
        </Form>
        <Button
          className="login__btn"
          onClick={submit}
          type="primary"
        >
          {t('login.login')}
        </Button>
      </div>
    </div>
  )
}

export default Login
