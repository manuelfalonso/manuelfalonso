using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Represents a condition that evaluates whether the current runtime platform matches any of the specified valid
    /// platforms.
    /// </summary>
    /// <remarks>This condition is considered valid if the current runtime platform, as determined by <see
    /// cref="Application.platform"/>,  matches any of the platforms specified in the <c>_validPlatforms</c> field. Logs
    /// can optionally be displayed to indicate  whether the condition was met.</remarks>
    [CreateAssetMenu(fileName = "NewPlatformCondition", menuName = "Sombra Studios/Conditions/Platform Condition")]
    public class PlatformConditionSO : ConditionSO
    {
        [Header(PROPERTIES_TITLE)]
        [Tooltip("The platforms on which the condition is valid.")]
        [SerializeField] private RuntimePlatform[] _validPlatforms;

        public override bool IsValid()
        {
            foreach (var platform in _validPlatforms)
            {
                if (Application.platform == platform)
                {
                    if (_showLogs)
                    {
                        Logger.Log(LOG_CATEGORY, $"Condition met for platform: {platform}", this);
                    }
                    return true;
                }
            }
            if (_showLogs)
            {
                Logger.Log(LOG_CATEGORY, $"Condition not met for current platform: {Application.platform}", this);
            }
            return false;
        }
    }
}