# Strategy Pattern Implementation with ScriptableObjects

- **Generic Strategy Interface (`IStrategy<T>`)**: A reusable, type-safe interface that defines an `Execute(T context)` method, enabling flexible behavior definition across various systems.
- **ScriptableObject-Based Strategies**: Each strategy is implemented as a `ScriptableObject`, allowing you to create, reuse, and edit behaviors directly in the Unity Editor without modifying code.
- **`MultiStrategy` Composition**: Compose multiple strategies into a single `MultiStrategySO`, which executes each sub-strategy sequentially, enabling modular and scalable behavior trees or action stacks.
- **Plug-and-Play Architecture**: Integrate new behaviors at runtime or in the editor simply by creating new `StrategySO` assets and assigning them to MonoBehaviours or systems that consume `IStrategy<T>`.

## When to Use It

- When designing AI, abilities, event responses, or any modular behavior that benefits from separation of concerns and reuse.
- When you want to empower designers or non-programmers to configure behaviors in the Unity Editor without writing code.
- In projects that require easily testable and swappable logic components following the open/closed principle.

## When Not to Use It

- For performance-critical operations where `ScriptableObject` indirection might introduce unnecessary overhead.
- When behavior logic is tightly coupled to runtime data or state that cannot be serialized or injected into a stateless strategy context.