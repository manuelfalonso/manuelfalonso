using UnityEngine;

namespace SombraStudios.Shared.Utility.NullReferenceChecker
{
    /// <summary>
    /// A custom PropertyAttribute to bypass the above Validate check.
    /// </summary>
    public class OptionalAttribute : PropertyAttribute
    {
    }
}
