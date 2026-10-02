# Safe Area

Keeps UI out of the parts of the screen the hardware covers: notch, Dynamic Island, punch-hole camera,
rounded corners, home indicator and gesture bar. Works with both uGUI and UI Toolkit, lives in its own
assembly (`SombraStudios.Shared.UI.SafeArea`, folder `UI/SafeArea/`) and depends on no packages.

| Type | Use it for |
|---|---|
| `SafeAreaUGUI` | uGUI: a component that drives a RectTransform's anchors |
| `SafeAreaElement` | UI Toolkit: a custom element that sets its own padding |
| `SafeAreaCalculator` | The math behind both, for anything else (e.g. touch zones) |

## When to use it

- Any mobile UI with buttons, text or HUD elements near the screen edges.
- Screens that rotate, run in split screen or on foldables: changes are detected automatically.

## When not to use it

- World Space canvases and UI Toolkit panels rendered to a texture: the screen safe area does not apply.
- To keep the *game world* visible (e.g. a board under the notch): that is a camera concern, not a UI one.

## Concepts

- **Backgrounds bleed, content does not.** Put full-screen backgrounds *outside* the safe area container and
  everything that must stay visible or touchable *inside* it.
- **Edges** (`SafeAreaEdges`, flags) choose which edges are inset. A top bar only needs `Top`, a bottom
  navigation bar only `Bottom`. Default: `All`.
- **Horizontally symmetric** uses the larger of the left and right insets on both sides, so content stays
  centred in landscape when the notch sits on one side only.

## uGUI

1. In a Screen Space (Overlay or Camera) canvas, create an empty child and stretch it to fill the canvas.
2. Add **Sombra Studios > UI > Safe Area (uGUI)** to it.
3. Move the content that must avoid the unsafe area under it.

```
Canvas
├── Background          ← full screen
└── SafeArea            ← SafeAreaUGUI
    └── Content
```

- Insets are relative to the **parent**, so the component also works on a non-fullscreen parent such as a
  top bar.
- Applied in **Play Mode only**, so scenes and prefabs are never modified. Disabling the component restores
  the original anchors.

## UI Toolkit

Wrap the content in a `SafeAreaElement` that fills the screen.

**UI Builder:** Library > Project > Custom Controls (C#) > SombraStudios.Shared.UI.SafeArea >
**SafeAreaElement**. Drag it into the Hierarchy, set **Flex > Grow** to `1` and place the content inside.

**UXML:**

```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements">
    <SombraStudios.Shared.UI.SafeArea.SafeAreaElement edges="All" style="flex-grow: 1;">
        <!-- Content -->
    </SombraStudios.Shared.UI.SafeArea.SafeAreaElement>
</ui:UXML>
```

- The element **owns the padding** of the edges it handles; excluded edges keep their USS padding.
- It does nothing in the UI Builder canvas. The result shows in the Game view and Device Simulator.
- It ignores pointer events, so clicks on empty space reach whatever is behind it.

## From code

Both adapters expose the same members:

```csharp
safeArea.Edges = SafeAreaEdges.Top | SafeAreaEdges.Horizontal;
safeArea.IsHorizontallySymmetric = true;
SafeAreaInsets insets = safeArea.CurrentInsets;
safeArea.ForceRefresh();
```

`CurrentInsets` is in screen pixels for `SafeAreaUGUI` and in panel units for `SafeAreaElement`.

For anything that is not UI, call the calculator directly:

```csharp
var screen = new Rect(0f, 0f, Screen.width, Screen.height);
SafeAreaInsets insets = SafeAreaCalculator.CalculateInsets(Screen.safeArea, screen, SafeAreaEdges.All);
```

## Testing

1. Switch the Game view to **Simulator** (top-left dropdown) and pick a device with a notch.
2. Enable the **Safe Area** toggle to draw the safe area gizmo.
3. Enter Play Mode and check that the content's edges match the gizmo.
4. **Rotate** the device and switch to a device without a notch; both should update immediately.

## Troubleshooting

| Symptom | Cause |
|---|---|
| Warning: *must be a child of a RectTransform inside a Canvas* | `SafeAreaUGUI` is on the canvas itself or outside one |
| Warning: *is on a World Space canvas* | World Space UI is not affected by the screen safe area |
| uGUI content does not move in edit mode | Expected: it only applies in Play Mode |
| UI Toolkit content does not move in the UI Builder | Expected: it only acts on runtime panels |
| UI Toolkit content does not fill the screen | The `SafeAreaElement` needs `flex-grow: 1` (or absolute position with zero offsets) |
