using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Represents a condition that evaluates whether the application is running in the Unity Editor.
    /// </summary>
    /// <remarks>This condition checks the <see cref="Application.isEditor"/> property to determine if the
    /// application is running in the Unity Editor. It can optionally log the evaluation result if logging is
    /// enabled.</remarks>
    [CreateAssetMenu(fileName = "NewEditorModeCondition", menuName = "Sombra Studios/Conditions/Editor Mode Condition")]
    public class EditorModeConditionSO : ConditionSO
    {
        public override bool IsValid()
        {
            bool isEditor = Application.isEditor;
            if (_showLogs)
            {
                Logger.Log(LOG_CATEGORY, $"Condition met: {isEditor}", this);
            }
            return isEditor;
        }
    }
}