# Systems

`SombraStudios.Shared.Systems` — 62 scripts. Self-contained gameplay systems,
each in its own folder and independent of the others. Unlike `Gameplay/`, these
are *systems* with their own data model, not single-purpose components.

## Layout

| Subfolder | What it does |
|---|---|
| `Stats/` | Stat values with stacking modifiers |
| `Resource/` | Depletable/regenerating resources (health, mana, stamina) |
| `Events/` | Sequenceable, asset-authored event steps |
| `GameState/` | Generic game-state holder with listeners |
| `Objectives/` | Objective tracking with an event channel |
| `Teleport/` | Registry of spawn points and teleporters |
| `CloseCaptions/` | Subtitles driven by `AudioSource` playback |
| `LootBox/` | 2D/3D loot containers with item assets |
| `Damage/`, `Heal/` | `IDamageable` / `IHealable` + their data structs |
| `Unlock/` | Unlockable assets and a runtime set |
| *(module root)* | `AmmoReloadSystem` |

## Stats vs Resource — which one

They look similar and solve different problems:

- **`Stats/`** — a value that other things *modify*. `Stat<T, TModifier>` holds
  a base value plus a stack of `StatModifier<T>` (add, multiply, etc. via
  `ModifierOperationType`). Concrete types: `FloatStat`, `IntStat`, `BoolStat`.
  `StatContainer` groups them, `StatSheetSO` authors them as an asset, and
  `StatFactory` builds them by type. Reach for it for attack power, move speed,
  buffs and debuffs. `StatClientExample` is a working usage sample.
- **`Resource/`** — a value that *depletes and regenerates*. `Resource<T>` is
  the struct, `ResourceSystem<T>` the MonoBehaviour that ticks regeneration
  (configured by `ResourceSystem<T>.RegenerationData`), and
  `ResourceSystemDataSO<T>` the authored settings. `FloatResource` /
  `FloatResourceSystem` and `IntResource` are the ready-made implementations.
  Reach for it for health, mana, stamina. `ResourceSystemClient` is a working
  player-health sample.

Both are generic with concrete float/int subclasses — derive the concrete type
rather than closing the generic at the use site.

## Events — the sequencer

`Events/` composes gameplay beats out of assets. `EventActionSO` is the
abstract step; the shipped steps are `WaitTimeActionSO`, `WaitSceneActionSO`,
`RaiseEventActionSO`, `ListenEventActionSO`, `CheckpointActionSO`, and
`AudioEventActionSO`. `EventsStepSO` groups steps and `EventSequencer` (a
MonoBehaviour) runs the sequence.

This is asset-authored by design — it exists so a designer can order a cutscene
or tutorial without code. Write a new `EventActionSO` subclass to add a step.

## Other systems worth knowing

- **`Teleport/`** — `TeleportManager` is a `PersistentSingleton` holding
  registered `SpawnPoint`s. `Teleporter` performs the move, `TeleporterTrigger`
  fires it from a collider, and `ITeleportable` is the contract
  (`TeleportableNavMeshAgent` shows a NavMesh implementation).
- **`CloseCaptions/`** — put a `CCSource` next to an `AudioSource`, map clips
  to text with timestamps in `CCDatabaseSO`, and `CCManager` + `CCCanvas`
  display them.
- **`GameState/`** — `GameStateSO<T>` holds the state, `GameStateListener<T>`
  reacts to changes, `GameStateEvent<T>` bridges to the event system.
- **`Objectives/`** — `ObjectiveSO` per objective, `ObjectiveManager` tracks
  completion, `ObjectiveEventChannelSO` broadcasts it (an event channel from
  `Patterns/Behavioural/Observer/ScriptableObjects/`).

## Gotchas

- `Damage/` and `Heal/` are **contracts only** — `IDamageable` + `DamageData`,
  `IHealable` + `HealData`. There is no damage system here; you implement the
  interface on your own component.
- `Unlock/` depends on `SOWithId` and `RuntimeSetSO` from the
  `ScriptableObjects` module.
- Nothing in this module is gated behind an optional package.

```bash
awk -F'\t' '$5 ~ /^Systems\// && $6 != "-"' INDEX.tsv
```
