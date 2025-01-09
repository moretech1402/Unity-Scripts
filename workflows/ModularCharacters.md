# Flujo de trabajo para exportar personajes de Blender a Unity (con Rigging)

Este documento describe el proceso para exportar personajes animados desde Blender a Unity, asegurando la correcta escala, orientación y funcionamiento del rig.

**I. Creación del modelo en Blender:**

Recomiendo usar un mismo archivo para todas las partes del juego.
También he comprobado que puedes descargar animaciones de [mixamo](mixamo.com) sin la skin y luego importarlas en Unity. Funcionan perfectamente (al menos con avatares humanoides)

1.  **Escala del modelo:**
    *   Blender utiliza unidades que, por defecto, no corresponden directamente a metros. Para que 1 unidad de Blender equivalga a 1 metro en Unity (y en el mundo real), configura las unidades en Blender:
        *   Ve a *Properties > Scene Properties > Units*.
        *   Asegúrate de que *Unit System* esté en *Metric* y *Unit Scale* esté en **1**.
    *   Un humano promedio mide alrededor de 1.75 metros. Utiliza esta referencia al modelar.
    *   Crea un cubo de 1x1x1 unidades como referencia visual de 1 metro.

2.  **Modelado y separación de partes:**
    *   Modela tu personaje en Blender.
    *   Si el personaje tiene partes separadas (cabeza, brazos, etc.), modélalas por separado y luego únelas temporalmente para el rigging. *Después del rigging, se separarán nuevamente usando instancias enlazadas.*

3.  **Creación y emparentamiento del Armature:**
    *   Crea el Armature (esqueleto) en Blender.
    *   Ajusta la posición y orientación de los huesos para que coincidan con la anatomía del personaje.
    *   Emparenta la malla al Armature usando *With Automatic Weights* (`Ctrl + P > With Automatic Weights`).
    *   Ajusta el *Weight Painting* (Pintado de Pesos) para una deformación suave y realista.

4.  **Animación (opcional):**
    *   Puedes crear las animaciones en Blender o importarlas por separado en Unity. Si animas en Blender, asegúrate de probar las animaciones antes de exportar.

**II. Exportación desde Blender (FBX):**

1.  **Selección:** En *Object Mode*, selecciona *primero* la malla y *luego* el Armature (manteniendo `Shift` presionado). El orden es importante.

2.  **Exportación:**
    *   Ve a *File > Export > FBX (.fbx)*.
    *   Configura las siguientes opciones en la ventana de exportación:
        *   **Path Mode:** "Copy" y activa el icono del cuadrado a la derecha (incrusta las texturas en el FBX).
        *   **Main > Transform:**
            *   **Scale:** **1** (NO modificar).
            *   **Apply Transform:** "All Local" (aplica las transformaciones localmente, crucial para la escala).
        *   **Geometry:**
            *   **Apply Modifiers:** Activado (aplica los modificadores a la malla).
            *   **Smoothing:** "Normals" (para un sombreado suave).
            *   **Triangulate:** Activado (Unity funciona mejor con mallas trianguladas).
        *   **Armature:**
            *   **Primary Bone Axis:** "-Y Axis".
            *   **Secondary Bone Axis:** "+Z Axis" (orientación correcta para Unity).
            *   **Add Leaf Bones:** Desactivado (evita complejidad innecesaria).
        *   **Bake Animation:** Activar si se exportan animaciones.
        *   **Forward:** -Y
        *   **Up:** Z
    *   Elige una ubicación y nombre para el archivo FBX (ej., "Personaje.fbx").
    *   Haz clic en "Export FBX".

**III. Importación en Unity:**

1.  **Copia el FBX al proyecto de Unity:** Copia el archivo FBX a la carpeta *Assets* de tu proyecto de Unity.

2.  **Configuración en el Inspector (archivo FBX seleccionado):**
    *   **Model:**
        *   **Scale Factor:** **1** (¡FUNDAMENTAL! No modificar este valor).
        *   **Mesh Compression:** "Medium" (buen compromiso entre calidad y rendimiento).
        *   **Read/Write Enabled:** Desactivado (a menos que necesites modificar los atributos de los huesos con scripts).
    *   **Rig:**
        *   **Animation Type:** "Humanoid" (para personajes humanoides) o "Generic" (para otros rigs).
        *   **Avatar Definition:** "Create From This Model". Haz clic en "Apply".
        *   **Configure:** Configura el Avatar mapeando los huesos a los huesos estándar de Unity. Haz clic en "Done".
    *   **Materials:** Configura los materiales si no se importaron correctamente.
    *   **Animations:** Configura las animaciones si se exportaron desde Blender.

**IV. Prueba del Rig en Unity:**

1.  **Arrastra el modelo a la escena.**
2.  **Añade un componente *Animator*.**
3.  **Crea un *Animator Controller* y asígnaselo al componente *Animator*.**
4.  **Crea un nuevo estado en el *Animator Controller*.**
5.  **Crea un nuevo *AnimationClip* (si no se importaron animaciones).**
6.  **Abre la ventana *Animation* y anima el modelo.**

**V. Integración de partes del cuerpo (si el modelo está dividido):**

1.  **Duplicar con instancia enlazada en Blender:**
    *   Selecciona el modelo base.
    *   `Shift + D` para duplicar.
    *   `Alt + D` para crear una instancia enlazada.

2.  **Separar las partes en *Edit Mode* (`P > Selection`).**

3.  **Ajustar posición y forma de las partes en *Object Mode*.**

4.  **Verificar y ajustar el *Weight Painting* en las zonas de unión.**

5.  **En Unity, mantener las partes como GameObjects hijos del mismo Armature.**

**Puntos clave para evitar problemas de escala:**

*   **Aplicar TODAS las transformaciones en Blender (`Ctrl + A > All Transforms`).**
*   **Configurar *Unit Scale* a 1 en Blender.**
*   **Configurar *Scale Factor* a 1 en Unity.**
*   **Revisar la escala del Armature en Edit Mode.**
*   **Escena limpia en Blender: verificar que no haya objetos con escalas extrañas.**
