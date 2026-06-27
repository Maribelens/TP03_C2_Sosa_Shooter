# TP03_C2_Sosa_Shooter
🕹️ Shooter — Trabajo Práctico 03
🎯 Descripción del juego
Shooter es un videojuego 3D en primera persona donde el jugador se infiltra en una zona enemiga altamente vigilada.
El objetivo es eliminar a todos los enemigos del mapa antes de que se acabe el tiempo, manteniéndose con vida.
Si el jugador muere o el tiempo llega a cero sin cumplir el objetivo, el juego termina en derrota.
Si logra eliminar a todos los enemigos antes del límite y sigue vivo, obtiene la victoria.

🧩 Gameloop
El jugador inicia la partida con tiempo limitado.
Si el jugador muere → derrota.
Si se acaba el tiempo → derrota.

Si todos los enemigos mueren y el jugador sigue vivo → victoria.

🧠 Arquitectura
Core: GameManager, GameConditionChecker, CustomSceneManager, SplashScreenManager

Gameplay: GameTimer, EnemyCounter, WeaponController, Bullet, ExplosionParticle

Player: HealthSystem, CameraRegister, IDamageable

UI: UIResultScreen, UIPause, LoadingBar, MainMenu

Enemies: scripts de comportamiento y registro de enemigos.


🧑‍💻 Créditos
Desarrollo: Mariana

Assets de terceros: modelos, sonidos y partículas del Unity Asset Store y fuentes libres.
Unity Asset Store: Assets usados
https://assetstore.unity.com/packages/2d/textures-materials/sky/colorskies-91541
https://assetstore.unity.com/packages/3d/props/industrial/tower-the-last-70663
https://assetstore.unity.com/packages/3d/props/grenades-lowpoly-120047
https://assetstore.unity.com/packages/3d/characters/robots/drone-guard-175607
https://assetstore.unity.com/packages/3d/environments/free-pack-rocks-stylized-316250

- Humanoid models and animations: Mixamo (Adobe)

🕹️ Cómo jugar
Movimiento: WASD
Correr: Shift
Saltar: Espacio
Agacharse: C
Disparar: Click izquierdo
Pausar: P

Objetivo: Eliminar todos los enemigos antes de que se acabe el tiempo.

🌐 Publicación
Build WebGL: https://mb-lens.itch.io/shooter
