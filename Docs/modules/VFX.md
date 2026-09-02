# VFX

`SombraStudios.Shared.VFX` — 47 scripts, and 37 of them are one system:
`PropertySO/`, an asset-authored way to drive shader and material properties
without writing a script per effect. The rest are standalone helpers.

There is a longer walkthrough in [`Docs/manual/VFXPropertySO.md`](../manual/VFXPropertySO.md).

## Layout

| Subfolder | What's in it |
|---|---|
| `PropertySO/` | `VFXPropertySO` base, `BaseVFXController`, `CompositeVFXPropertySO`, `RevertVFXOnStateExit` |
| `PropertySO/Material/` | 18 SOs that write directly to a `Material` |
| `PropertySO/MaterialPropertyBlock/` | 13 SOs that write through a `MaterialPropertyBlock` |
| `PropertySO/BlendShape/` | `SetBlendShapeVFXSO` for skinned meshes |
| `Parallax/` | `ParallaxController` + `ParallaxLayer` |
| `CameraShake/` | `CameraShakeStrategy` + `CameraShakeDataSO` — **gated on `DOTWEEN`** |
| *(module root)* | `LerpColor`, `RandomColor`, `TextureOffset`, `PingPongLight`, `MaterialHandler`, `LinePositionsUpdater`, `AttachGameObjectsToParticles`, `ParticleSystemStoppedAction` |

## How PropertySO works

`VFXPropertySO` derives from `StrategySO` (see `Patterns/Behavioural/Strategy`)
— each asset *is* one effect, and `BaseVFXController` is the MonoBehaviour that
applies it. Compose several with `CompositeVFXPropertySO`, and use
`RevertVFXOnStateExit` (a `StateMachineBehaviour`) to undo an effect when an
animator state exits.

To apply an effect: create the matching `…VFXSO` asset, set the shader property
name and value on it, and hand it to a `BaseVFXController` on the renderer.

## Material or MaterialPropertyBlock

This is the choice that matters, and both trees mirror each other (`MaterialSet…`
vs `MPBSet…` for Color, Float, Integer, Vector, Matrix, Texture, and the array
forms).

- **`Material/`** — writes to the material itself. Affects every renderer
  sharing it, and touching `renderer.material` at runtime instantiates a copy.
  Also covers things a property block cannot do: `MaterialSetShaderVFXSO`,
  `MaterialEnableKeywordVFXSO` / `MaterialDisableKeywordVFXSO`,
  `MaterialSetRenderQueueVFXSO`, `MaterialCopyPropertiesFromMaterialVFXSO`.
- **`MaterialPropertyBlock/`** — per-renderer overrides with no material
  instancing, so batching survives. Use it for per-instance tints, progress
  bars, hit flashes — anything where many objects share a material but need
  different values. `MaterialPropertyBlockClearVFXO` resets the block.

Rule of thumb: **per-object value → MPB; changing the shader, a keyword, or the
render queue → Material.**

`IMaterialProperty<T>` and `IMPBProperty<T>` are the respective contracts if
you add a property type.

## Gates

| Type | Gate |
|---|---|
| `CameraShakeStrategy` | `DOTWEEN` |
| `CameraShakeDataSO` | `DOTWEEN` |

`CameraShake/` compiles out entirely without DOTween — do not build anything on
it that must work in a bare project. Everything else in the module is
unconditional.

```bash
awk -F'\t' '$5 ~ /^VFX\// && $6 != "-"' INDEX.tsv
```
