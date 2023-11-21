using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Audio.Footsteps
{
    /// <summary>
    /// Select an audio clip list with the CharacterController floor hit
    /// </summary>
    public class CharacterControllerFootstepController : FootstepController
    {
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            SelectAudioClipList(hit.transform);
        }
    }
}

