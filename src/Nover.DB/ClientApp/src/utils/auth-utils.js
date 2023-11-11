import Cookies from 'js-cookie'

const tokenKey = 'REACT_ELEMENT_ADMIN_TOKEN'
export const getToken = () => {
  return Cookies.get(tokenKey)
}
export const setToken = (token) => {
  Cookies.set(tokenKey, token)
}
export const removeToken = () => {
  Cookies.remove(tokenKey)
}
