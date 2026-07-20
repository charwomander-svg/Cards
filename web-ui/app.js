// UI client for richer web UI
const undoCountEl = () => document.getElementById('undoCount');
const redoCountEl = () => document.getElementById('redoCount');
const nextRedoEl = () => document.getElementById('nextRedo');
const consoleEl = () => document.getElementById('console');
const snapshotEl = () => document.getElementById('snapshot');
const actionsEl = () => document.getElementById('actions');
const gameSelect = () => document.getElementById('gameSelect');

async function api(path, opts) {
  try {
    const res = await fetch(path, opts);
    if (!res.ok) throw new Error(`HTTP ${res.status}`);
    return await res.json().catch(() => null);
  } catch (e) {
    appendConsole(`API error ${path}: ${e}`);
    return null;
  }
}

function appendConsole(txt) {
  consoleEl().textContent += '\n' + txt;
}

async function refreshStatus() {
  const s = await api('/api/session/status');
  if (!s) return;
  undoCountEl().textContent = s.undoCount ?? 0;
  redoCountEl().textContent = s.redoCount ?? 0;
  nextRedoEl().textContent = s.topRedoLabel ?? '(none)';
}

async function refreshGames() {
  const games = await api('/api/games');
  if (!games) return;
  const sel = gameSelect();
  sel.innerHTML = '';
  games.forEach(g => {
    const opt = document.createElement('option');
    opt.value = g.name;
    opt.textContent = `${g.name} — ${g.players} — ${g.category}`;
    sel.appendChild(opt);
  });
}

async function refreshSnapshot() {
  const snap = await api('/api/session/snapshot');
  if (!snap) return;
  if (snap.message === 'no session') {
    snapshotEl().textContent = '(no session)';
    actionsEl().innerHTML = '';
    return;
  }

  snapshotEl().textContent = JSON.stringify(snap, null, 2);
  // render piles and hands visually
  const pilesEl = document.getElementById('piles');
  const handsEl = document.getElementById('hands');
  pilesEl.innerHTML = '';
  handsEl.innerHTML = '';
  if (snap.piles) {
    Object.entries(snap.piles).forEach(([k, arr]) => {
        // render as stacked if more than 6 cards
        if (arr.length > 6) {
          const wrapper = document.createElement('div');
          wrapper.className = 'stacked';
          arr.forEach((card, i) => {
            const c = makeCardEl(card);
            c.style.top = (i * 12) + 'px';
            c.style.zIndex = i;
            // allow click-to-select
            c.addEventListener('click', () => c.classList.toggle('selected'));
            wrapper.appendChild(c);
          });
          pilesEl.appendChild(wrapper);
        } else {
          arr.forEach(card => {
            const c = makeCardEl(card);
            c.style.margin = '2px';
            c.addEventListener('click', () => c.classList.toggle('selected'));
            pilesEl.appendChild(c);
          });
        }
    });
  }
  if (snap.hands) {
    Object.entries(snap.hands).forEach(([k, arr]) => {
        arr.forEach(card => {
          const c = makeCardEl(card);
          c.style.margin = '2px';
          c.addEventListener('click', () => c.classList.toggle('selected'));
          handsEl.appendChild(c);
        });
    });
  }

  const actions = await api('/api/session/actions');
  actionsEl().innerHTML = '';
  if (actions) {
    actions.forEach((a, idx) => {
      const btn = document.createElement('button');
      btn.className = 'btn';
      btn.style.marginRight = '8px';
      btn.textContent = `${idx+1}. ${a.label}`;
      btn.onclick = async () => {
        const selectedEls = Array.from(document.querySelectorAll('.card.selected'));
        const selected = selectedEls.map(c => c.title);
        const res = await api('/api/session/action', { method: 'POST', headers:{'content-type':'application/json'}, body: JSON.stringify({ index: idx, selected }) });
        appendConsole(`Action ${idx+1}: ${res?.message ?? 'no response'}`);
        // animate and remove selected cards for visual feedback
        selectedEls.forEach(c => c.classList.add('fade-out'));
        setTimeout(() => selectedEls.forEach(c => c.remove()), 420);
        // clear selection state
        document.querySelectorAll('.card.selected').forEach(c => c.classList.remove('selected'));
        await refreshAll();
      };
      const help = document.createElement('div');
      help.style.fontSize = '12px';
      help.style.color = '#444';
      help.textContent = a.help ?? '';
      const wrapper = document.createElement('div');
      wrapper.style.marginBottom = '6px';
      wrapper.appendChild(btn);
      wrapper.appendChild(help);
      actionsEl().appendChild(wrapper);
    });
  }
}

async function callUndo(count = 1) {
  const r = await api('/api/session/undo', { method:'POST', headers:{'content-type':'application/json'}, body: JSON.stringify({ count }) });
  appendConsole('Undo: ' + (r?.message ?? 'no response'));
  await refreshAll();
}

async function callRedo(count = 1) {
  const r = await api('/api/session/redo', { method:'POST', headers:{'content-type':'application/json'}, body: JSON.stringify({ count }) });
  appendConsole('Redo: ' + (r?.message ?? 'no response'));
  await refreshAll();
}

async function callStart(gameName) {
  gameName = gameName || gameSelect().value || 'Klondike';
  const r = await api('/api/session/start', { method:'POST', headers:{'content-type':'application/json'}, body: JSON.stringify({ gameName }) });
  appendConsole('Start: ' + (r?.message ?? 'no response'));
  await refreshAll();
}

async function callSave() {
  const r = await api('/api/session/save', { method:'POST' });
  appendConsole('Save: ' + (r?.message ?? 'no response'));
}

async function callLoad() {
  const r = await api('/api/session/load', { method:'POST' });
  appendConsole('Load: ' + (r?.message ?? 'no response'));
  await refreshAll();
}

async function callAction(index) {
  const res = await api('/api/session/action', { method: 'POST', headers:{'content-type':'application/json'}, body: JSON.stringify({ index }) });
  appendConsole(`Action ${index}: ${res?.message ?? 'no response'}`);
  await refreshAll();
}

function makeCardEl(cardStr){
  const el = document.createElement('div');
  el.className = 'card';
  // cardStr expected like "Ace of Spades" or "10 of Hearts"
  const parts = cardStr.split(' of ');
  const rank = parts[0];
  const suit = parts[1] ?? '';
  // map suits to glyphs
  const suitMap = { 'Hearts':'♥', 'Diamonds':'♦', 'Clubs':'♣', 'Spades':'♠' };
  let suitGlyph = '';
  for (const key of Object.keys(suitMap)) if (suit.toLowerCase().includes(key.toLowerCase())) suitGlyph = suitMap[key];
  const rankText = rank === '10' ? '10' : (rank[0] || rank);
  const rankEl = document.createElement('div');
  rankEl.className = 'rank';
  rankEl.textContent = rankText;
  const suitEl = document.createElement('div');
  suitEl.className = 'suit';
  suitEl.textContent = suitGlyph;
  // color by suit
  if (/Hearts|Diamonds/i.test(suit)) el.classList.add('red'); else el.classList.add('black');
  el.appendChild(rankEl);
  el.appendChild(suitEl);
  el.title = cardStr;
  return el;
}

async function refreshAll() {
  await refreshStatus();
  await refreshSnapshot();
}

// hooks
window.addEventListener('load', () => {
  document.getElementById('undoBtn').addEventListener('click', () => callUndo(1));
  document.getElementById('redoBtn').addEventListener('click', () => callRedo(1));
  document.getElementById('startBtn').addEventListener('click', () => callStart());
  document.getElementById('saveBtn').addEventListener('click', () => callSave());
  document.getElementById('loadBtn').addEventListener('click', () => callLoad());
  document.getElementById('gameSelect').addEventListener('dblclick', () => callStart());
  document.getElementById('toggleCompact').addEventListener('click', () => {
    document.body.classList.toggle('compact');
    appendConsole('Toggled compact mode');
    refreshAll();
  });

  // Keyboard shortcuts
  window.addEventListener('keydown', (e) => {
    // avoid typing in inputs
    if (e.target && (e.target.tagName === 'INPUT' || e.target.tagName === 'TEXTAREA' || e.target.isContentEditable)) return;
    if (e.code === 'Space') { e.preventDefault(); callUndo(1); appendConsole('Shortcut: Undo'); }
    if (e.key === 'r' || e.key === 'R') { callRedo(1); appendConsole('Shortcut: Redo'); }
    if (e.key === 'c' || e.key === 'C') { document.getElementById('toggleCompact').click(); }
    if (e.key === 's' || e.key === 'S') { callStart(); }
    // number keys 1-9 trigger actions
    if (/^[1-9]$/.test(e.key)) {
      const idx = parseInt(e.key, 10) - 1;
      callAction(idx);
    }
  });

  setInterval(refreshAll, 2000);
  refreshGames().then(refreshAll);
});