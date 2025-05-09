# Enum Flags Utility Extensions

- **Bitwise Operation Helpers**: Provides clean, readable extension methods to work with `[Flags]` enums in C#, such as `HasFlagFast`, `AddFlag`, and `RemoveFlag`.
- **Improved Readability**: Eliminates verbose bitwise logic in your code, making conditionals involving enum flags much easier to read and maintain.
- **Safe and Generic**: Fully generic implementation that ensures type safety across all enum flag operations using constraints.

## Example Usage

```csharp
[Flags]
public enum UnitState
{
    None = 0,
    Idle = 1 << 0,
    Moving = 1 << 1,
    Attacking = 1 << 2
}

// Combine flags
UnitState state = UnitState.Idle | UnitState.Moving;

// Check flag
bool isMoving = state.HasFlagFast(UnitState.Moving);

// Remove a flag
state = state.RemoveFlag(UnitState.Idle);

// Add a flag
state = state.AddFlag(UnitState.Attacking);
```

## When to Use It

- When using [Flags] enums to represent combinations of states, permissions, toggles, or modifiers.
- In systems where flag manipulation is frequent and needs to remain concise and highly readable (e.g., state machines, ability systems, UI settings, etc).

## When Not to Use It

- If you're not using [Flags] enums, or if your enum represents mutually exclusive values.
- In highly performance-critical sections where even the minimal overhead of an extension method might be a concern (though in most cases it's negligible).