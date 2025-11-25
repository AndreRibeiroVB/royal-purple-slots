# Cenas Unity - Guia de Importação

## Estrutura das Cenas

Este pacote contém 3 cenas Unity prontas para uso:

### 1. Main.unity
**Cena principal do jogo**
- Camera principal configurada com background roxo (#6A0DAD)
- Bootstrap GameObject com componente Bootstrap.cs anexado
- Canvas UI responsivo (1920x1080) com:
  - **SpinButton**: Botão roxo central para girar (conectado a SpinManager.Spin())
  - **BalanceText**: Display do saldo atual no canto superior esquerdo
  - **AutoplayButton**: Botão verde para autoplay (à esquerda do Spin)
  - **MaxBetButton**: Botão dourado para aposta máxima (à direita do Spin)
- Iluminação direcional configurada

### 2. Bonus.unity
**Cena exclusiva do modo Bonus (Hold & Win)**
- Camera com background laranja/dourado (tema quente)
- Canvas UI dedicado com:
  - **BonusTitleText**: Título "BONUS HOLD & WIN" em dourado
  - **CollectedText**: Display do total coletado no bonus
  - Grade 5x3 preparada para moedas (implementação visual adicional necessária)
- Ambiente otimizado para transição dramática do modo normal

### 3. FreeSpins.unity
**Cena exclusiva de Free Spins**
- Camera com background azul brilhante (tema frio/místico)
- Canvas UI com:
  - **FreeSpinsTitleText**: Título "FREE SPINS" em ciano
  - **SpinsLeftText**: Contador de spins restantes
  - **MultiplierText**: Display do multiplicador progressivo (x1, x2, etc.)
- Iluminação ambiente diferenciada para destacar o modo especial

## Prefab Bootstrap

**Bootstrap.prefab** - Prefab reutilizável do sistema core
- Contém componente Bootstrap.cs
- Auto-inicializa todos os managers (RNG, Symbols, Paylines, UI, Bonus, etc.)
- Cria automaticamente 5 ReelControllers
- Arraste para qualquer cena para setup instantâneo

## Como Importar

### Método 1: Importação Direta
1. Copie a pasta `Unity/Assets/` para dentro da pasta Assets do seu projeto Unity
2. Unity detectará automaticamente as cenas
3. Abra `Assets/Scenes/Main.unity`
4. Clique Play

### Método 2: UnityPackage (Recomendado)
1. No Unity, vá em `Assets > Import Package > Custom Package`
2. Selecione o arquivo `.unitypackage` (se fornecido)
3. Marque todos os itens e clique Import
4. Abra a cena Main

## Conectando Scripts aos Componentes

**IMPORTANTE**: Os GUIDs dos scripts nos arquivos .unity são placeholders. Você precisará:

1. Abrir cada cena
2. Selecionar o GameObject Bootstrap
3. No Inspector, reconectar o script Bootstrap.cs se necessário
4. Para os botões UI:
   - Selecionar cada botão
   - No componente Button, seção OnClick()
   - Arrastar o GameObject Bootstrap
   - Selecionar função `SpinManager.Spin()` ou equivalente

## Customização de UI

### Substituindo Cores/Sprites
- Todos os botões usam sprites padrão do Unity (10905)
- Para usar sprites customizados:
  1. Importe suas imagens para `Assets/Art/`
  2. Configure como Sprite (2D and UI)
  3. No Inspector de cada botão, substitua o campo `Source Image`

### Adicionando Texto aos Botões
Para adicionar labels nos botões:
```
1. Selecione o botão (ex: SpinButton)
2. Clique direito > UI > Text
3. Configure o texto, fonte, cor
4. Centralize usando Anchors (Center-Middle)
```

## Configurações Recomendadas

### Build Settings
- **WebGL**: Use Compression Format: Brotli
- **Android**: IL2CPP, ARM64
- **Windows**: x86_64

### Quality Settings
- Ajuste Anti-Aliasing para 4x MSAA em desktop
- Mobile: 2x MSAA ou desabilitado
- VSync: Ativado para evitar tearing

### Player Settings
- **Company Name**: Seu nome/studio
- **Product Name**: Nome do jogo
- **Default Icon**: Seu ícone
- **Splash Screen**: Configure ou desabilite

## Próximos Passos

1. **Arte**: Substitua placeholders por assets finais em `Assets/Art/`
2. **Audio**: Adicione AudioClips em `Assets/Audio/` e conecte ao SoundManager
3. **Animações**: Crie AnimationControllers para símbolos e transições
4. **Partículas**: Adicione ParticleSystems para wins e bonus triggers
5. **DevPanel**: Configure a UI do DevPanel para testes (DevPanel.cs)

## Testando

1. Abra Main.unity
2. Clique Play
3. No Console, você verá logs do Bootstrap
4. Clique no botão Spin ou chame `FindObjectOfType<SpinManager>().Spin()` via script de teste
5. Observe os logs de ganhos, bônus e free spins

## Resolução de Problemas

**"Missing Script"**: 
- Os scripts não foram compilados. Aguarde a compilação ou reimporte

**"NullReferenceException"**:
- Verifique se todos os managers foram inicializados pelo Bootstrap
- Confirme que Bootstrap está na cena

**UI não aparece**:
- Confirme que a Camera está taggeada como "MainCamera"
- Canvas deve estar em modo Screen Space - Overlay

**Botões não respondem**:
- Verifique se há um EventSystem na cena (adicione via GameObject > UI > Event System)
- Confirme que Canvas tem componente GraphicRaycaster

## Otimizações Adicionais

- Use **Sprite Atlas** para agrupar símbolos e UI
- Ative **Batching Estático** em elementos fixos
- Configure **LOD Groups** para efeitos complexos
- Implemente **Object Pooling** via PoolManager.cs
- Profile com **Unity Profiler** antes do build final

## Builds de Produção

### WebGL
```bash
# Build Settings
Target Platform: WebGL
Compression: Brotli
Memory Size: 2048 MB (ajuste conforme necessário)
Enable Exceptions: None
Strip Engine Code: Yes
```

### Android
```bash
# Build Settings
Target: Android
Architecture: ARM64
Scripting Backend: IL2CPP
API Level: 30+
Texture Compression: ASTC
```

### Windows
```bash
# Build Settings
Target: PC, Mac & Linux Standalone
Architecture: x86_64
Configuration: Release
```

---

**Nota**: Este é um framework funcional. Arte, áudio e animações devem ser adicionados conforme seu projeto visual. Consulte o GDD.md para especificações completas de gameplay.
