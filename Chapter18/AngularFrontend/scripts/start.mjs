import { spawn } from 'node:child_process';
import fs from 'node:fs';
import path from 'node:path';

const port = process.env.PORT ?? '4200';
const blazorBaseUrl = (process.env.BLAZOR_BASE_URL ?? 'https://localhost:7154').replace(/\/$/, '');
const cliPath = path.resolve('node_modules', '@angular', 'cli', 'bin', 'ng.js');
const runtimeConfigPath = path.resolve('public', 'runtime-config.js');

fs.writeFileSync(
  runtimeConfigPath,
  `window.__appConfig = { blazorBaseUrl: ${JSON.stringify(blazorBaseUrl)} };\n`,
);

const child = spawn(process.execPath, [cliPath, 'serve', '--port', port], {
  stdio: 'inherit',
  env: process.env,
});

child.on('exit', (code) => {
  process.exit(code ?? 0);
});
