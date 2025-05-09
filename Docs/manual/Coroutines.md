# Coroutine Management Utilities

- **Centralized Coroutine Handling via Static Class**: Provides a global, static entry point to start and stop coroutines without requiring a MonoBehaviour reference.
- **Persistent Coroutine Runner**: A hidden, automatically created `MonoBehaviour` instance is used internally to run coroutines safely from any context.
- **Safe Coroutine Lifecycle**: Ensures coroutines persist correctly through scene loads (if desired) and avoids common pitfalls with manually-managed runners or singletons.
- **Simplified API for Non-MonoBehaviour Scripts**: Greatly improves ergonomics when triggering coroutines from ScriptableObjects, static methods, or services without tight coupling to GameObjects.

## When to Use It

- When you want to decouple coroutine execution from scene objects or lifetime-bound MonoBehaviours.
- In utility libraries, service locators, or ScriptableObject systems where coroutine support is required.

## When Not to Use It

- In cases where you need more complex coroutine scheduling or time-slicing, such as a job system or task queue.