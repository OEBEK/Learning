// src/api/client.js
import axios from 'axios'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'https://localhost:58028/api',
  headers: {
    'Content-Type': 'application/json',
  },
})

// Optional: add interceptor for tokens or logging
api.interceptors.response.use(
  response => response,
  error => {
    console.error('API Error:', error)
    return Promise.reject(error)
  }
)

export default api
