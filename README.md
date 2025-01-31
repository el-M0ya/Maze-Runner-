# JailBreak

¡Bienvenido al repositorio del proyecto **JailBreak**! Este es un juego desarrollado en Unity utilizando C#. En este juego, tu objetivo es explorar un laberinto, encontrar una llave y escapar a través de la puerta antes de que se agote el tiempo. A continuación, encontrarás las instrucciones para configurar, jugar y entender la arquitectura del proyecto.

---

## Instrucciones de Uso y Desarrollo

### Requisitos Previos
Para ejecutar y desarrollar este proyecto, necesitarás lo siguiente:
- **Unity Hub**: Para gestionar las versiones de Unity.
- **Unity 2022 o superior**: El proyecto está desarrollado en Unity versión 2022 o superior. Asegúrate de tener esta versión instalada a través de Unity Hub.

### Configuración del Proyecto
1. **Descarga los archivos**: Clona este repositorio o descarga los archivos como un archivo ZIP.
2. **Crea una carpeta**: Coloca los archivos descargados en una carpeta en tu computadora.
3. **Abre el proyecto en Unity**:
   - Abre Unity Hub.
   - Selecciona la opción "Add" (Añadir).
   - Navega hasta la carpeta donde colocaste los archivos del proyecto y selecciónala.
   - Unity Hub abrirá el proyecto en la versión de Unity correspondiente.

### Ejecutar el Proyecto
1. **Abre la escena principal**: En el panel "Project" (Proyecto), navega hasta la carpeta `Scenes` y haz doble clic en la escena principal (por ejemplo, `MainScene.unity`).
2. **Ejecuta el juego**: Presiona el botón "Play" (Reproducir) en la parte superior de la ventana de Unity para iniciar el juego.

---

## Manual de Usuario

### Cómo Jugar
1. **Iniciar el juego**:
   - Al abrir el juego, haz clic en el botón **Start**.
   - Selecciona las dimensiones del laberinto (por ejemplo, pequeño, mediano o grande).
   - Elige tu personaje entre las opciones disponibles.

2. **Controles**:
   - Usa las **flechas direccionales** o el clásico **WASD** para moverte por el laberinto.
   - Para usar la habilidad especial, espera a que el **cooldown** llegue a 0 y haz clic en el botón en la esquina inferior derecha.

3. **Objetivo**:
   - Encuentra la **llave** que está escondida en el laberinto.
   - Una vez que tengas la llave, busca la **puerta** para escapar.
   - ¡Haz todo esto antes de que se agote el tiempo!

---

## Explicación de la Arquitectura del Código

El proyecto está estructurado de la siguiente manera:

### 1. **Scripts Principales**
- **TurnManager.cs**: Gestiona los turnos y la lógica del flujo del juego, como el cambio entre personajes o fases del juego.
- **Character.cs**: Controla el comportamiento del personaje, incluyendo movimiento, habilidades y interacciones con objetos.
- **Metodos.cs**: Contiene métodos auxiliares y funciones reutilizables que son utilizadas por otros scripts.
- **UI.cs**: Maneja la interfaz de usuario, como los botones, el temporizador, el cooldown de la habilidad y la visualización de información importante.

### 2. **Estructura de Carpetas**
- **Assets/Scenes**: Contiene las escenas del juego, incluyendo la escena principal (`PrincipalScene.unity`).
- **Assets/Scripts**: Aquí se encuentran todos los scripts de C# que controlan la lógica del juego.
- **Assets/Sprites**: Aloja los sprites del juego, como los personajes, la llave, la puerta y otros elementos visuales.
- **Assets/Sounds**: Contiene los sonidos del juego, como efectos de sonido y música de fondo.

### 3. **Flujo del Juego**
1. El jugador selecciona las dimensiones del laberinto y el personaje.
2. El laberinto se genera dinámicamente usando el script `Metodos.cs`.
3. El jugador se mueve por el laberinto usando `Character.cs`.
4. El `TurnManager.cs` verifica si el jugador ha encontrado la llave y ha llegado a la puerta antes de que se agote el tiempo.

### 4. **Extensibilidad**
El código está diseñado para ser modular y fácil de extender. Por ejemplo:
- Para agregar nuevos personajes, debes crear nuevos prefabs que contengan el script `Character`, además de un `Sprite Renderer` y un `Animator`.
- Puedes cambiar la lógica de generación del laberinto editando `Metodos.cs`.
- Puedes añadir nuevas habilidades o mecánicas al jugador.

---

## Licencia

Este proyecto está bajo la licencia **MIT**. Para más detalles, consulta el archivo [LICENSE](LICENSE).

---

