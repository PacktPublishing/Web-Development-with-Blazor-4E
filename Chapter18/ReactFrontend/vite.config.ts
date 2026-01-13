import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig(() => {
    const port = Number(process.env.PORT) || 5173

    return {
        plugins: [react()],
        server: {
            port,
            strictPort: true
        }
    }
})