# Projeto Unity – Slots 5x3 (Entrega para Importação)

Este pacote contém scripts C#, JSON de demonstração e GDD para você importar no seu projeto Unity.

Estrutura:
- Assets/
  - Scripts/
    - Config: GameSettings.cs
    - Services: RNGService.cs
    - Slots: SymbolManager.cs, PaylinesManager.cs, ReelController.cs, SpinManager.cs
    - Bonus: BonusPotController.cs, FreeSpinsController.cs
    - UI: UIManager.cs
    - Audio: SoundManager.cs
    - Animation: AnimationManager.cs
    - Core: PoolManager.cs, SaveLoadManager.cs, Bootstrap.cs
    - Dev: DemoDataLoader.cs, DevPanel.cs
  - Resources/
    - DemoSamples/sample_demo.json
- Docs/GDD.md

Como usar (5 minutos):
1) Arraste a pasta Unity deste repositório para a pasta Assets do seu projeto Unity.
2) Crie uma cena vazia (Main). Adicione um GameObject vazio e anexe o componente Bootstrap.
3) Dê Play. No Console você verá logs e pode chamar Spin manualmente:
   - Se desejar, crie um botão UI que chame SpinManager.Spin() (encaminhe a referência via Inspector) ou adicione um script temporário para chamar automaticamente no Start.
4) Ajuste no Inspector: GameSettings (RTP, Linhas, Volatilidade) e UIManager (saldo/aposta).
5) Para Demo JSON por URL, defina no GameObject DemoDataLoader o campo urlOrEmpty.

Observações:
- As animações/UI/SFX são placeholders. Substitua por seus assets reais e conecte nos managers.
- Para cenas e prefabs prontos, basta criar GameObjects e vincular componentes conforme sua pipeline.
- O GDD está em Docs/GDD.md. Exporte para PDF se necessário.

Builds:
- WebGL/Android/Windows: use as targets padrão da Unity. Garanta compressão de texturas para mobile.
