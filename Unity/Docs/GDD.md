# GDD – Slot 5x3 estilo Playson (Resumo)

Versão: 1.0  
Alvo: WebGL, Android (ARM64), Windows  
Tema: Roxo (#6A0DAD) + branco, metálico brilhante

1) Visão Geral
- Slot 5x3 com 20–25 linhas (default 25).  
- Volatilidade média/alta, RTP configurável 85%–97%.  
- Símbolos: baixos (A,K,Q,J), médios (Pedra Roxa, Moeda Branca, Místico), altos (Diamante Roxo, Coroa Branca), especiais (Wild, Scatter, Bonus Coin).

2) Fluxo de Estados
Idle -> Spin -> Resolve -> (Bonus Hold&Win opcional) -> (Free Spins opcional) -> End -> Idle

3) Tabela de Pagamentos (por linha, multiplicador do bet)
- J,Q: 3=1x, 4=2x, 5=5x  
- K,A: 3=2x, 4=5x, 5=10x  
- Pedra Roxa, Moeda Branca: 3=5x, 4=10x, 5=20x  
- Místico: 3=10x, 4=15x, 5=20x  
- Diamante Roxo: 3=25x, 4=50x, 5=100x  
- Coroa Branca: 3=40x, 4=80x, 5=150x  
- Wild substitui todos menos Scatter/Bonus.  
- Scatter aciona Free Spins (>=3).  
- Bonus Coin aciona Hold&Win (>=3).

4) RNG, RTP e Volatilidade
- RNGService seedable para replays.  
- Ajustes futuros de RTP podem aplicar escala no pagamento/weights.  
- Três predefinições de pesos de símbolos por volatilidade.

5) Bonus Hold & Win
- Inicia com 3 spins. Cada moeda coletada preenche slot e reseta contagem.  
- Moedas (multiplicadores): Bronze 1x, Silver 5x, Gold 10x, Mega 25x.  
- Pots fixos (multiplicadores do bet): Mini 20x, Major 100x, Grand 500x, Ultra 1000x.  
- Fim: sem novas moedas (spins=0) ou grade completa.

6) Free Spins
- 10 giros com multiplicador progressivo (+1 a cada 2 vitórias).  
- Layout e música distintos (a definir em assets).  
- UI mostra contador e multiplicador.

7) UI/UX
- HUD com saldo, aposta, botões Spin/Autoplay/MaxBet.  
- Destaques de vitória, partículas, e tela Big Win.  
- Transições de layout ao entrar/saír do bônus.

8) Estrutura de Projeto
- Assets/Scripts: módulos centrais (SpinManager, RNGService, PaylinesManager, SymbolManager, BonusPotController, FreeSpinsController, UIManager, SoundManager, AnimationManager, PoolManager, SaveLoadManager, DemoDataLoader, DevPanel, Bootstrap).  
- Assets/Resources/DemoSamples: sample_demo.json.  
- Scenes: Main, Bonus, FreeSpins (a criar no Editor).  
- Docs: este GDD.

9) Requisitos de Build e Otimização
- Alvo 60 FPS; pooling para símbolos; LOD para VFX.  
- Mobile: reduzir partículas/sombras quando FPS<45.  
- Texturas com compressão apropriada; atlas packing.

10) QA e Ferramentas
- DevPanel (F1) com sliders de RTP/linhas/volatilidade.  
- Logs por giro; carregamento de Demo JSON (URL/Resources).

11) Próximos Passos (Assets)
- Criar artes originais (sprites/atlas) e SFX originais (ou royalty-free).  
- Mapear animações reais nos hooks do AnimationManager/SoundManager.
