using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SO used by the Footsteps Manager script to separate list of clips 
/// depending of checking Collisions with Tags.
/// </summary>
[CreateAssetMenu(fileName = "New Footstep Audio List by Tag", menuName = "Footstep Audio List", order = 51)]
public class FootstepAudioClipListSO : ScriptableObject
{
    [Tooltip("The Tag must be the same name as the one in the Tag List in the Editor")]
    public string Tag;
    public List<AudioClip> Steps = new List<AudioClip>();
}
