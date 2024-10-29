# Prefab Instantiation Management with Scriptable Objects

- **Scriptable Object-Based Instantiation**: This system allows you to manage the instantiation of prefabs using ScriptableObjects, promoting clean, decoupled code.
- **`RuntimeInitializeOnLoadMethod` Integration**: Prefabs are instantiated at runtime using the `RuntimeInitializeOnLoadMethod` attribute, providing flexibility for initialization before or after scene loading.
- **Support for `RuntimeInitializeLoadType`**: Each instantiation can be configured to initialize at different points in the application lifecycle by managing the `RuntimeInitializeLoadType`, such as:
  - **BeforeSceneLoad**: Initialize prefabs before any scene is loaded.
  - **AfterSceneLoad**: Initialize prefabs after the first scene is loaded.
  - **AfterAssembliesLoaded**: Run instantiation after the assemblies are loaded.

## When to Use It

- When you need to automatically instantiate essential objects (e.g., managers, global controllers) during game loading or when entering Play Mode, ensuring consistency across all scenes.
- For large projects where you want to centralize and simplify scene setup by ensuring base objects are always present without manual intervention.

## When Not to Use It

- If the prefab needs to be instantiated on-demand based on user interactions (e.g., spawning enemies or items during gameplay).
- When prefabs need to be instantiated with specific runtime data that cannot be pre-configured in a ScriptableObject.