import http from'../http_common'

export default function authTokenRequest(token) {
    if (token) {
      http.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    } else {
      delete http.defaults.headers.common['Authorization'];
    }
  }