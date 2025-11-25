# Slot Game Art Assets - Playson Style

## 📋 Visão Geral
Esta pasta contém todos os assets visuais do jogo de slot, criados com estilo inspirado na Playson. Todos os símbolos possuem fundo transparente e qualidade premium para animações suaves.

---

## 🎰 Símbolos do Jogo

### Símbolos de Cartas (Card Symbols)
Símbolos de baixo valor com molduras ornamentadas e gemas:

- **A.png** (512x512px) - Ás com moldura dourada e gemas roxas
- **K.png** (512x512px) - Rei com moldura dourada e rubis vermelhos
- **Q.png** (512x512px) - Rainha com moldura dourada e safiras azuis
- **J.png** (512x512px) - Valete com moldura dourada e esmeraldas verdes
- **10.png** (512x512px) - Número 10 com moldura dourada e diamantes

**Características:**
- Estilo metálico dourado luxuoso
- Gemas coloridas incrustadas
- Fundo totalmente transparente
- Alta definição para animações

### Símbolos Especiais (Special Symbols)

#### **wild.png** (512x512px)
- Texto "WILD" em dourado brilhante
- Efeitos de raios elétricos roxos e azuis
- Moldura ornamentada dourada
- Aura mágica com partículas
- **Uso:** Substitui outros símbolos (exceto Scatter e Bonus)

#### **Scatter.png** (512x512px)
- Estrela dourada radiante com 8 pontas
- Texto "SCATTER" em dourado
- Raios de luz emanando
- Brilho intenso amarelo-dourado
- **Uso:** 3+ ativa Free Spins

#### **Bonus.png** (512x512px)
- Moeda de ouro grande e ornamentada
- Texto "BONUS" em relevo
- Detalhes gravados complexos
- Superfície metálica reflexiva
- **Uso:** Ativa modo Hold & Win

---

## 🎨 Backgrounds Temáticos

### **background.png** (1920x1080px)
**Main Game Background**
- Cortinas de veludo roxo luxuosas
- Moldura dourada ornamentada
- Ambiente de cassino elegante
- Padrões decorativos em ouro
- **Cena:** Main.unity

### **bonus2.png** (1920x1080px)
**Bonus Game Background**
- Sala do tesouro dourada
- Potes de ouro e moedas espalhadas
- Atmosfera quente laranja/dourada
- Efeitos de brilho mágico
- **Cena:** Bonus.unity

### **freespins.png** (1920x1080px)
**Free Spins Background**
- Céu noturno místico
- Via Láctea brilhante
- Gradiente azul/roxo/rosa
- Montanhas ao horizonte
- Atmosfera etérea e mágica
- **Cena:** FreeSpins.unity

---

## 🖼️ Elementos UI

### **moldura.png** (1024x1024px)
- Moldura dourada ornamental
- Cantos decorativos com gemas
- Padrões intrincados
- Centro transparente
- **Uso:** Bordas de UI, frames de reels

### **pot.png** (512x512px)
- Pote de ouro transbordando moedas
- Superfície metálica brilhante
- Partículas de luz dourada
- **Uso:** Ícone de jackpot, prêmios

### **Coin.png** (512x512px)
- Moeda de ouro com gravuras
- Superfície metálica com reflexos
- Padrões ornamentados
- **Uso:** Animações de vitória, contadores

### **diamond.png** (512x512px)
- Diamante brilhante em corte premium
- Reflexos iridescentes azul/rosa
- Efeitos de brilho intenso
- **Uso:** Símbolos de alto valor, decoração

### **roundedSpin.png** (512x512px)
- Botão circular "SPIN" dourado
- Efeito de glow verde
- Borda metálica brilhante
- Design circular premium
- **Uso:** Botão principal de spin

---

## 📐 Especificações Técnicas

### Formatos e Resolução
- **Formato:** PNG com canal alpha (fundo transparente)
- **Símbolos:** 512x512px
- **Backgrounds:** 1920x1080px (16:9)
- **UI Elements:** 512x512px ou 1024x1024px
- **DPI:** 300 para qualidade premium

### Paleta de Cores Principal
- **Ouro:** #FFD700, #FFA500
- **Roxo:** #8B00FF, #9400D3
- **Vermelho:** #DC143C, #FF4500
- **Azul:** #1E90FF, #4169E1
- **Verde:** #00FF7F, #3CB371

### Estilo Visual
- **Inspiração:** Playson casino games
- **Estética:** Luxuosa, metálica, brilhante
- **Efeitos:** Glow, reflexos, sombras suaves
- **Molduras:** Ornamentadas com gemas
- **Transparência:** Alpha channel real (não simulada)

---

## 🔧 Integração no Unity

### Import Settings Recomendadas
```
Texture Type: Sprite (2D and UI)
Pixels Per Unit: 100
Filter Mode: Bilinear
Max Size: 2048 (símbolos) / 4096 (backgrounds)
Compression: High Quality
Alpha Is Transparency: Enabled
```

### Uso com Animações
Todos os símbolos foram projetados para:
- ✅ Glow effects (SymbolAnimator.cs)
- ✅ Particle systems (ParticleWinSystem.cs)
- ✅ Escala/rotação (sem perda de qualidade)
- ✅ Transições suaves (TransitionManager.cs)

---

## 📝 Notas de Produção

### Versão 1.0 - Assets Iniciais
- ✅ Símbolos de cartas (A, K, Q, J, 10)
- ✅ Símbolos especiais (Wild, Scatter, Bonus)
- ✅ Backgrounds temáticos (3 cenas)
- ✅ Elementos UI principais
- ✅ Fundo transparente real (não simulado)

---

## 🎨 Diretrizes de Arte

### Consistência Visual
- Manter paleta de cores dourada/luxuosa
- Usar gemas coloridas consistentes por símbolo
- Molduras ornamentadas em todos os símbolos
- Efeitos de brilho metálico

### Performance
- Usar transparência real (alpha channel)
- Comprimir sem perder qualidade visual
- Agrupar sprites em atlas quando possível
- Otimizar resolução para mobile se necessário

---

**Gerado por:** Lovable AI Image Generator (Flux.dev model)  
**Estilo:** Inspirado em Playson casino games
