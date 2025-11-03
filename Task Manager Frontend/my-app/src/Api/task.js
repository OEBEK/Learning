// src/api/users.js
import api from './client.js'

export async function getAllTasks() {
  const res = await api.get('/tasks')
  return res.data
}

export async function createTask(user) {
  const res = await api.post('/tasks', user)
  return res.data
}
