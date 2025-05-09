# Asset and Reference Search Tool (Editor-Only)

- **Advanced Search Utility for Assets and Components**: This custom Editor tool enables deep and flexible search operations across your Unity project, including assets, components in scenes, and serialized references.
- **Supports Asset and Scene Queries**: It can locate assets by type, name, and path, and also search scenes for components and fields that reference specific objects or types.
- **Reference Search Functionality**: Built-in tools allow you to trace serialized references to specific ScriptableObjects, prefabs, or MonoBehaviours—ideal for refactoring and debugging.
- **Modular Search Architecture**: The tool is organized into modular search strategies, allowing extension and customization to fit your studio or project’s asset workflow.
- **Context Menu Integration**: Many of the search utilities can be triggered via the Unity context menu, enabling fast one-click investigation from the Project or Hierarchy views.

## When to Use It

- When cleaning up unused assets or determining the impact of removing a ScriptableObject or prefab.
- During refactors to track all references to a given type or instance.
- For debugging unexpected behaviors linked to misplaced or redundant references in scenes or prefabs.

## When Not to Use It

- If you're looking for runtime search or tracking (this tool is strictly Editor-only).
- In very small projects where reference tracing can be managed manually.