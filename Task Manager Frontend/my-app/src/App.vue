<script setup>
import { ref, onMounted } from 'vue'
import { getAllTasks } from './Api/task.js'

const tasks = ref([])

onMounted(async () => {
  try {
    tasks.value = await getAllTasks()
  } catch (err) {
    console.error('Failed to load users:', err)
  }
})
</script>

<template>
  <div v-for="u in tasks" :key="u.id" class="task-card">
    <h2>{{ u.title }}</h2>
    <p>{{ u.description }}</p>
    <small>Fällig: {{ new Date(u.dueDate).toLocaleDateString() }}</small>
    <small> Erstellt: {{ new Date(u.createdAt).toLocaleDateString() }}</small>
    <p>Status: {{ u.isCompleted ? 'Abgeschlossen' : 'Offen' }}</p>
  </div>
</template>
