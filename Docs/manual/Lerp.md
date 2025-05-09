# Lerp Utilities for Smooth Transitions

- **Generic Lerp Utility Functions**: Provides reusable linear interpolation (`Lerp`) and unclamped interpolation methods for various data types like `float`, `Vector2`, `Vector3`, and `Color`.
- **Ease of Use in Gameplay and UI Code**: Enables smooth transitions without the boilerplate, helping with animations, UI effects, camera movement, fading, or value blending.
- **Custom Extension Methods**: Includes intuitive and readable extension methods for applying interpolation directly on supported types, increasing code clarity.
- **Support for Fixed and Delta Time**: Offers overloads for both unscaled and delta time, ensuring smooth performance regardless of framerate or time scaling.

## When to Use It

- When you need to implement smooth transitions over time for values, visuals, or game logic.
- For improving the readability and maintainability of any codebase with frequent interpolation needs.

## When Not to Use It

- For highly complex animations or easing that require tweening libraries.
- If transitions depend on physics-based motion or require precise physical accuracy.