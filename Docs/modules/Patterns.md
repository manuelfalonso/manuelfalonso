# Patterns

`SombraStudios.Shared.Patterns` — 92 scripts. Design-pattern implementations
grouped as Behavioural / Creational / InversionOfControl. Several patterns ship
in **more than one flavour**; picking the right flavour matters more than
picking the right pattern, so that is what this page is mostly about.

## Layout

```
Behavioural/
  Command/                    ICommand, CommandSO, CommandManager
  FiniteStateMachine/         StateMachine + IState/ITransition/ICondition
  StackStateMachine/          struct-based, push/pop states
  Observer/
    EventBus/                 static, type-keyed, code-only
    StaticRegistry/           TapestryEvent + TapestryEventRegistry
    ScriptableObjects/        event channels + Inspector-wired listeners
  Strategy/                   IStrategy, IStrategy<T>
  Visitor/                    IVisitor/IVisitable + VisitorSO variants
Creational/
  Singleton/                  Singleton, PersistentSingleton, RegulatorSingleton
  ObjectPool/                 BaseObjectPool<T>
  FlyweightFactoryPool/       pooled flyweights driven by a settings asset
  FactoryMethod/              Factory + IProduct
  LazyInitialization/         LazyReference<T>, LazyService<T>
InversionOfControl/
  ServiceLocator/             scene- and global-scoped service lookup
  DependencyInjection/        attribute-driven field injection + Example/
```

## Choosing an observer flavour

Three implementations, three different trade-offs. This is the most common
wrong turn in the module:

| Flavour | Wiring | Reach for it when |
|---|---|---|
| `EventBus/` | `EventBus<T>.Register` / `.Raise` in code | Pure-code decoupling, one event type per struct. No assets, no Inspector. Static, so it lives across scenes — unregister. |
| `Observer/ScriptableObjects/` | Designer drags an `*EventChannelSO` asset onto a listener | A designer needs to wire the connection without code. Costs one asset per event. |
| `StaticRegistry/` | `TapestryEventRegistry` static fields | You want named, discoverable events with 0–5 typed parameters and no per-event asset. |

Event channels come pre-typed for `Bool`, `Float`, `Int`, `String`,
`Vector2`, `Vector3`, `GameObject`, `SO`, and `Void`; derive
`GenericEventChannelSO<T>` for anything else. Listeners come in single
(`GenericEventChannelListener<T>`) and multi (`…ListListener<T>`) forms.

## Choosing a state machine

- **`FiniteStateMachine/`** — `StateMachine` is a POCO with `Update`,
  `FixedUpdate`, `SetState`, `AddTransition`, `AddAnyTransition`. States
  implement `IState` (or derive `BaseState`); transitions are guarded by
  `ICondition`, with `FuncCondition` for a lambda. `StateMachineClient` is the
  MonoBehaviour adapter. **Start here.**
- **`StackStateMachine/`** — a `struct` machine with push/pop semantics, for
  states that nest (paused-over-playing, submenu-over-menu).

```csharp
private StateMachine _states;

private void Awake()
{
    var idle = new IdleState();
    var chase = new ChaseState();

    _states = new StateMachine();
    _states.AddTransition(idle, chase, new FuncCondition(() => _target != null));
    _states.AddAnyTransition(idle, new FuncCondition(() => _health <= 0f));
    _states.SetState(idle);
}

private void Update() => _states.Update();
```

## Choosing between ServiceLocator and DependencyInjection

Both solve "how does this object find its collaborators", and they do not mix
well — pick one per project.

- **`ServiceLocator/`** — `ServiceLocator.For(this)`, `.ForSceneOf(this)`, or
  the global instance; `Register<T>` / `Get<T>` / `TryGet<T>`. Bootstrap it
  with `ServiceLocatorGlobal` or `ServiceLocatorScene`. Fewer moving parts, and
  the fastest to stand up in a prototype.
- **`DependencyInjection/`** — mark fields with `[InjectField]`, put a
  `DependenciesContext` in the scene, and `DependenciesProvider` fills them in.
  Works on plain classes as well as MonoBehaviours. See
  `DependencyInjection/Example/` for a working scene setup.

Note both are service *lookup*, not constructor injection. Architecture rule 4
still applies: pass config in, don't reach for a static.

## Singletons

Three variants, all `MonoBehaviour`-based:

| Type | Behaviour |
|---|---|
| `Singleton<T>` | Per-scene; destroyed on load |
| `PersistentSingleton<T>` | Survives scene loads (`DontDestroyOnLoad`) |
| `RegulatorSingleton<T>` | Persistent, and destroys older instances of itself |

These are the exception to "no mutable statics", not a licence. Prefer
`ServiceLocator` or an injected reference where a prototype allows it.

## Gotchas

- **`Observer/ScriptableObjects/Editor/`** is a separate assembly. The runtime
  channels and listeners must work with it deleted.
- **`EventBus<T>` is static.** Bindings survive Play-mode exits when domain
  reload is disabled; whatever registers must unregister. `EventBusUtil` and
  `PredefinedAssemblyUtil` handle the type discovery.
- **`CommandSO` / `VisitorSO` / `StrategySO` are the asset-authored flavours**
  of their patterns. Per architecture rule 4 they are wrappers — a plain
  `ICommand` / `IStrategy` implementation is the default, and the SO exists for
  when a designer authors variants.
- `Examples/Patterns/` (in the `Examples` module, not here) holds runnable
  demonstrations and is free to delete.

## Gates

```bash
awk -F'\t' '$5 ~ /^Patterns\// && $6 != "-"' INDEX.tsv
```
