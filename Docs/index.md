# Unity Scripts Repository

Welcome to my collection of Unity scripts, a growing library designed to help Unity developers streamline their development process. This repository contains reusable, optimized, and production-ready scripts that focus on modularity and clean code practices.

## About Me

I am a Unity Developer with a passion for creating scalable and reusable code. My experience spans across VR game development, mobile optimization, and implementing complex systems in Unity. This repository reflects my journey in game development and my desire to share useful tools with the Unity community.

## Technologies Used

- **Unity**: All scripts are developed using [Unity](https://unity.com/), leveraging both C# scripting and the Unity Engine.
- **Docfx**: Documentation for the repository is generated using [Docfx](https://dotnet.github.io/docfx/), providing detailed explanations and guides for each script. You can visit the [documentation site here](https://manuelfalonso.github.io/Unity-Scripts/).
  
## Packages and Preprocessor Directives

This repository includes various Unity packages, each associated with preprocessor directives to ensure compatibility and efficient usage. Below is a list of the packages used and their related directives:

### Unity
| Package                       | Preprocessor Directive Symbol      | Namespace                              | Documentation |
|-------------------------------|------------------------------------|----------------------------------------|-------------------------------|
| **Splines**                   | `#if UNITY_SPLINES`                | **UnityEngine.Splines**                | [Splines](https://docs.unity3d.com/Packages/com.unity.splines@2.4/manual/index.html) |
| **XR Interaction Toolkit**    | `#if UNITY_XR_INTERACTION_TOOLKIT` | **UnityEngine.XR.Interaction.Toolkit** | [XR Interaction Toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.0/manual/index.html) |
| **Cinemachine**               | `#if CINEMACHINE`                  | **Unity.Cinemachine**                  | [Cinemachine](https://docs.unity3d.com/Packages/com.unity.cinemachine@3.1/manual/index.html) |
| **Ads**                       | `#if UNITY_ADVERTISEMENTS`         | **UnityEngine.Advertisements**         | [Ads](https://docs.unity.com/ads/en-us/manual/UnityAdsHome) |

### External
| Package                       | Preprocessor Directive Symbol              | Namespace                              | Documentation |
|-------------------------------|--------------------------------------------|----------------------------------------|-------------------------------|
| **Naughty Attributes**        | `#if NAUGHTY_ATTRIBUTES`                   | **NaughtyAttributes**                  | [Naughty Attributes](https://assetstore.unity.com/packages/p/naughtyattributes-129996) |
| **Serialized Dictionary**     | `#if A_YELLOWPAPER_SERIALIZED_COLLECTIONS` | **AYellowpaper.SerializedCollections** | [Serialized Dictionary](https://assetstore.unity.com/packages/tools/utilities/serialized-dictionary-243052) |
| **DOTween (HOTween v2)**      | `#if DOTWEEN`                              | **DG.Tweening**                        | [DOTween (HOTween v2)](https://docs.unity3d.com/Packages/com.unity.cinemachine@3.1/manual/index.html) |
| **Eflatun.SceneReference**    | `#if Eflatun_SceneReference`               | **Eflatun.SceneReference**             | [Eflatun.SceneReference](https://github.com/starikcetin/Eflatun.SceneReference.git#4.0.0) |
| **Firebase**                  | `#if FIREBASE_APP`                         | **Firebase**                           | [Firebase](https://firebase.google.com/docs/unity/setup?hl=en) |
| **Firebase Authentication**   | `#if FIREBASE_AUTH`                        | **Firebase.Auth**                      | [Firebase Authentication](https://firebase.google.com/docs/auth/unity/start?hl=en) |

Make sure the appropriate symbols are defined in the **Scripting Define Symbols** in Unity's **Player Settings** when working with these packages:
Go to Edit > Project Settings > Player. Under **Scripting Define Symbols**, add a new symbol, for example, CINEMACHINE.

## Repository Structure

The repository is organized into the following main folders:

- **/**: Contains all the Unity scripts organized by functionality.
- **Docs/**: Includes all files necessary to generate the documentation site using Docfx.

## Getting Started

To get started with this repository:

1. Clone the repository to your local machine.
2. Make sure all necessary packages are installed and enabled in your Unity project.
3. Add the required scripting define symbols (mentioned above) in your Unity Player settings.
4. Review the [documentation](https://manuelfalonso.github.io/Unity-Scripts/) for detailed usage instructions for each script.

## Contributing

If you would like to contribute to this repository, feel free to submit a pull request or open an issue. Any suggestions or improvements are welcome!

---

Thank you for visiting this repository. Happy coding!
