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
          wrapper.className = 'stacked pile';
          wrapper.dataset.pileId = k;
          arr.forEach((card, i) => {
            const c = makeCardEl(card);
            c.style.top = (i * 12) + 'px';
            c.style.zIndex = i;
            // allow click-to-select
            c.addEventListener('click', () => {
              c.classList.toggle('selected');
              previewSelection();
            });
            wrapper.appendChild(c);
          });
          pilesEl.appendChild(wrapper);
        } else {
          const pileWrapper = document.createElement('div');
          pileWrapper.className = 'pile';
          pileWrapper.dataset.pileId = k;
          arr.forEach(card => {
            const c = makeCardEl(card);
            c.style.margin = '2px';
            c.addEventListener('click', () => { c.classList.toggle('selected'); previewSelection(); });
            pileWrapper.appendChild(c);
          });
          pilesEl.appendChild(pileWrapper);
        }
    });

    // after rendering piles, measure their DOM rects and POST layout to server
    postPileLayoutDebounced();

    // keep layout updated: on resize and DOM changes
    window.addEventListener('resize', postPileLayoutDebounced);
    if (typeof MutationObserver !== 'undefined') {
      const mo = new MutationObserver(postPileLayoutDebounced);
      mo.observe(pilesEl, { childList: true, subtree: true, attributes: true });
    } else {
      // fallback: periodic update
      setInterval(postPileLayoutDebounced, 2000);
    }
  }
  if (snap.hands) {
    Object.entries(snap.hands).forEach(([k, arr]) => {
        const handWrapper = document.createElement('div');
        handWrapper.dataset.handId = k;
        arr.forEach(card => {
          const c = makeCardEl(card);
          c.style.margin = '2px';
          c.addEventListener('click', () => { c.classList.toggle('selected'); previewSelection(); });
          handWrapper.appendChild(c);
        });
        handsEl.appendChild(handWrapper);
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
      btn.dataset.actionIndex = idx;
      btn.onclick = async () => {
        const selectedEls = Array.from(document.querySelectorAll('.card.selected'));
        const selected = selectedEls.map(c => c.title);
        const res = await api('/api/session/action', { method: 'POST', headers:{'content-type':'application/json'}, body: JSON.stringify({ index: idx, selected, sourceItems: getSelectedSourceItems() }) });
        appendConsole(`Action ${idx+1}: ${res?.message ?? 'no response'}`);
        // if server returned applied move info, animate moving cards from source to destination
        if (res && res.applied && Array.isArray(res.applied.cards) && res.applied.cards.length > 0) {
                  await animateMove(res.applied);
        } else {
          selectedEls.forEach(c => c.classList.add('fade-out'));
          setTimeout(() => selectedEls.forEach(c => c.remove()), 420);
        }
        // clear selection state
        document.querySelectorAll('.card.selected').forEach(c => c.classList.remove('selected'));
        // clear highlights
        document.querySelectorAll('.btn.highlight').forEach(b => b.classList.remove('highlight'));
        document.getElementById('selectionChoices').innerHTML = '';
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
  const res = await api('/api/session/action', { method: 'POST', headers:{'content-type':'application/json'}, body: JSON.stringify({ index, sourceItems: getSelectedSourceItems() }) });
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

// Preview selection to server and highlight matching actions
let _previewTimer = null;
async function previewSelection() {
  // debounce quick clicks
  if (_previewTimer) clearTimeout(_previewTimer);
  _previewTimer = setTimeout(async () => {
    const selected = Array.from(document.querySelectorAll('.card.selected')).map(c => c.title);
    const choicesEl = document.getElementById('selectionChoices');
    choicesEl.innerHTML = '';
    // clear previous highlights
    document.querySelectorAll('.btn.highlight').forEach(b => b.classList.remove('highlight'));

    const spinner = document.getElementById('previewSpinner');
    spinner.style.display = 'block';
    if (selected.length === 0) { spinner.style.display = 'none'; return; }
    const res = await api('/api/session/selection-preview', { method: 'POST', headers: {'content-type':'application/json'}, body: JSON.stringify({ selected }) });
    spinner.style.display = 'none';
    if (!res) return;
    const matches = res.matches ?? [];
  if (matches.length === 0) {
    choicesEl.textContent = 'No matching actions for selection.';
    return;
  }
  // Highlight matching action buttons
  matches.forEach(m => {
    const btn = document.querySelector(`button[data-action-index='${m.index}']`);
    if (btn) btn.classList.add('highlight');
  });
  if (matches.length > 1) {
    const intro = document.createElement('div');
    intro.textContent = 'Ambiguous selection — choose action:';
    choicesEl.appendChild(intro);
    matches.forEach(m => {
      const b = document.createElement('button');
      b.className = 'btn secondary';
      b.style.marginRight = '6px';
      b.textContent = `${m.index+1}. ${m.label}`;
      b.onclick = async () => {
        // apply chosen action
        const result = await api('/api/session/action', { method: 'POST', headers: {'content-type':'application/json'}, body: JSON.stringify({ index: m.index, selected, sourceItems: getSelectedSourceItems() }) });
        appendConsole(`Chose action ${m.index+1}: ${result?.message ?? 'no response'}`);
        // if server returned applied move info, animate move
        if (result && result.applied && Array.isArray(result.applied.cards) && result.applied.cards.length > 0) {
                  await animateMove(result.applied);
        }
        // clear selection and choices
        document.querySelectorAll('.card.selected').forEach(c => c.classList.remove('selected'));
        choicesEl.innerHTML = '';
        await refreshAll();
      };
      choicesEl.appendChild(b);
    });
  } else {
    choicesEl.textContent = `Matched action: ${matches[0].label}`;
  }
}

// Animate moved cards from source to destination
async function postPileLayout() {
  try {
    const layout = {};
    document.querySelectorAll('[data-pile-id]').forEach(p => {
      const id = p.dataset.pileId;
      const r = p.getBoundingClientRect();
      layout[id] = { left: Math.round(r.left), top: Math.round(r.top), width: Math.round(r.width), height: Math.round(r.height) };
    });
    if (Object.keys(layout).length > 0) await api('/api/session/pile-layout', { method: 'POST', headers: {'content-type':'application/json'}, body: JSON.stringify(layout) });
  } catch (e) {
    console.warn('posting pile layout failed', e);
  }
}

const postPileLayoutDebounced = debounce(() => { postPileLayout(); }, 120);

// Post selected cards' source coords so server can animate from exact origins
async function postSelectedSourceLayout() {
  try {
    const els = Array.from(document.querySelectorAll('.card.selected'));
    if (els.length === 0) return;
    const items = els.map(e => {
      const r = e.getBoundingClientRect();
      return { title: e.title, left: Math.round(r.left), top: Math.round(r.top), width: Math.round(r.width), height: Math.round(r.height) };
    });
    await api('/api/session/source-layout', { method: 'POST', headers: {'content-type':'application/json'}, body: JSON.stringify({ items }) });
  } catch (e) {
    console.warn('posting source layout failed', e);
  }
}
const postSelectedSourceLayoutDebounced = debounce(() => { postSelectedSourceLayout(); }, 80);

// helper to gather selected items synchronously for action payload fallback
function getSelectedSourceItems() {
  const els = Array.from(document.querySelectorAll('.card.selected'));
  return els.map(e => {
    const r = e.getBoundingClientRect();
    return { title: e.title, left: Math.round(r.left), top: Math.round(r.top), width: Math.round(r.width), height: Math.round(r.height) };
  });
}

// observe selection changes (class toggles) to post source coords proactively
if (typeof MutationObserver !== 'undefined') {
  const selObserver = new MutationObserver((mutations) => {
    for (const m of mutations) {
      if (m.type === 'attributes' && m.attributeName === 'class') {
        const target = m.target;
        if (target && target.classList && target.classList.contains('card')) {
          postSelectedSourceLayoutDebounced();
          break;
        }
      }
    }
  });
  selObserver.observe(document.body, { attributes: true, subtree: true, attributeFilter: ['class'] });
}

async function animateMove(applied) {
  try {
    const cardTitles = applied.cards || [];
    const clones = [];
    for (const title of cardTitles) {
      // prefer server-provided sourceItems positions when available
      let rect = null;
      if (applied.sourceItems && Array.isArray(applied.sourceItems)) {
        const si = applied.sourceItems.find(s => (s && (s.title || s.Title || s.name) && String((s.title||s.Title||s.name)).toLowerCase() === String(title).toLowerCase()));
        if (si) {
          rect = { left: Number(si.left) || Number(si.x) || 0, top: Number(si.top) || Number(si.y) || 0, width: Number(si.width) || Number(si.w) || 0, height: Number(si.height) || Number(si.h) || 0 };
        }
      }
      // fallback to DOM element rect when no server-provided origin
      const sel = `.card[title="${title.replace(/"/g, '\\"')}"]`;
      const el = document.querySelector(sel);
      if (!rect && !el) continue;
      if (!rect) rect = el.getBoundingClientRect();
      const clone = (el ? el.cloneNode(true) : document.createElement('div'));
      clone.classList.add('move-clone');
      clone.style.left = rect.left + 'px';
      clone.style.top = rect.top + 'px';
      clone.style.width = rect.width + 'px';
      clone.style.height = rect.height + 'px';
      clone.style.transform = 'none';
      document.body.appendChild(clone);
      clones.push({ clone, from: rect });
    }

    // determine destination rect: prefer server-provided destinationCoords, else fallback to DOM lookup
    let destRect = null;
    if (applied.destinationCoords) {
      const d = applied.destinationCoords;
      // expect { left, top, width, height }
      destRect = { left: d.left || d.x || 0, top: d.top || d.y || 0, width: d.width || d.w || 0, height: d.height || d.h || 0 };
    } else if (applied.destination) {
      const piles = Array.from(document.querySelectorAll('[data-pile-id]'));
      const p = piles.find(p => p.dataset.pileId && p.dataset.pileId.toLowerCase() === String(applied.destination).toLowerCase())
             || piles.find(p => p.textContent && p.textContent.toLowerCase().includes(String(applied.destination).toLowerCase()));
      destRect = p ? p.getBoundingClientRect() : null;
    }

    if (!destRect) destRect = { left: window.innerWidth/2, top: window.innerHeight/2, width:0, height:0 };

    // auto-scroll destination into view for better animation (if dest element exists)
    if (applied.destination && !applied.destinationCoords) {
      const piles = Array.from(document.querySelectorAll('[data-pile-id]'));
      const p = piles.find(p => p.dataset.pileId && p.dataset.pileId.toLowerCase() === String(applied.destination).toLowerCase())
             || piles.find(p => p.textContent && p.textContent.toLowerCase().includes(String(applied.destination).toLowerCase()));
      if (p && typeof p.scrollIntoView === 'function') p.scrollIntoView({ behavior: 'smooth', block: 'center', inline: 'center' });
    } else if (applied.destinationCoords) {
      // ensure destination coords are visible by attempting to scroll window if needed
      const d = applied.destinationCoords;
      if (d && typeof d.top === 'number') window.scrollTo({ top: Math.max(0, d.top - window.innerHeight/2), behavior: 'smooth' });
    }

    // animate clones with stagger and easing
    for (let i = 0; i < clones.length; i++) {
      const item = clones[i];
      const dx = destRect.left + destRect.width/2 - (item.from.left + item.from.width/2);
      const dy = destRect.top + destRect.height/2 - (item.from.top + item.from.height/2);
      // apply transition styles
      item.clone.style.transition = 'transform 420ms cubic-bezier(0.2, 0.8, 0.2, 1), opacity 300ms linear';
      // stagger using timeout so clones fly in sequence
      setTimeout(() => {
        requestAnimationFrame(() => {
          item.clone.style.transform = `translate(${dx}px, ${dy}px) scale(0.92)`;
          item.clone.style.opacity = '0.0';
        });
      }, i * 60);
    }

    // wait for last animation to finish (stagger + duration)
    const totalWait = 60 * Math.max(0, clones.length - 1) + 460;
    await new Promise(r => setTimeout(r, totalWait));
    clones.forEach(c => c.clone.remove());
  } catch (e) {
    console.error('animateMove error', e);
  }
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