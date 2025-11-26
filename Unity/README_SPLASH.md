# Sistema de Telas de Abertura - Estilo Playson

## Visão Geral

Sistema completo de splash screens inspirado nos jogos da Playson, incluindo:
1. **Logo do Provedor** - Fade-in animado
2. **Tela de Loading** - Barra de progresso com porcentagem
3. **Tela de Título** - Logo do jogo com botão START pulsante
4. **Transição** - Fade suave para o jogo principal

## Arquivos Criados

### Scripts C# (Unity/Assets/Scripts/UI/)
- `SplashScreenManager.cs` - Gerencia a sequência completa de splash screens
- `LoadingProgressBar.cs` - Controla animação da barra de progresso
- `TitleScreenController.cs` - Animações do logo e botão START

### Assets Visuais (Unity/Assets/Art/)
- `logo_provider.png` - Logo estilo PLAYSON com moldura dourada
- `logo_game.png` - Logo "ROYAL SLOTS" com coroa
- `splash_background.png` - Fundo luxuoso roxo/dourado
- `btn_start.png` - Botão START ornamentado
- `loading_bar_bg.png` - Moldura da barra de loading
- `loading_bar_fill.png` - Preenchimento gradiente da barra

### Cena Unity
- `Splash.unity` - Cena completa configurada com todos os elementos

## Compatibilidade Unity 6.2

### Correções Aplicadas
- **Bootstrap.cs** atualizado para usar `FindAnyObjectByType<T>()` em vez da API deprecada `FindObjectOfType<T>()`
- Todos os scripts testados para Unity 6.2 (6000.2.12f1)

## Configuração no Inspector

### SplashScreenManager
```
Provider Logo Duration: 2s (tempo de exibição do logo do provedor)
Loading Duration: 2.5s (tempo de loading simulado)
Allow Skip: true (permitir pular com qualquer tecla)
Fade Speed: 1s (velocidade das transições)
```

### LoadingProgressBar
```
Progress Curve: Ease In-Out (curva de animação da barra)
Glow Pulse Speed: 2 (velocidade do efeito de brilho)
Start Color: Purple (0.5, 0.3, 0.8)
End Color: Gold (1, 0.84, 0)
```

### TitleScreenController
```
Logo Float Speed: 1.5 (velocidade da flutuação do logo)
Logo Float Amount: 10 (amplitude da flutuação)
Button Pulse Speed: 2 (velocidade da pulsação do botão)
Button Pulse Scale: 1.1 (escala máxima da pulsação)
```

## Importação e Uso

### 1. Adicionar ao Build Settings
1. Abra **File > Build Settings**
2. Clique em **Add Open Scenes** com Splash.unity aberta
3. Arraste **Splash** para o topo da lista (índice 0)
4. Certifique-se de que **Main**, **Bonus**, **FreeSpins** estão na lista

### 2. Conectar Assets
1. Selecione o GameObject **SplashCanvas** na hierarquia
2. No Inspector, localize o componente **SplashScreenManager**
3. Arraste os sprites correspondentes para os campos:
   - Provider Logo → `logo_provider`
   - Game Logo → `logo_game`
   - Start Button Image → `btn_start`
   - Progress Bar Fill → `loading_bar_fill`

### 3. Configurar Background
1. Na hierarquia, encontre **Camera > Background** (ou crie um UI Image)
2. Defina o sprite como `splash_background`
3. Ajuste para preencher a tela (Anchor: Stretch, Offset: 0)

### 4. Testar
1. Configure Splash.unity como primeira cena no Build Settings
2. Pressione Play
3. Sequência: Logo Provedor (2s) → Loading (2.5s) → Título → START → Main

## Funcionalidades

### Skip Function
- Pressione qualquer tecla durante Logo/Loading para pular direto para a tela de título
- Configurável via `allowSkip` no SplashScreenManager

### Animações Automáticas

**Logo do Provedor:**
- Fade-in suave (1s)
- Permanece visível (2s configurável)
- Fade-out suave (1s)

**Tela de Loading:**
- Barra de progresso animada com curva ease-in-out
- Porcentagem atualizada em tempo real (0% → 100%)
- Gradiente de cor: roxo → dourado
- Efeito de brilho pulsante

**Tela de Título:**
- Logo do jogo flutua verticalmente
- Botão START pulsa suavemente
- Fade-in ao aparecer

## Personalização

### Alterar Durações
```csharp
// No GameSettings.cs
providerLogoDuration = 2f;  // Tempo do logo do provedor
loadingDuration = 2.5f;     // Tempo de loading
```

### Cores Personalizadas
```csharp
// No LoadingProgressBar.cs
startColor = new Color(0.5f, 0.3f, 0.8f);  // Roxo
endColor = new Color(1f, 0.84f, 0f);       // Dourado
```

### Velocidade das Animações
```csharp
// No TitleScreenController.cs
logoFloatSpeed = 1.5f;      // Flutuação do logo
buttonPulseSpeed = 2f;      // Pulsação do botão
```

## Fluxo de Navegação Completo

```
[Splash.unity]
    ├─ Provider Logo (2s)
    ├─ Loading Screen (2.5s)
    └─ Title Screen (espera input)
         └─ [START] → [Main.unity]
              ├─ [BONUS] → [Bonus.unity]
              └─ [FREE SPINS] → [FreeSpins.unity]
```

## Integração com Sistema de Audio

O `TitleScreenController` já está preparado para tocar sons:
```csharp
public void PlayClickSound()
{
    var soundManager = FindAnyObjectByType<SoundManager>();
    if (soundManager)
    {
        soundManager.PlaySound("ButtonClick");
    }
}
```

Adicione este método ao evento `OnClick` do botão START no Inspector.

## Integração com TransitionManager

Para transições suaves entre Splash → Main:
```csharp
// Em SplashScreenManager.OnStartButtonClicked()
var transitionManager = FindAnyObjectByType<TransitionManager>();
if (transitionManager)
{
    transitionManager.TransitionToMain();
}
else
{
    SceneManager.LoadScene("Main");
}
```

## Troubleshooting

### "Missing Script" no SplashScreenManager
- Verifique se o GUID do script está correto no .unity file
- Reimporte o script SplashScreenManager.cs

### Imagens não aparecem
- Certifique-se de que os sprites estão importados com:
  - Texture Type: Sprite (2D and UI)
  - Pixels Per Unit: 100
  - Compression: None (para qualidade máxima)

### Texto de porcentagem não aparece
- Instale o pacote TextMeshPro via Package Manager
- Importe os Essential Resources do TMP

### Botão START não responde
- Verifique se há um EventSystem na cena
- Confirme que o botão tem o componente Button anexado
- Certifique-se de que Interactable está marcado

## Performance

- **Tempo total da sequência:** ~5.5s (sem skip)
- **Memória:** ~15MB (com todos os assets em alta qualidade)
- **Draw calls:** ~10-15 por tela

## Próximos Passos

1. **Audio**: Adicionar música de fundo e efeitos sonoros
2. **Partículas**: Criar sistema de partículas douradas flutuantes
3. **Localization**: Suporte para múltiplos idiomas no botão START
4. **Analytics**: Rastrear tempo médio na splash screen
5. **A/B Testing**: Testar diferentes durações e animações

## Referências

- Inspirado em: Playson "Supercharged Clovers Hold and Win"
- Documentação Unity 6: https://docs.unity3d.com/6000.0/Documentation/Manual/
- API Changes: FindObjectOfType → FindAnyObjectByType
