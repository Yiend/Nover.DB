import { legacy_createStore as createStore } from 'redux'

const reducer = (state = { routes: [] }, action) => {
  const { type, payload } = action
  switch (type) {
    case 'SET_ROUTES':
      state.routes = payload
      return state
    default:
      return state
  }
}

const store = createStore(reducer)

const actions = {

  setRoutes(val) {
    store.dispatch({
      type: 'SET_ROUTES',
      payload: val
    })
  },

  getRoutes() {
    return store.getState()?.routes
  }
}

const PermissionStore = {
  ...actions
}

export default PermissionStore
