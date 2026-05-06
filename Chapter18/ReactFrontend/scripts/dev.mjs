import { spawn } from 'node:child_process';
import fs from 'node:fs';
import path from 'node:path';

const port = process.env.PORT ?? '5173';
const blazorBaseUrl = (process.env.BLAZOR_BASE_URL ?? 'https://localhost:7154').replace(/\/$/, '');
const vitePath = path.resolve('node_modules', 'vite', 'bin', 'vite.js');
const runtimeConfigPath = path.resolve('public', 'runtime-config.js');

fs.writeFileSync(
  runtimeConfigPath,
  `window.__appConfig = { blazorBaseUrl: ${JSON.stringify(blazorBaseUrl)} };\n`,
);

const child = spawn(process.execPath, [vitePath, '--port', port, '--strictPort'], {
  stdio: 'inherit',
  env: process.env,
});

child.on('exit', (code) => {
  process.exit(code ?? 0);
});
