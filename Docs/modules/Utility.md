# Utility

`SombraStudios.Shared.Utility` — the largest module (108 scripts). Small,
independent helpers with no dependency on each other: timers, lerp helpers,
coroutine hosting, gizmo drawing, loggers, and a long tail of single-purpose
components.

Nothing here is a framework. Every entry is meant to be taken on its own.

## Layout

| Subfolder | What's in it |
|---|---|
| `Timers/Core/` | POCO timers — `Timer` base plus countdown, stopwatch, interval, frequency |
| `Timers/Unity/` | MonoBehaviour hosts that tick the POCO timers |
| `Timers/OLD/` | **Deprecated.** UI-coupled MonoBehaviour timer. Use `Timers/Core/` |
| `Lerp/` | `LerpTool` plus per-type data structs (float, Vector2/3, Color, Quaternion) |
| `Coroutines/` | `CoroutineRunner` / `CoroutineManager` — run coroutines without owning a GameObject |
| `Cooldown/` | `Cooldown` + `ICooldown`, waiting periods between actions |
| `UnityGizmos/` | `GizmosUtility` base + one component per shape (cube, sphere, line, ray, mesh, grid, frustum, FOV cone, icon) |
| `UnityMessages/` | 18 handler components, one per Unity callback group, each documenting its execution order |
| `Loggers/` | `Logger`, `ILoggerService`, `MonoBehaviourLogger` |
| `Destroyer/` | Strip components or GameObjects on Awake, by platform or release build |
| `DontDestroy/` | Add to / remove from `DontDestroyOnLoad` |
| `Mirror/` | Mirror a transform's position, rotation, or scale across an axis |
| `Sprites/` | Random sprite assembly, sorting-order control |
| `NullReferenceChecker/` | Validate required serialized fields; `[Optional]` opts a field out |
| `PrefabInstantiateOnLoad/` | Spawn prefabs from `Resources` at load, driven by a ScriptableObject |
| `ReferenceBinder/`, `Resources/`, `TimeScale/`, `Mobile/` | One or two helpers each |
| *(module root)* | Camera-visibility checks, screen bounds, encoding, Unix timestamps, debug panel, `Direction` enum |

## Start here

| Type | Path | Use it when |
|---|---|---|
| `CountdownTimer` | `Timers/Core/CountdownTimer.cs` | You need a timer that is testable and has no scene presence |
| `LerpTool` | `Lerp/LerpTool.cs` | Interpolating a value, transform, or color over time |
| `CoroutineRunner` | `Coroutines/CoroutineRunner.cs` | Something that isn't a MonoBehaviour needs to run a coroutine |
| `GizmosUtility` | `UnityGizmos/Monobehaviours/GizmosUtility.cs` | You want editor-visible debug shapes without writing `OnDrawGizmos` |
| `NullReferenceChecker` | `NullReferenceChecker/NullReferenceChecker.cs` | Catching unassigned Inspector fields before Play mode |

## Drop-in: a countdown timer

`Timer` is a POCO, so nothing ticks it for you — that is the point (it is
testable without the Editor, per architecture rule 1). Whatever owns it ticks
it:

```csharp
private CountdownTimer _reload;

private void Awake()
{
    _reload = new CountdownTimer(2f);
    _reload.OnTimerStop += Reloaded;
}

private void OnDestroy()
{
    _reload.OnTimerStop -= Reloaded;   // POCOs don't unsubscribe themselves
}

private void Update()
{
    _reload.Tick();
    if (_reload.IsFinished) { /* ... */ }
}
```

`Timer` exposes `Start`, `Stop`, `Pause`, `Resume`, `Reset`, `CurrentTime`,
`IsRunning`, `Progress`, and the `OnTimerStart` / `OnTimerStop` actions.
`CountdownTimer` adds `RemainingTimePercentage`, `ElapsedTime`, and
`ElapsedTimePercentage`. Use `Timers/Unity/` if you would rather a
MonoBehaviour did the ticking.

## Gotchas

- **`Timers/OLD/` is superseded.** It declares a second type called `Timer`, in
  `…Utility.Timers.MonobehaviourTimer` rather than `…Utility.Timers.Core` — no
  compile collision, but `using` both namespaces makes `Timer` ambiguous.
  Reach for `Timers/Core/`.
- **`UnityGizmos/Editor/` is a separate assembly**
  (`SombraStudios.Shared.Utility.Editor`). Runtime code must work with it
  deleted.
- **`UnityMessages/` handlers are reference material**, not something to ship —
  each one implements a group of Unity callbacks with its execution order
  documented. Read them to check ordering; don't attach them to a live object.
- Several root-level helpers are `MonoBehaviour` only because they were written
  that way, not because they need a GameObject (`StringEncodingUtil`,
  `UnixTimestampConverter`, `HexToRGBA`). Check before assuming you need to
  attach one.

## Gates

Nothing in this module is gated behind an optional package except
`UnityGizmos/Editor/`, which is `UNITY_EDITOR` by virtue of being an Editor
assembly. Grep `INDEX.tsv` for the current picture:

```bash
awk -F'\t' '$5 ~ /^Utility\// && $6 != "-"' INDEX.tsv
```
