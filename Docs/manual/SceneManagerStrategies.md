# Scene Management via ScriptableObject Strategies

- **Decoupled Scene Loading Logic**: This system replaces tightly coupled scene-loading code with flexible ScriptableObject strategies, enabling reusable and testable scene operations.
- **Strategy Pattern Architecture**: Each scene-related behavior (load, unload, reload, set active, etc.) is encapsulated within a dedicated `SceneStrategySO`, following the Strategy pattern. These behaviors are pluggable and composable at runtime.
- **Support for Multi-Strategy Composition**: Complex loading sequences can be created by composing multiple strategies via `MultiSceneStrategySO`, allowing for chainable and layered scene logic (e.g., load multiple scenes additively, then set one active).
- **Asset-Driven Design for Reusability**: Scene strategies are defined as assets, allowing designers or developers to reuse and reference them directly in other systems like gameplay logic, UI buttons, or startup sequences without code changes.
- **Lifecycle Awareness and Delayed Execution**: Strategies support integration with Unity’s coroutine system for asynchronous loading, and can be configured to delay, wait for conditions, or execute in order.

## When to Use It

- In large or modular projects where scene loading behaviors need to be reused across multiple contexts (e.g., level management, transitions, UI-driven flows).
- When you want to enable designers to configure and tweak scene logic without code intervention.
- For tools, prototypes, or frameworks that require flexible, data-driven scene control.

## When Not to Use It

- For simple or one-off scene transitions where the overhead of asset creation and modular logic isn’t justified.
- If you need tight control over scene loading lifecycles in performance-critical code, where monolithic logic might be more efficient.