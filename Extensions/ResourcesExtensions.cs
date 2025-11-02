using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    public static class ResourcesExtensions
    {
        /// <summary>
        /// Loads a volume profile from the specified path and assigns it to the volume.
        /// </summary>
        /// <remarks>If a volume profile is found at the specified path, it is assigned to the <see
        /// cref="UnityEngine.Rendering.Volume.profile"/> property of the <paramref name="volume"/>. If no profile is
        /// found, a warning is logged to the console.</remarks>
        /// <param name="volume">The volume to which the loaded profile will be assigned.</param>
        /// <param name="path">The relative path to the volume profile within a 'Resources' folder. The path should not include the file
        /// extension.</param>
        public static void LoadVolumeProfile(this UnityEngine.Rendering.Volume volume, string path)
        {
            var volumeProfile = Resources.Load<UnityEngine.Rendering.VolumeProfile>(path);
            if (volumeProfile != null)
            {
                volume.profile = volumeProfile;
            }
            else
            {
                Debug.LogWarning($"Volume Profile not found at path: {path}. Make sure the profile is located in a 'Resources' folder and the path is correct.");
            }
        }
    }
}
