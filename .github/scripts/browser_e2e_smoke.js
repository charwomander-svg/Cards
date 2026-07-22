let chromium;
try {
  ({ chromium } = require('playwright'));
} catch {
  ({ chromium } = require('../../playtests/node_modules/playwright'));
}

const baseUrl = process.env.ADAPTER_URL || 'http://localhost:5000';

async function run() {
  const browser = await chromium.launch({ headless: true });
  const page = await browser.newPage();
  const pageErrors = [];
  const consoleLogs = [];
  page.on('pageerror', (err) => pageErrors.push(String(err)));
  page.on('console', (msg) => consoleLogs.push(`${msg.type()}: ${msg.text()}`));

  await page.goto(`${baseUrl}/index.html`, { waitUntil: 'domcontentloaded', timeout: 60000 });
  await page.waitForSelector('#gameSelect', { timeout: 60000 });
  await page.waitForFunction(() => {
    const sel = document.querySelector('#gameSelect');
    return !!sel && sel.options.length > 0;
  }, null, { timeout: 60000 });

  const gameValues = await page.$$eval('#gameSelect option', (opts) => opts.map((o) => o.value).filter(Boolean));
  const sample = gameValues.slice(0, Math.min(5, gameValues.length));
  if (sample.length === 0) throw new Error('No games loaded in UI');

  for (const gameName of sample) {
    await page.selectOption('#gameSelect', gameName);
    await page.click('#startBtn');
    await page.waitForTimeout(300);

    const snapshotText = await page.$eval('#snapshot', (el) => el.textContent || '');
    if (snapshotText.includes('(no session)')) {
      throw new Error(`Session did not start for game: ${gameName}`);
    }

    const actionButtons = await page.$$('#actions button.btn');
    if (actionButtons.length > 0) {
      await actionButtons[0].click();
      await page.waitForTimeout(300);
    }
  }

  if (pageErrors.length > 0) {
    throw new Error(`Browser page errors: ${pageErrors.slice(0, 3).join(' | ')}; console: ${consoleLogs.slice(0, 5).join(' | ')}`);
  }

  await browser.close();
  console.log(`Browser E2E smoke passed for ${sample.length} games`);
}

run().catch((err) => {
  console.error(err);
  process.exit(1);
});
