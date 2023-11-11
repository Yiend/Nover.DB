import { legacy_createStore as createStore } from 'redux'
import { getToken, setToken } from '@/utils/auth-utils'

const reducer = (state = { token: '' }, action) => {
  const { type, payload } = action
  switch (type) {
    case 'SET_TOKEN':
      state.token = payload
      setToken(payload)
      return state
    default:
      return state
  }
}

const store = createStore(reducer)

const actions = {

  setToken(val) {
    store.dispatch({
      type: 'SET_TOKEN',
      payload: val
    })
  },

  removeToken() {
    this.setToken('')
  },

  getToken() {
    return getToken() || store.getState()?.token
  }
}

const UserStore = {
  ...actions
}

export default UserStore
