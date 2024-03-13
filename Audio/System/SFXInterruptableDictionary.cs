using System;

namespace SombraStudios.Shared.Audio.System
{
    /// <summary>
    /// Represents a dictionary mapping string keys to interruptable SFXRack instances.
    /// </summary>
    [Serializable]
    public class SFXInterruptableDictionary : SFXStringDictionary
    {
        /// <summary>
        /// Plays the audio associated with the specified key, interrupting any currently playing audio.
        /// </summary>
        /// <param name="evt">The key associated with the audio to play.</param>
        public override void Play(string evt)
        {
            InterruptAll();
            base.Play(evt);
        }


        /// <summary>
        /// Stops all audio instances associated with this dictionary.
        /// </summary>
        private void InterruptAll()
        {
            foreach (var rack in this)
            {
                rack.Value.AudioInstance.Stop();
            }
        }
    }
}
