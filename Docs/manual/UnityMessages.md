# Unity Message Forwarding for Decoupled Callbacks

- **Unity Lifecycle Event Forwarders**: Contains MonoBehaviour scripts like `UnityInitializationMessagesHandler`, `UnityDecommissioningMessagesHandler`, `UnityOnMouseMessagesHandler`, `UnityPhysicsCollisionMessagesHandler`, and `UnityPhysicsTriggerMessagesHandler` that expose Unity’s internal lifecycle messages through public C# events.
- **UnityEvent-Based Architecture**: Each script defines one or more `UnityEvent` delegates that external components can subscribe to, enabling decoupled logic execution tied to specific Unity lifecycle events.
- **Minimal and Reusable**: Designed to be lightweight components that can be attached to any GameObject to broadcast Unity messages without subclassing MonoBehaviour.

## When to Use It

- When you want to connect Unity lifecycle events (like `Awake` or `Update`) to external systems without inheriting or modifying MonoBehaviour scripts.
- For rapid prototyping where you need quick lifecycle event hooks in modular components.
- To trigger effects, behaviors, or custom logic in a decoupled and testable way.

## When Not to Use It

- If your project architecture already favors centralized lifecycle handling (e.g., via managers or script execution order).
- When directly writing behavior inside MonoBehaviour methods is simpler and clearer.