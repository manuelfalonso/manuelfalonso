# Unity Scripts Repository
![Background 1280-720](https://github.com/user-attachments/assets/10a650d4-e674-4dea-9b3e-635640fcf29f)

Welcome to my collection of Unity scripts, a growing library designed to help Unity developers streamline their development process. This repository contains reusable, optimized, and production-ready scripts that focus on modularity and clean code practices.

## Table of Contents

1. [About Me](#about-me)
2. [Technologies Used](#technologies-used)
3. [Packages and Preprocessor Directives](#packages-and-preprocessor-directives)
4. [Repository Structure](#repository-structure)
5. [Getting Started](#getting-started)
6. [Key Features](#key-features)
   - [Prefab Instantiation Management with Scriptable Objects](manual/PrefabInstantiateOnLoad.md)
   - [Strategy Pattern Implementation with ScriptableObjects](manual/ScriptableObjectsStrategyPattern.md)
   - [Enum Flags Utility Extensions](manual/EnumFlagExtensions.md)
   - [Extensible ScriptableObject Observer Pattern (Generic Event System)](manual/ScriptableObjectEventChannels.md)
   - [Scene Management via ScriptableObject Strategies](manual/SceneManagerStrategies.md)
   - [Asset and Reference Search Tool (Editor-Only)](manual/Search.md)
   - [Coroutine Management Utilities](manual/Coroutines.md)
   - [Lerp Utilities for Smooth Transitions](manual/Lerp.md)
   - [Unity Message Forwarding for Decoupled Callbacks](manual/UnityMessages.md)
   - [VFX PropertySO Management with Scriptable Objects](manual/VFXPropertySO.md)
7. [Contributing](#contributing)

## About Me

I am a Unity Developer with a passion for creating scalable and reusable code. My experience spans across VR game development, mobile optimization, and implementing complex systems in Unity. This repository reflects my journey in game development and my desire to share useful tools with the Unity community.

## Technologies Used

- **Unity**: All scripts are developed using [Unity](https://unity.com/), leveraging both C# scripting and the Unity Engine.
- **Docfx**: Documentation for the repository is generated using [Docfx](https://dotnet.github.io/docfx/), providing detailed explanations and guides for each script. You can visit the [documentation site here](https://manuelfalonso.github.io/manuelfalonso/).
  
## Packages and Preprocessor Directives

This repository includes various Unity packages, each associated with preprocessor directives to ensure compatibility and efficient usage. Below is a list of the packages used and their related directives:

### Unity
| Package                       | Preprocessor Directive Symbol      | Documentation |
|-------------------------------|------------------------------------|-------------------------------|
| **Splines**                   | `#if UNITY_SPLINES`                | [Splines](https://docs.unity3d.com/Packages/com.unity.splines@2.4/manual/index.html) |
| **XR Interaction Toolkit**    | `#if UNITY_XR_INTERACTION_TOOLKIT` | [XR Interaction Toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.0/manual/index.html) |
| **Cinemachine**               | `#if CINEMACHINE`                  | [Cinemachine](https://docs.unity3d.com/Packages/com.unity.cinemachine@3.1/manual/index.html) |
| **Ads**                       | `#if UNITY_ADVERTISEMENTS`         | [Ads](https://docs.unity.com/ads/en-us/manual/UnityAdsHome) |

### External
| Package                       | Preprocessor Directive Symbol              | Documentation |
|-------------------------------|--------------------------------------------|-------------------------------|
| **Naughty Attributes**        | `#if NAUGHTY_ATTRIBUTES`                   | [Naughty Attributes](https://assetstore.unity.com/packages/p/naughtyattributes-129996) |
| **Serialized Dictionary**     | `#if A_YELLOWPAPER_SERIALIZED_COLLECTIONS` | [Serialized Dictionary](https://assetstore.unity.com/packages/tools/utilities/serialized-dictionary-243052) |
| **DOTween (HOTween v2)**      | `#if DOTWEEN`                              | [DOTween (HOTween v2)](https://docs.unity3d.com/Packages/com.unity.cinemachine@3.1/manual/index.html) |
| **Eflatun.SceneReference**    | `#if Eflatun_SceneReference`               | [Eflatun.SceneReference](https://github.com/starikcetin/Eflatun.SceneReference.git#4.0.0) |
| **Firebase**                  | `#if FIREBASE_APP`                         | [Firebase](https://firebase.google.com/docs/unity/setup?hl=en) |
| **Firebase Authentication**   | `#if FIREBASE_AUTH`                        | [Firebase Authentication](https://firebase.google.com/docs/auth/unity/start?hl=en) |

Make sure the appropriate symbols are defined in the **Scripting Define Symbols** in Unity's **Player Settings** when working with these packages:
Go to Edit > Project Settings > Player. Under **Scripting Define Symbols**, add a new symbol, for example, CINEMACHINE.

## Repository Structure

The repository is organized into the following main folders:

- **Docs/**: Includes all files necessary to generate the documentation site using Docfx.
- **/**: Contains all the Unity scripts organized by functionality.
   - **AI/**: Scripts for artificial intelligence systems and logic.
   - **Animations/**: Scripts related to animators and animation handling.
   - **Attributes/**: Scripts for custom editor attributes and property drawers.
   - **Audio/**: Scripts for audio systems, including sound effects, footsteps, and ambient sounds.
   - **Editor/**: Editor utility scripts, such as scriptable objects, search tools, and custom gizmos.
   - **Enums/**: General-purpose enumerations used across the project.
   - **Examples/**: Demonstration scripts for common patterns and delegates usage.
   - **Extensions/**: C# extension methods to enhance core functionalities.
   - **Gameplay/**: Scripts designed to improve or extend gameplay mechanics.
      - **Behaviours/**: Reusable behavior scripts for common gameplay actions.
      - **Drag/**: Mouse dragging mechanics scripts.
      - **Movement2D/**: Scripts for 2D movement and rotation.
      - **PlayerController/**: Scripts for handling character control and interactions.
      - **PlayerMovement2D/**: Scripts focused on 2D player movement mechanics.
      - **PlayerMovement3D/**: Scripts for handling 3D player movement.
      - **Spawners/**: Systems for spawning objects or entities in the game world.
   - **Inputs/**: 
   - **Interfaces/**: General-purpose interfaces for consistent API designs.
   - **Networking/**: Basic NetCode boilerplate scripts for networked functionality.
   - **Optimization/**: Scripts aimed at improving performance and resource management.
   - **Patters/**: Implementations of programming patterns, like Singleton or Factory.
   - **Physics/**: Scripts interacting with Unity's physics engine.
   - **Scenes/**: Scripts related to managing and transitioning between Unity scenes.
      - **Editor/**: Editor scene change tool
	  - **SceneAsset/**: Utilities for referencing and managing Unity scene assets at runtime.
	  - **Strategies/**: Scenes strategies encapsulated with a flexible and extendable architecture.
   - **ScriptableObjects/**: Scriptable object architecture scripts
      - **Conditions/**: Scriptable objects representing conditional evaluations.
	  - **Editor/**: Editor tools and custom inspectors for managing ScriptableObjects efficiently.
	  - **Patterns/**: ScriptableObject-based implementations of common design patterns.
	  - **RuntimeSets/**: Runtime collections of objects that update dynamically during gameplay.
	  - **Values/**: Generic value containers using ScriptableObjects for shared, observable data.
   - **Services/**: Scripts for third-party integrations or service management.
      - **Advertisement/**: Boilerplate scripts for handling Unity Ads integration.
      - **Firebase/**: Utility scripts for integrating Firebase services.
      - **Notifications/**: Scripts for managing Unity’s notification system.
      - **WebAPI/**: Scripts demonstrating web API consumption.
   - **Splines/**: Scripts related to Unity's Spline package functionalities.
   - **Structs/**: Generic serializable data containers designed for lightweight and efficient value types.
   - **Systems/**: Various game systems and utilities.
      - **CloseCaptions/**: Scripts for implementing closed captions in games.
	  - **Damage/**: Handles the application, calculation, and effects of damage in gameplay.
      - **GameState/**: Scripts to manage game state transitions and logic.
	  - **Heal/**: Encapsulates healing mechanics, including health restoration logic.
      - **LootBox/**: Systems for loot box generation and rewards.
	  - **Objectives/**: Defines goals, milestones, and mission progress tracking.
      - **Resource/**: Systems to manage resources like health, mana, etc.
      - **Stats/**: Systems for managing character stats or game variables.
      - **Teleport/**: Scripts handling teleportation mechanics.
      - **Tutorial/**: Scripts for managing in-game tutorials and guidance.
	  - **Unlock/**: 
   - **Tilemaps/**: Scripts related to Unity's Tilemap system.
   - **Tools/**: General-purpose tools for various tasks.
      - **Search/**: Provides runtime and editor search tools for filtering and locating assets and references.
   - **UI/**: Scripts for managing user interface elements.
      - **ImageDrag/**: Scripts to allow dragging of UI images.
      - **InventorySystem/**: Scripts for handling inventory systems.
      - **Mobile/**: UI scripts optimized for mobile devices.
      - **PanelDragAndResize/**: Scripts for dragging and resizing UI panels.
   - **Utility/**: General utility scripts that provide common functionality.
      - **Cooldown/**: Scripts to manage cooldown timers.
      - **Coroutines/**: Scripts to simplify coroutine management.
      - **Destroyer/**: Conditional object destruction logic.
      - **DontDestroy/**: Ensures persistence of GameObjects across scene transitions.
      - **Lerp/**: Linear interpolation utility scripts.
	  - **Logger/**: Simple and customizable logging system for debugging and tracing.
      - **FrameRate/**: Scripts to manage or display frame rates.
	  - **Mirror/**: Scripts to mirror the Transform on position, rotation or scale along a given axis.
      - **Mobile/**: Scripts for mobile-specific functionality, like touch inputs or keyboards.
	  - **NullReferenceChecker/**: Helps identify and report missing object references.
      - **PrefabInstantiateOnLoad/**: Automatically instantiate prefabs on scene load.
      - **Resources/**: Scripts to manage resource loading and references.
	  - **Sprites/**: Utility scripts and containers for managing sprite assignments and metadata.
      - **Timers/**: Timer-related scripts for time-based actions.
      - **TimeScale/**: Scripts for controlling time scaling in the game.
      - **UnityGizmos/**: Custom Gizmo drawing utilities.
      - **UnityMessages/**: Scripts to handle Unity event functions, like Awake() and Start().
   - **VFX/**: Scripts for managing visual effects.
      - **CameraShake/**: Scripts for implementing camera shake effects using DoTween.
      - **Parallax/**: Parallax scrolling effects for 2D or 3D.
      - **PropertySO/**: ScriptableObject-driven system for applying and reverting visual effects modularly.
   - **Video/**: Scripts related to video playback and controls.
   - **XR/**: Scripts related to XR (extended reality) interactions.
      - **Interactables/**: Scripts for XR interactable objects.
      - **KeyLockSystem/**: Systems for key-based locking mechanisms in XR environments.
      - **SocketInteractor/**: Scripts for handling socket interactions in XR setups.

## Getting Started

To get started with this repository:

1. Clone the repository to your local machine.
2. Make sure all necessary packages are installed and enabled in your Unity project.
3. Add the required scripting define symbols (mentioned above) in your Unity Player settings.
4. Review the [documentation](https://manuelfalonso.github.io/manuelfalonso/) for detailed usage instructions for each script.

## Key Features

This repository includes a variety of features designed to enhance Unity development. Below are some of the key features:

* [Prefab Instantiation Management with Scriptable Objects](manual/PrefabInstantiateOnLoad.md)
* [Strategy Pattern Implementation with ScriptableObjects](manual/ScriptableObjectsStrategyPattern.md)
* [Enum Flags Utility Extensions](manual/EnumFlagExtensions.md)
* [Extensible ScriptableObject Observer Pattern (Generic Event System)](manual/ScriptableObjectEventChannels.md)
* [Scene Management via ScriptableObject Strategies](manual/SceneManagerStrategies.md)
* [Asset and Reference Search Tool (Editor-Only)](manual/Search.md)
* [Coroutine Management Utilities](manual/Coroutines.md)
* [Lerp Utilities for Smooth Transitions](manual/Lerp.md)
* [Unity Message Forwarding for Decoupled Callbacks](manual/UnityMessages.md)
* [VFX PropertySO Management with Scriptable Objects](manual/VFXPropertySO.md)

## Contributing

If you would like to contribute to this repository, feel free to submit a pull request or open an issue. Any suggestions or improvements are welcome!

---

Thank you for visiting this repository. Happy coding!