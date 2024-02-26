import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import DropZone from 'dropzone-vue';
import 'dropzone-vue/dist/dropzone-vue.common.css';

const app = createApp(App)

app.use(router)
app.use(DropZone)

app.mount('#app')
