# Gameplay

`SombraStudios.Shared.Gameplay` — 66 scripts. Drop-on-a-GameObject components
for prototyping movement, spawning, dragging, and simple interactions. Almost
everything here is a `MonoBehaviour` you attach and configure in the Inspector;
this is the module with the lowest time-to-first-use in the library.

## Layout

| Subfolder | What's in it |
|---|---|
| `Behaviours/` | Controller + settings-SO pairs, one folder per behaviour |
| `Movement2D/` | Nine single-purpose 2D movers: `Move`, `Jump`, `Patrol`, `Wander`, `Push`, `Rotate`, `AutoMove`, `AutoRotate`, `FollowTarget` |
| `PlayerMovement3D/` | Eight 3D movement strategies, one per driving mechanism |
| `PlayerMovement2D/` | `PlayerMovement2DTopDown` |
| `Player Controller/` | `CharacterController2D` (move/jump/crouch) and `CharacterController3D` (third person) |
| `Drag/` | Mouse drag, drop, and throw — transform, Rigidbody, and Rigidbody2D variants |
| `Spawners/` | Area, position, grid, and wave spawners |
| *(module root)* | `OnInteract`, `OnTriggerEvent`, `SelectionWithRaycast`, `Ragdoll`, `TiltObject`, `TimedSelfDestruct`, `SimpleObjectPathing`, `SimpleCameraController`, `PickUpAndHold2D` |

## The `Behaviours/` convention

Every folder under `Behaviours/` is the same shape: a `…Controller`
MonoBehaviour that does the work, plus a `…SO` ScriptableObject holding its
tunables. Attach the controller, assign the SO, done.

| Behaviour | Controller | Settings |
|---|---|---|
| Auto-aim | `AutoAimBehaviourController` | `AutoAimBehaviourSO` |
| Auto move / rotate / force / torque | `AutoMoveController`, `AutoRotateController`, `AutoForceController`, `AutoTorqueController` | matching `…SO` |
| Look at / look with lerp | `LookAtController`, `LookWithLerpController` | `LookAtSO`, `LookWithLerpSO` |
| Move towards | `MoveTowardsController` | `MoveTowardsSO` |
| Charge | `ChargeBehaviour` (returns `ChargeBehaviourResult`) | `ChargeBehaviourSO` |
| Destructible | `DestructibleBehaviour` | `DestructibleBehaviourSO` |

All of them implement `IBehaviour`, which carries the enable/disable contract —
use it when you need to toggle a set of behaviours uniformly.

Note this module predates architecture rule 4: the settings here are
ScriptableObject-first rather than POCO-with-defaults. New behaviours should
take a POCO config and wrap it in an SO only if a designer needs asset
variants.

## Picking a movement script

`Movement2D/` and `PlayerMovement3D/` deliberately overlap — each script uses a
*different* mechanism, and the name says which:

- `MovementTransform` — writes `transform.position` directly. Simplest, ignores
  physics.
- `MovementRigidbody` — forces/velocity in `FixedUpdate`. Use when colliders
  matter.
- `MovementCharacterController` — Unity's `CharacterController`. Slopes and
  steps without a Rigidbody.
- `MovementNavMeshAgent` — pathfinds to a clicked point.
- `MovementWithMouse` / `MovementLerpCoroutine` — move to a supplied target
  over time.
- `PlayerMoveOnSphere` — walks around a `SphereCollider`. **Gated on
  `CINEMACHINE`.**
- `ConstrainToBounds` — clamp any of the above inside a box collider.

## Spawners

`SpawnerAreaBase` is the shared base; `SpawnerArea2D` and `SpawnerArea3D`
spawn prefabs at intervals inside a region. `SpawnerPosition` spawns at
predefined points, `GridSpawner` fills an array, and `Spawners/Wave/`
(`WaveSpawner` + `WaveConfigSO`) sequences waves. `SpawnData` holds the shared
prefab/interval configuration.

Anything spawned repeatedly should come from a pool — see
`Patterns/Creational/ObjectPool/` and `Optimization/ReturnToPool`.

## Gates

One gated type in this module:

| Type | Gate |
|---|---|
| `PlayerMoveOnSphere` | `CINEMACHINE` |

Everything else compiles in an empty project. Re-check with:

```bash
awk -F'\t' '$5 ~ /^Gameplay\// && $6 != "-"' INDEX.tsv
```
