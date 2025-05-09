# Extensible ScriptableObject Observer Pattern (Generic Event System)

- **ScriptableObject-Based Event Dispatching**: A highly reusable observer pattern using `ScriptableObject` assets as event channels. Events are defined as generic base classes, supporting payloads or parameterless types.
- **Generic and Extensible**: The system is fully generic, enabling you to create type-safe event assets with payloads (e.g., `GameEvent<T>`) or without (e.g., `VoidEvent`). This makes it easy to scale across multiple event types without duplicating logic.
- **Runtime Listener Support**: Includes MonoBehaviour-based `EventListener<T>` and `VoidEventListener` scripts that register/unregister with the corresponding `ScriptableObject` events automatically during the object’s lifecycle.
- **Editor Tooling Included**: Custom editor drawers and inspectors show live subscribers at runtime, allowing quick debugging and better insight into event flow across your project.

## When to Use It

- For decoupling game systems like UI, audio, input, and gameplay logic using event-driven architecture.
- In projects where designers or non-programmers need to connect logic via the Inspector without tight coupling.
- When you want a centralized, editor-visible, and testable alternative to UnityEvents or C# delegates.

## When Not to Use It

- If your project doesn't benefit from decoupling or is too small to justify the abstraction.
- In performance-critical inner loops where event overhead could introduce latency (e.g., physics step logic).